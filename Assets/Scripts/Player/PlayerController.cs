using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TadaLib;
using KoitanLib;
using TadaInput;
using UnityEngine.UI;

namespace MainGame.Actor
{
    public partial class PlayerController : BaseStageObject
    {

        // プレイヤーのステート一覧
        private enum eState
        {
            Walk, // 通常時
            Jump, // ジャンプ時
            SpringJump, // ばねジャンプ時
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
        private StateSpringJump stateSpringJump;
        [SerializeField]
        private StateFall stateFall;
        [SerializeField]
        private StateSuper stateSuper;
        [SerializeField]
        private StateDead stateDead;
        #endregion

        private TadaLib.TadaRigidbody2D trb;

        private BasePlayerInput input;

        private int appealGauge = 0;

        [SerializeField]
        private int needAppealGauge = 25;

        [SerializeField]
        private int addAppealGaugeAmount = 5;

        [SerializeField]
        private int maxAppealGauge = 100;

        [SerializeField]
        private SimpleRotater rotater;

        [SerializeField]
        private AppealGaugeController appealGaugeController;

        [SerializeField]
        private GameOverController gameOverController;

        [SerializeField]
        private AudioClip gaugeSE;

        private AudioSource audioSource;

        private SpriteColor color;

        private void Start()
        {
            // ステートマシンのメモリ確保 自分自身を渡す
            stateMachine = new StateMachine<PlayerController>(this);
            // ステート登録
            stateMachine.AddState((int)eState.Walk, stateWalk);
            stateMachine.AddState((int)eState.Jump, stateJump);
            stateMachine.AddState((int)eState.SpringJump, stateSpringJump);
            stateMachine.AddState((int)eState.Fall, stateFall);
            stateMachine.AddState((int)eState.Super, stateSuper);
            stateMachine.AddState((int)eState.Dead, stateDead);

            // 始めのステートを設定
            stateMachine.SetInitialState((int)eState.Fall);

            trb = GetComponent<TadaLib.TadaRigidbody2D>();
            input = GetComponent<BasePlayerInput>();
            audioSource = GetComponent<AudioSource>();
            color = GetComponent<SpriteColor>();

            appealGaugeController.SetGauge(appealGauge, maxAppealGauge, needAppealGauge);
        }

        private void Update()
        {
            // 状態を更新する
            stateMachine.Proc();
            //string s = "state : " + stateMachine.ToString() + "\n" + trb.LeftCollide.ToString() + " " + trb.RightCollide.ToString();
            //KoitanDebug.DisplayBox(s, this);
        }

        // 無敵状態になれるか
        private bool MutekiRequest()
        {
            return appealGauge >= needAppealGauge || StageObjectManager.isTitle;
        }

        private void UseAppealGauge()
        {
            appealGauge = Mathf.Max(appealGauge - needAppealGauge, 0);
            appealGaugeController.SetGauge(appealGauge, maxAppealGauge, needAppealGauge);
        }

        private void AddAppealGauge(int rate = 1)
        {
            //ゲージの本数増えたらSE鳴らす(Zakky)
            {
                int nextPowerNum = Mathf.Min((appealGauge + addAppealGaugeAmount * rate) / needAppealGauge, maxAppealGauge / needAppealGauge);
                int nowPowerNum = appealGauge / needAppealGauge;
                if (nextPowerNum > nowPowerNum)
                {
                    audioSource.PlayOneShot(gaugeSE);
                }
            }

            appealGauge = Mathf.Min(appealGauge + addAppealGaugeAmount * rate, maxAppealGauge);
            appealGaugeController.SetGauge(appealGauge, maxAppealGauge, needAppealGauge);
        }

        // 殺す 強制的に死亡へ
        public void DoKill()
        {
            if(stateMachine.CurrentStateId != (int)eState.Dead)
                stateMachine.ChangeState((int)eState.Dead);
        }

        // 強制的にばねジャンプへ
        public void DoSpringJump()
        {
            int id = stateMachine.CurrentStateId;
            if (id != (int)eState.Dead && id != (int)eState.Super)
            {
                stateMachine.ChangeState((int)eState.SpringJump);
            }
        }
    }
} // namespace Main.Actor