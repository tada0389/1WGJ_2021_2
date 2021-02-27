using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TadaLib;

/// <summary>
/// プレイヤーの死亡状態を管理するステート
/// </summary>

namespace MainGame.Actor
{
    public partial class PlayerController
    {
        [System.Serializable]
        private class StateDead : StateMachine<PlayerController>.StateBase
        {
            [SerializeField]
            private ParticleSystem deadEff;

            private bool sceneLoaded = false;

            // ステートが始まった時に呼ばれるメソッド
            public override void OnStart()
            {
                Parent.Velocity = Vector2.zero;
                Parent.Accel = Vector2.zero;

                deadEff.gameObject.SetActive(true);
                deadEff.Play();

                // カメラ揺らす
                CameraSpace.CameraShaker.Shake(0.2f, 0.5f);
            }

            // ステートが終了したときに呼ばれるメソッド
            public override void OnEnd()
            {

            }

            // 毎フレーム呼ばれる関数
            public override void Proc()
            {
                if (Timer > 3.0f && !sceneLoaded)
                {
                    sceneLoaded = true;
                    // 再リロード
                    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                }
            }
        }
    }
} // namespace MainGame.Actor