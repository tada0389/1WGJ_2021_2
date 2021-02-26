using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame
{
    public class PlayerController : BaseStageObject
    {
        [SerializeField]
        private float jumpPower_ = 1.0f;

        private TadaLib.TadaRigidbody trb_;
        [SerializeField]
        private SimpleRotater rotater_;

        private bool IsBoosting = false;
        private bool isReseted = false;

        private void Start()
        {
            trb_ = GetComponent<TadaLib.TadaRigidbody>();
        }

        private void Update()
        {
            if (trb_.RightCollide || trb_.LeftCollide) Reset();

            if (trb_.ButtomCollide) Velocity = new Vector2(Velocity.x, Mathf.Max(0.0f, Velocity.y));

            if (Position.x <= 119.5f && trb_.ButtomCollide)
            {
                Velocity = new Vector2(0.0f, Velocity.y);
                rotater_.enabled = false;
                IsBoosting = false;
            }
            else
            {
                Velocity = new Vector2((IsBoosting)? 1.6f : 1.0f, Velocity.y);
                rotater_.enabled = true;
            }
            if (Input.GetMouseButtonDown(0) && trb_.ButtomCollide)
            {
                Velocity = new Vector2(Velocity.x, Velocity.y + jumpPower_);
            }

            if (Input.GetMouseButtonDown(1) && !trb_.ButtomCollide)
            {
                Velocity = new Vector2(1.6f, Velocity.y);
                IsBoosting = true;
            }
        }

        private void OnDestroy()
        {
            Reset();   
        }

        private void Reset()
        {
            if (isReseted) return;
            isReseted = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}