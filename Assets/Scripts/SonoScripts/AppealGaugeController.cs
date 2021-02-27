using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class AppealGaugeController : MonoBehaviour
{
    [SerializeField]
    private Image useGaugeImage;

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
        useGaugeImage.GetComponent<GamingImageComponent>().StartGaming();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGauge(int val,int maxVal, int needVal)
    {
        int currentCount = val / needVal;
        if (currentCountText.text != currentCount.ToString())
        {
            currentCountText.text = currentCount.ToString();
            currentCountText.rectTransform.DOPunchScale(Vector3.one * 0.3f, 0.2f, 1, 0);
        }

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

            bool prevActive = useGaugeImage.gameObject.activeSelf;
            useGaugeImage.gameObject.SetActive(true);
            float angle = 180 - 360 * ((float)val / maxVal - 0.25f);
            if (prevActive)
            {
                useGaugeImage.rectTransform.DORotate(Vector3.forward * angle, 0.5f);
                useGaugeImage.rectTransform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 1, 0);
            }
            else
            {
                useGaugeImage.fillAmount = 0.25f;
                useGaugeImage.rectTransform.localEulerAngles = Vector3.forward * angle;
                useGaugeImage.rectTransform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 1, 0);
            }
        }
        else
        {
            currentCountText.color = notSatisfyTextColor;
            currentBackGroundImage.color = notSatisfyBackGroundColor;
            bool prevUseGauge = useGaugeImage.gameObject.activeSelf;
            if (prevUseGauge)
            {
                useGaugeImage.DOFillAmount(0, 0.5f).OnComplete(()=>
                {
                    useGaugeImage.gameObject.SetActive(false);
                });
            }
            gaugeImages[0].gameObject.SetActive(true);
            gaugeImages[0].DOFillAmount((float)val / maxVal, 0.5f);
            gaugeImages[0].rectTransform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 1, 0);
        }
    }
}
