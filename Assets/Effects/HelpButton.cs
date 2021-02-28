using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HelpButton : MonoBehaviour
{
    [SerializeField]
    private RectTransform helpRect;

    [SerializeField]
    private RectTransform childTextRect;

    private bool isShowButton = true;

    // Start is called before the first frame update
    void Start()
    {
        childTextRect.DOPunchScale(Vector3.one * 0.2f, 1f, 1, 1).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        if (StageObjectManager.isTitle || StageObjectManager.isDead)
        {
            if (!isShowButton)
            {
                isShowButton = true;
                transform.DOScale(Vector3.one, 0.1f);
            }
        }
        else
        {
            if (isShowButton)
            {
                isShowButton = false;
                transform.DOScale(Vector3.zero, 0.2f);
            }
        }
    }

    public void HelpOpen()
    {
        helpRect.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
    }

    public void HelpClose()
    {
        helpRect.DOScale(Vector3.zero, 0.25f);
    }
}
