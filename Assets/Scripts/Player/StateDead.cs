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

            [SerializeField]
            private AudioClip deadSE;

            [SerializeField]
            private List<GameObject> destoryObjects;

            private Timer timer;
            private bool isGameOver;

            // ステートが始まった時に呼ばれるメソッド
            public override void OnStart()
            {
                Parent.Velocity = Vector2.zero;
                Parent.Accel = Vector2.zero;

                deadEff.gameObject.SetActive(true);
                deadEff.Play();

                Parent.audioSource.PlayOneShot(deadSE);

                // カメラ揺らす
                CameraSpace.CameraShaker.Shake(0.2f, 0.5f);

                Time.timeScale = 0.7f;
                StageObjectManager.isDead = true;
                timer = new Timer(0.5f);
                isGameOver = false;

                foreach (var obj in destoryObjects) obj.SetActive(false);
                Parent.color.Hide();

                PlayerPrefs.SetFloat("highscore", StageObjectManager.highScore);
            }

            // ステートが終了したときに呼ばれるメソッド
            public override void OnEnd()
            {

            }

            // 毎フレーム呼ばれる関数
            public override void Proc()
            {
                if (!isGameOver&& timer.IsTimeout())
                {
                    Parent.gameOverController.StartGameOver();
                    isGameOver = true;
                }
            }
        }
    }
} // namespace MainGame.Actor