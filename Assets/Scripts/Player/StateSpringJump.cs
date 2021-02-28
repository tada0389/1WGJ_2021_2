using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TadaLib;

/// <summary>
/// プレイヤーのばねジャンプ状態を管理するステート
/// </summary>

namespace MainGame.Actor
{
    public partial class PlayerController
    {
        [System.Serializable]
        private class StateSpringJump : StateMachine<PlayerController>.StateBase
        {
            // ジャンプ力
            [SerializeField]
            private float jumpPower = 0.15f;

            [SerializeField]
            private float fixedPosY = 7.2f;

            // どれくらいのy軸方向の速度ならアピールを許すか
            [SerializeField]
            private float appealEnableSpeed = 0.01f;

            // アピールエフェクト
            [SerializeField]
            private ParticleSystem appearlEff;

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

                // 座標を固定する (飛んでいく方向がおかしくなるので)
                Parent.SetPosition(new Vector2(Parent.Position.x, fixedPosY));

                
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
                if (Parent.trb.ButtomCollide && Timer >= (0.4f / 5.0f))
                {
                    ChangeState((int)eState.Walk);
                    return;
                }

                // ジャンプボタン＋頂点付近ならアピール
                if (!doneAppeal)
                {
                    if (Mathf.Abs(Parent.Velocity.y) <= appealEnableSpeed && Parent.input.GetButtonDown(TadaInput.ButtonCode.MouseLeft, false))
                    {
                        Parent.AddAppealGauge(2);
                        appearlEff.gameObject.SetActive(true);
                        appearlEff.Play();
                        Parent.color.Flash(); // 本体を光らせる
                        Parent.audioSource.PlayOneShot(appealSE);
                        doneAppeal = true;
                    }

                    // アピールチャンスの円を書く
                    if (Parent.Velocity.y >= 0.0f)
                    {
                        circle.Show(Mathf.Abs(Parent.Velocity.y));
                    }
                    else circle.Hide();
                }
                else circle.Hide();
            }
        }
    }
} // namespace MainGame.Actor