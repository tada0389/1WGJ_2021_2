using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TadaLib;

/// <summary>
/// プレイヤーの歩行状態を管理するステート
/// </summary>

namespace MainGame.Actor
{
    public partial class PlayerController
    {
        [System.Serializable]
        private class StateWalk : StateMachine<PlayerController>.StateBase
        {
            // ステートが始まった時に呼ばれるメソッド
            public override void OnStart()
            {
                Parent.Velocity = new Vector2(0.5f, 0.0f);
                Parent.Accel = this.Accel;

                // 回転
                Parent.rotater.StartRotate();
            }

            // ステートが終了したときに呼ばれるメソッド
            public override void OnEnd()
            {

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

                // 自身の左右が壁に当たったら死亡
                if (Parent.trb.RightCollide || Parent.trb.LeftCollide)
                {
                    ChangeState((int)eState.Dead);
                    return;
                }

                // 接地してなかったら落下へ
                if (!Parent.trb.ButtomCollide)
                {
                    ChangeState((int)eState.Fall);
                    return;
                }

                // ジャンプ
                if (Parent.input.GetButtonDown(TadaInput.ButtonCode.MouseLeft, true))
                {
                    ChangeState((int)eState.Jump);
                    return;
                }
            }
        }
    }
} // namespace MainGame.Actor