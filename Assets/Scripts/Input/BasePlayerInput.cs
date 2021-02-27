using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 入力情報を管理するクラス
/// </summary>

namespace TadaInput
{
    public enum ButtonCode
    {
        MouseLeft = 0,
        MouseRight = 1,
    }

    public abstract class BasePlayerInput : MonoBehaviour
    {
        public bool ActionEnabled = true;

        // 入力状態をリセットする
        public abstract void Reset();

        /// <summary>
        /// 指定したボタンが入力されたかを取得する
        /// </summary>
        /// <param name="code">ボタン</param>
        /// <returns></returns>
        public abstract bool GetButtonDown(ButtonCode code, bool flag = true);
        /// <summary>
        /// 指定したボタンが入力されているかを取得する
        /// </summary>
        /// <param name="code">ボタン</param>
        /// <returns></returns>
        public abstract bool GetButton(ButtonCode code);

        /// <summary>
        /// 指定したボタンの入力が離されたかを取得する
        /// </summary>
        /// <param name="code">ボタン</param>
        /// <returns></returns>
        public abstract bool GetButtonUp(ButtonCode code);
    }
}