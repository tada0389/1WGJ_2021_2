using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TadaLib;

/// <summary>
/// プレイヤーの落下状態を管理するステート
/// </summary>

namespace MainGame.Actor
{
    public partial class PlayerController
    {
        [System.Serializable]
        private class StateFall : StateMachine<PlayerController>.StateBase
        {
            // コヨーテタイム
            [SerializeField]
            private float rejumpEnableDuration = 0.2f;

            // ステートが始まった時に呼ばれるメソッド
            public override void OnStart()
            {
                Parent.Accel = this.Accel;
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

                // 接地してたらwalkステートへ
                if (Parent.trb.ButtomCollide)
                {
                    ChangeState((int)eState.Walk);
                    return;
                }

                // 落下直後は再ジャンプできる
                if(Input.GetMouseButtonDown(0) && PrevStateId == (int)eState.Walk && Timer <= rejumpEnableDuration)
                {
                    ChangeState((int)eState.Jump);
                    return;
                }
            }
        }
    }
} // namespace MainGame.Actor