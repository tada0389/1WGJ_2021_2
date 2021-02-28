using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TadaLib;

/// <summary>
/// プレイヤーの無敵状態を管理するステート
/// </summary>

namespace MainGame.Actor
{
    public partial class PlayerController
    {
        [System.Serializable]
        private class StateSuper : StateMachine<PlayerController>.StateBase
        {
            [SerializeField]
            private Vector2 power = new Vector2(1.5f, 0.2f);

            // 無敵時間
            [SerializeField]
            private float mutekiDuration = 1.0f;

            [SerializeField]
            private ParticleSystem mutekiEff;

            [SerializeField]
            private ParticleSystem earthEffPrefab;

            // ダッシュSE
            [SerializeField]
            private AudioClip dashSE;

            // ステートが始まった時に呼ばれるメソッド
            public override void OnStart()
            {
                // アピールゲージの消費
                Parent.UseAppealGauge();

                // 右上に飛ばす
                Parent.Velocity = power;
                Parent.Accel = Accel;

                // 回転
                Parent.rotater.Rotate(2.0f);

                mutekiEff.gameObject.SetActive(true);
                mutekiEff.Play();

                Parent.audioSource.PlayOneShot(dashSE);
            }

            // ステートが終了したときに呼ばれるメソッド
            public override void OnEnd()
            {
                mutekiEff.gameObject.SetActive(false);
            }

            // 毎フレーム呼ばれる関数
            public override void Proc()
            {
                // 最低限の横速度は守る
                Parent.Velocity = new Vector2(Mathf.Max(1.0f, Parent.Velocity.x), Parent.Velocity.y);

                // 接地してたらwalkステートへ　ただ、一定時間は無視する(ジャンプできなくなるので)
                if (Parent.trb.ButtomCollide && Timer >= ((50.0f / StageObjectManager.staticRotateSpeed) * 0.2f))
                {
                    // エフェクトも出す
                    var obj = Instantiate(earthEffPrefab, Parent.transform.position - new Vector3(-0.05f, 0.0f), Quaternion.identity);

                    ChangeState((int)eState.Walk);
                    return;
                }
            }
        }
    }
} // namespace MainGame.Actor