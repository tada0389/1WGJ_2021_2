using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteColor : MonoBehaviour
{
    [SerializeField]
    private float flashPower = 0.2f;
    [SerializeField]
    private float flashDuration = 0.4f;

    private Color initColor;

    private SpriteRenderer renderer;

    private bool isFlashing = false;
    private float flashStartTime;

    [SerializeField]
    private TadaLib.GamingComponent moyou;

    private float defaultScale;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        initColor = renderer.color;
        defaultScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFlashing && Time.time - flashStartTime >= flashDuration)
        {
            isFlashing = false;
            renderer.color = initColor;
            moyou.FinishGaming(Color.white);
        }
    }

    // 光らせる
    public void Flash()
    {
        transform.DOPunchScale(Vector3.one * (defaultScale * flashPower), flashDuration);
        isFlashing = true;
        flashStartTime = Time.time;
        //float h, s, v;
        //Color.RGBToHSV(initColor, out h, out s, out v);
        //renderer.color = Color.HSVToRGB(h, Mathf.Min(1.0f, s - flashPower), v);
        moyou.StartGaming();
    }
}
