using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HelpButton : MonoBehaviour
{
    [SerializeField]
    RectTransform helpRect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
