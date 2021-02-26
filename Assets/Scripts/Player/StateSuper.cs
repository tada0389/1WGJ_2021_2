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
            // ステートが始まった時に呼ばれるメソッド
            public override void OnStart()
            {

            }

            // ステートが終了したときに呼ばれるメソッド
            public override void OnEnd()
            {

            }

            // 毎フレーム呼ばれる関数
            public override void Proc()
            {

            }
        }
    }
} // namespace MainGame.Actor