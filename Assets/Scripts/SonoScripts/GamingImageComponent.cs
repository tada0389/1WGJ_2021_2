using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GamingImageComponent : MonoBehaviour
{
    [SerializeField]
    private float color_speed_ = 1.0f;

    private Image image;

    private float hue_;

    private bool isGaming = false;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        hue_ = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGaming) return;

        image.color = Color.HSVToRGB(hue_, 1.0f, 1.0f);
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
        image.color = initColor;
    }
}