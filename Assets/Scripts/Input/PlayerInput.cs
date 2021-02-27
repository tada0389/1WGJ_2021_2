using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 入力情報を管理するクラス
/// 先行入力などに対応している
/// 先行入力は，ジャンプとダッシュのみ対応
/// また，Updateで受け取った入力をFixedUpdateへの入力に変換している
/// 
/// 先行入力に関して
/// ・タイムスケールが通常とは異なった時はどうなるか
/// -> タイムスケールを考慮せず，常にタイムスケールが1のときと同じ挙動をする
/// 
/// </summary>

namespace TadaInput
{
    public class PlayerInput : BasePlayerInput
    {
        // 先行入力の受付時間
        [SerializeField]
        private float persuadeTime = 0.06f;

        private Queue<float> jumpBuff;

        private void Awake()
        {
            jumpBuff = new Queue<float>();
        }

        private void Update()
        {
            // 入力もないことにする
            if (Time.timeScale < 1e-6 || !ActionEnabled)
            {
                Reset();
                return;
            }

            // 制限時間を超えているものはあるか
            while (jumpBuff.Count >= 1)
            {
                if (Time.unscaledTime - jumpBuff.Peek() > persuadeTime) jumpBuff.Dequeue();
                else break;
            }

            if (Input.GetMouseButton((int)ButtonCode.MouseLeft)) jumpBuff.Enqueue(Time.unscaledTime);
        }

        public override void Reset()
        {
            jumpBuff.Clear();
        }

        public override bool GetButton(ButtonCode code)
        {
            if (!ActionEnabled) return false;
            return Input.GetMouseButton((int)code);
        }

        public override bool GetButtonDown(ButtonCode code, bool usePreceding = false)
        {
            if (!ActionEnabled) return false;
            if (usePreceding && code == ButtonCode.MouseLeft)
            {
                bool res = (jumpBuff.Count >= 1);
                if (res) jumpBuff.Dequeue();
                return res;
            }

            return Input.GetMouseButtonDown((int)code);
        }

        public override bool GetButtonUp(ButtonCode code)
        {
            if (!ActionEnabled) return false;
            return Input.GetMouseButtonUp((int)code);
        }
    }
}