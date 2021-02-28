using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RankingButton : MonoBehaviour
{
    [SerializeField]
    Vector3 targetScale;
    Vector3 defaultScale;
    // Start is called before the first frame update
    void Start()
    {
        defaultScale = transform.localScale;

        // Type == Number の場合
        // naichilab.RankingLoader.Instance.SendScoreAndShowRanking(1200.10);

        // Type == Time の場合
        /*
        var millsec = 123456;
        var timeScore = new System.TimeSpan(0, 0, 0, 0, millsec);
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(timeScore);
        */
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEnter()
    {
        transform.DOScale(targetScale, 0.2f);
    }

    public void OnExit()
    {
        transform.DOScale(defaultScale, 0.2f);
    }

    public void SendRanking()
    {
        //transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f).SetRelative();
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(PlayerPrefs.GetFloat("highscore", 0));
    }
}
