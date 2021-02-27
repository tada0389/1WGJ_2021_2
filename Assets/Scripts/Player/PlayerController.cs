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

        private TadaLib.TadaRigidbody2D trb;

        private BasePlayerInput input;

        private int appealGauge = 50;

        [SerializeField]
        private int needAppealGauge = 10;

        [SerializeField]
        private int addAppealGaugeAmount = 5;

        [SerializeField]
        private int maxAppealGauge = 100;

        [SerializeField]
        private SimpleRotater rotater;

        [SerializeField]
        private GameObject appealUI;

        private Image appealGaugeUI;
        private Color gaugeSatisfyColor;
        private Color gaugeNotSatisfyColor;

        private AudioSource audioSource;

        private SpriteColor color;

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

            trb = GetComponent<TadaLib.TadaRigidbody2D>();
            input = GetComponent<BasePlayerInput>();
            audioSource = GetComponent<AudioSource>();
            color = GetComponent<SpriteColor>();

            gaugeSatisfyColor = appealUI.transform.Find("WholeGuide").GetComponent<Image>().color;
            gaugeNotSatisfyColor = appealUI.transform.Find("NeedGuide").GetComponent<Image>().color;
            appealUI.transform.Find("NeedGuide").GetComponent<Image>().fillAmount = (float)needAppealGauge / maxAppealGauge;
            appealGaugeUI = appealUI.transform.Find("Gauge").GetComponent<Image>();
            UpdateAppealGaugeUI();
        }

        private void Update()
        {
            // 状態を更新する
            stateMachine.Proc();
            string s = "state : " + stateMachine.ToString() + "\n" + trb.LeftCollide.ToString() + " " + trb.RightCollide.ToString();
            KoitanDebug.DisplayBox(s, this);
        }

        // 無敵状態になれるか
        private bool MutekiRequest()
        {
            return appealGauge >= needAppealGauge;
        }

        private void UseAppealGauge()
        {
            appealGauge = Mathf.Max(appealGauge - needAppealGauge, 0);
            UpdateAppealGaugeUI();
        }

        private void AddAppealGauge()
        {
            appealGauge = Mathf.Min(appealGauge + addAppealGaugeAmount, maxAppealGauge);
            UpdateAppealGaugeUI();
        }

        private void UpdateAppealGaugeUI()
        {
            appealGaugeUI.fillAmount = (float)appealGauge / maxAppealGauge;
            appealGaugeUI.color = appealGauge >= needAppealGauge ? gaugeSatisfyColor : gaugeNotSatisfyColor;
        }

        //public void AudioPlay(AudioClip clip)
        //{
        //    audioSource.PlayOneShot(clip);
        //}
    }
} // namespace Main.Actor