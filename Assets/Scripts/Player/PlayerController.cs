using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TadaLib;

namespace MainGame.Actor
{
    public partial class PlayerController : BaseStageObject
    {

        // プレイヤーのステート一覧
        private enum eState
        {
            Walk, // 通常時
            Jump, // ジャンプ時
            Fall, // 落下時
            Super, // 無敵時
            Dead, // 死亡時
        }

        private StateMachine<PlayerController> stateMachine;

        #region state class
        // 各ステートのインスタンス これで各ステートのフィールドをInspectorでいじれる 神
        [SerializeField]
        private StateWalk stateWalk;
        [SerializeField]
        private StateJump stateJump;
        [SerializeField]
        private StateFall stateFall;
        [SerializeField]
        private StateSuper stateSuper;
        [SerializeField]
        private StateDead stateDead;
        #endregion

        private TadaLib.TadaRigidbody trb;
        [SerializeField]
        private SimpleRotater rotater;

        private void Start()
        {
            // ステートマシンのメモリ確保 自分自身を渡す
            stateMachine = new StateMachine<PlayerController>(this);
            // ステート登録
            stateMachine.AddState((int)eState.Walk, stateWalk);
            stateMachine.AddState((int)eState.Jump, stateJump);
            stateMachine.AddState((int)eState.Fall, stateFall);
            stateMachine.AddState((int)eState.Super, stateSuper);
            stateMachine.AddState((int)eState.Dead, stateDead);

            // 始めのステートを設定
            stateMachine.SetInitialState((int)eState.Fall);

            trb = GetComponent<TadaLib.TadaRigidbody>();
        }

        private void Update()
        {
            // 状態を更新する
            stateMachine.Proc();
        }
    }
} // namespace Main.Actor