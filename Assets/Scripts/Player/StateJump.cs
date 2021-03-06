﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TadaLib;

/// <summary>
/// プレイヤーのジャンプ状態を管理するステート
/// </summary>

namespace MainGame.Actor
{
    public partial class PlayerController
    {
        [System.Serializable]
        private class StateJump : StateMachine<PlayerController>.StateBase
        {
            // ジャンプ力
            [SerializeField]
            private float jumpPower = 0.15f;

            // ジャンプ長押しの有効時間
            [SerializeField]
            private float jumpEnableDuration = 0.3f;

            // どれくらいのy軸方向の速度ならアピールを許すか
            [SerializeField]
            private float appealEnableSpeed = 0.01f;

            private bool doubleJumped = false;

            // アピールエフェクト
            [SerializeField]
            private ParticleSystem appealEff;
            [SerializeField]
            private ParticleSystem appealEff2;

            [SerializeField]
            private float appealGreadUpPosYThr = 9.0f;

            // ジャンプSE
            [SerializeField]
            private AudioClip jumpSE;

            [SerializeField]
            private AudioClip appealSE;

            [SerializeField]
            private AppealChanceCircle circle;

            private bool doneAppeal;

            // ステートが始まった時に呼ばれるメソッド
            public override void OnStart()
            { 
                Parent.Accel = this.Accel;
                Parent.Velocity = new Vector2(1.0f, jumpPower);

                doubleJumped = false;

                // 回転
                Parent.rotater.StopRotate();

                Parent.audioSource.PlayOneShot(jumpSE);

                doneAppeal = false;
            }

            // ステートが終了したときに呼ばれるメソッド
            public override void OnEnd()
            {
                circle.Hide();
            }

            // 毎フレーム呼ばれる関数
            public override void Proc()
            {
                // 無敵モード
                if (Parent.MutekiRequest() && Parent.input.GetButtonDown(TadaInput.ButtonCode.MouseRight))
                {
                    ChangeState((int)eState.Super);
                    return;
                }

                // 上方向に当たっていたら速度をゼロに
                if (Parent.trb.TopCollide)
                {
                    Parent.Velocity = new Vector2(Parent.Velocity.x, -0.05f);
                }

                // 自身の左右が壁に当たったら死亡
                if (Parent.trb.RightCollide || Parent.trb.LeftCollide)
                {
                    ChangeState((int)eState.Dead);
                    return;
                }

                // 接地してたらwalkステートへ　ただ、一定時間は無視する(ジャンプできなくなるので)
                if (Parent.trb.ButtomCollide && Timer >= ((50.0f / StageObjectManager.staticRotateSpeed) * 0.2f))
                {
                    ChangeState((int)eState.Walk);
                    return;
                }

                // ジャンプボタン長押し
                if (Parent.input.GetButton(TadaInput.ButtonCode.MouseLeft) && Timer <= jumpEnableDuration && !doubleJumped)
                {
                    Parent.Velocity = new Vector2(Parent.Velocity.x, jumpPower);
                }
                else
                {
                    doubleJumped = true;
                }

                // ジャンプボタン＋頂点付近ならアピール
                if (!doneAppeal)
                {
                    if (Mathf.Abs(Parent.Velocity.y) <= appealEnableSpeed && Parent.input.GetButtonDown(TadaInput.ButtonCode.MouseLeft, false))
                    {
                        if (Parent.Position.y < appealGreadUpPosYThr)
                        {
                            if (!StageObjectManager.isTitle)
                            {
                                Parent.AddAppealGauge();
                            }
                            appealEff.gameObject.SetActive(true);
                            appealEff.Play();
                        }
                        else
                        {
                            if (!StageObjectManager.isTitle)
                            {
                                Parent.AddAppealGauge(2);
                            }
                            appealEff2.gameObject.SetActive(true);
                            appealEff2.Play();
                        }
                        Parent.color.Flash(); // 本体を光らせる
                        Parent.audioSource.PlayOneShot(appealSE);
                        doneAppeal = true;
                    }

                    // アピールチャンスの円を書く
                    if (Parent.Velocity.y >= 0.0f)
                    {
                        //もしボタン押してアピールできる状態なら
                        if (Mathf.Abs(Parent.Velocity.y) <= appealEnableSpeed)
                        {
                            //circleの色を押せることをわかりやすくするために変える
                            circle.AppealColorChenge();
                        }
                        circle.Show(Mathf.Abs(Parent.Velocity.y));
                    }
                    else circle.Hide();
                }
                else circle.Hide();
            }
        }
    }
} // namespace MainGame.Actor