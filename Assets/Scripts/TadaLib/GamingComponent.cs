using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TadaLib
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GamingComponent : MonoBehaviour
    {
        [SerializeField]
        private float color_speed_ = 1.0f;

        private SpriteRenderer renderer_;

        private float hue_;

        private bool isGaming = false;

        // Start is called before the first frame update
        void Start()
        {
            renderer_ = GetComponent<SpriteRenderer>();
            hue_ = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isGaming) return;

            renderer_.color = Color.HSVToRGB(hue_, 1.0f, 1.0f);
            hue_ += color_speed_ * Time.deltaTime;
            while (hue_ >= 1.0f) hue_ -= 1.0f;
        }

        public void StartGaming()
        {
            isGaming = true;
        }

        public void FinishGaming(Color initColor)
        {
            isGaming = false;
            renderer_.color = initColor;
        }
    }
}