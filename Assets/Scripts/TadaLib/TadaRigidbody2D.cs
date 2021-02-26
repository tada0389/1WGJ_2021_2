﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 左右上下で衝突しているか取得するクラス
/// もし判定に埋まっていた場合は戻してあげる
/// </summary>

namespace TadaLib
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TadaRigidbody2D : MonoBehaviour
    {
        // 衝突検知するオブジェクトレイヤー
        [SerializeField]
        private int collideMask = 1 << 8;

        // 左でぶつかっている
        public bool LeftCollide { private set; get; }
        // 右でぶつかっている
        public bool RightCollide { private set; get; }
        // 上でぶつかっている
        public bool TopCollide { private set; get; }
        // 下でぶつかっている
        public bool ButtomCollide { private set; get; }

        private BoxCollider2D hitBox;

        private const float kEpsilon = 0.001f;

        private void Start()
        {
            // 衝突の初期化
            LeftCollide = false;
            RightCollide = false;
            TopCollide = false;
            ButtomCollide = true;

            hitBox = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            // 衝突の判定チェック
            CollideCheck();
        }

        private void CollideCheck()
        {
            // 衝突の初期化
            LeftCollide = false;
            RightCollide = false;
            TopCollide = false;
            ButtomCollide = false;

            // レイを飛ばして衝突してるか判定
            // 埋まってる場合は戻す

            Vector2 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            scale.y = Mathf.Abs(scale.y);

            // 当たり判定(矩形)のサイズと中心
            Vector2 offset = hitBox.offset * scale;
            Vector2 halfSize = hitBox.size * scale * 0.5f;

            // レイキャストを飛ばす中心
            Vector2 origin = (Vector2)transform.position + offset;
            // レイの長さ
            float lengthX = halfSize.x / 2f;
            // y軸は少し余分に取る
            float lengthY = halfSize.y + halfSize.x * 1.5f;

            // 始めにy軸負の方向に飛ばす 3本の線を出す
            {
                // 左端，中央，右端の順に確かめる
                Vector2 originLeft = origin + (Vector2)(Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z) * new Vector2(-halfSize.x * 0.8f, 0f));
                Vector2 originRight = origin + (Vector2)(Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z) * new Vector2(halfSize.x * 0.8f, 0f));
                RaycastHit2D hitDownLeft = LinecastWithGizmos(originLeft, originLeft + new Vector2(0f, -lengthY), collideMask);
                RaycastHit2D hitDownCenter = LinecastWithGizmos(origin, origin + new Vector2(0f, -lengthY), collideMask);
                RaycastHit2D hitDownRight = LinecastWithGizmos(originRight, originRight + new Vector2(0f, -lengthY), collideMask);

                // めり込んでいる分は上に持ち上げる
                float length = halfSize.y; // 地面までの通常の距離

                float upLength = -1.0f;
                if (hitDownLeft) upLength = Mathf.Max(upLength, length - hitDownLeft.distance);
                if (hitDownCenter) upLength = Mathf.Max(upLength, length - hitDownCenter.distance);
                if (hitDownRight) upLength = Mathf.Max(upLength, length - hitDownRight.distance);
                
                // 盛り上がった
                if(upLength > -kEpsilon)
                {
                    ButtomCollide = true;
                    float rotZ = transform.eulerAngles.z * Mathf.Deg2Rad + Mathf.PI / 2.0f; // 要調整
                    Vector2 add = new Vector2(Mathf.Cos(rotZ), Mathf.Sin(rotZ)) * upLength;
                    transform.position += (Vector3)add;
                }
            }
            // 下方向に接していたなかったら上方向もやる
            {
                // やっぱやらない 今回は必要なさそう
            }

            // 右方向
            {
                RaycastHit2D hit_left = Physics2D.BoxCast(origin, new Vector2(halfSize.x, halfSize.y * 0.6f), 0f, Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z) * Vector2.left,
                        halfSize.x / 2f, collideMask);
                Debug.DrawLine(origin, origin - (Vector2)(Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z) * new Vector2(halfSize.x, 0f)), Color.blue);
                if (hit_left) LeftCollide = true;
            }

            // 左方向
            {
                RaycastHit2D hit_right = Physics2D.BoxCast(origin, new Vector2(halfSize.x, halfSize.y * 0.6f), 0f, Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z) * Vector2.right,
                    halfSize.x / 2f, collideMask);
                Debug.DrawLine(origin, origin + (Vector2)(Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z) * new Vector2(halfSize.x, 0f)), Color.blue);

                if (hit_right) RightCollide = true;
            }
        }

        // レイキャストを飛ばす(+ Debugの線を引く)
        RaycastHit2D LinecastWithGizmos(Vector2 from, Vector2 to, int layer_mask)
        {
            RaycastHit2D hit = Physics2D.Linecast(from, (Vector2)(Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z) * to), layer_mask);
            Debug.DrawLine(from, (hit) ? to : to);
            return hit;
        }
    }
}