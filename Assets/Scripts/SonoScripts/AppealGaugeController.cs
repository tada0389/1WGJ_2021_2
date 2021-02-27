using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AppealGaugeController : MonoBehaviour
{
    [SerializeField]
    private RectTransform useGaugeRT;

    [SerializeField]
    private Image[] gaugeImages;

    [SerializeField]
    private Image currentRingImage;

    [SerializeField]
    private Image currentBackGroundImage;

    [SerializeField]
    private TextMeshProUGUI currentCountText;

    [SerializeField]
    private Color satisfyBackGroundColor;

    [SerializeField]
    private Color notSatisfyBackGroundColor;

    [SerializeField]
    private Color satisfyTextColor;

    [SerializeField]
    private Color notSatisfyTextColor;

    // Start is called before the first frame update
    void Start()
    {
        useGaugeRT.GetComponent<GamingImageComponent>().StartGaming();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGauge(int val,int maxVal, int needVal)
    {
        int currentCount = val / needVal;
        currentCountText.text = currentCount.ToString();
        if (currentCount < gaugeImages.Length)
        {
            currentRingImage.color = gaugeImages[currentCount].color;
        }
        for (int i = 0; i < gaugeImages.Length; i++)
        {
            if (i < currentCount)
            {
                gaugeImages[i].fillAmount = 0.25f;
                gaugeImages[i].gameObject.SetActive(true);
            }
            else
            {
                gaugeImages[i].gameObject.SetActive(false);
            }
        }

        if (val >= needVal)
        {
            currentCountText.color = satisfyTextColor;
            currentBackGroundImage.color = satisfyBackGroundColor;

            useGaugeRT.gameObject.SetActive(true);
            float angle = 180 - 360 * ((float)val / maxVal - 0.25f);
            useGaugeRT.localEulerAngles = Vector3.forward * angle;
        }
        else
        {
            currentCountText.color = notSatisfyTextColor;
            currentBackGroundImage.color = notSatisfyBackGroundColor;
            useGaugeRT.gameObject.SetActive(false);
            gaugeImages[0].gameObject.SetActive(true);
            gaugeImages[0].fillAmount = (float)val / maxVal;
        }
    }
}
