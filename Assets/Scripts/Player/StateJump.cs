using System.Collections;
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

            // ステートが始まった時に呼ばれるメソッド
            public override void OnStart()
            {
                Parent.Accel = this.Accel;
                Parent.Velocity = new Vector2(Parent.Velocity.x, jumpPower);
            }

            // ステートが終了したときに呼ばれるメソッド
            public override void OnEnd()
            {

            }

            // 毎フレーム呼ばれる関数
            public override void Proc()
            {
                // 自身の左右が壁に当たったら死亡
                if (Parent.trb.RightCollide || Parent.trb.LeftCollide)
                {
                    ChangeState((int)eState.Dead);
                    return;
                }

                // 接地してたらwalkステートへ　ただ、一定時間は無視する(ジャンプできなくなるので)
                if (Parent.trb.ButtomCollide && Timer >= 0.4f)
                {
                    ChangeState((int)eState.Walk);
                    return;
                }

                // ジャンプボタン長押し
                if (Input.GetMouseButton(0) && Timer <= jumpEnableDuration)
                {
                    Parent.Velocity = new Vector2(Parent.Velocity.x, jumpPower);
                }

                // ジャンプボタン＋頂点付近ならアピール
                if(Input.GetMouseButtonDown(0) && Mathf.Abs(Parent.Velocity.y) <= appealEnableSpeed)
                {
                    Debug.Log("appeal");
                }
            }
        }
    }
} // namespace MainGame.Actor