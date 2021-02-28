using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject topPlate;
    [SerializeField]
    private GameObject springBody;

    // どれくらい沈むか
    [SerializeField]
    private float sinkStrength = 0.5f;

    // 抵抗率
    [SerializeField]
    private float resistibility = 0.1f;

    // 反復の周期
    [SerializeField]
    private float period = 0.4f;

    private bool isAnimating;
    private float animStartTime;

    private float topPlateDefaultPosY;
    private float springDefaultPosY;
    private float springDefaultScaleY;

    private void Start()
    {
        isAnimating = false;
        topPlateDefaultPosY = topPlate.transform.localPosition.y;
        springDefaultPosY = springBody.transform.localPosition.y;
        springDefaultScaleY = springBody.transform.localScale.y;

    }

    private void Update()
    {
        if (!isAnimating) return;

        float t = Time.time - animStartTime;
        int repeatNum = (int)(t / period) * 2;
        float strength = sinkStrength * Mathf.Pow(resistibility, (float)repeatNum); // やや重いかも

        float newPos = Mathf.Sin(t * 2.0f / period * Mathf.PI) * strength;

        // 上の板
        var platePos = topPlate.transform.localPosition;
        platePos.y = topPlateDefaultPosY + newPos * 0.5f;
        topPlate.transform.localPosition = platePos;

        // ばね
        var springScale = springBody.transform.localScale;
        springScale.y = springDefaultScaleY + newPos * 0.1f;
        springBody.transform.localScale = springScale;

        var springPos = springBody.transform.localPosition;
        springPos.y = springDefaultPosY + newPos * 0.25f;
        springBody.transform.localPosition = springPos;
    }

    public void StartAnim()
    {
        isAnimating = true;
        animStartTime = Time.time;
    }
}
