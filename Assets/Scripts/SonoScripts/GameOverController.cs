using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOverController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject homeButton;

    [SerializeField]
    GameObject rankingButton;

    [SerializeField]
    GameObject tweetButton;

    [SerializeField]
    GameObject wholeButton;

    void Start()
    {
        homeButton.transform.localScale = Vector3.zero;
        rankingButton.transform.localScale = Vector3.zero;
        wholeButton.transform.localScale = Vector3.zero;
        tweetButton.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGameOver()
    {
        homeButton.transform.DOScale(Vector3.one, 0.1f);
        rankingButton.transform.DOScale(Vector3.one, 0.1f);
        tweetButton.transform.DOScale(Vector3.one, 0.1f);
        wholeButton.transform.localScale = Vector3.one;
        homeButton.transform.DORotate(Vector3.back * 10, 1f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
        rankingButton.transform.DORotate(Vector3.back * 10, 1f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
        tweetButton.transform.DORotate(Vector3.back * 10, 1f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnHomeClick()
    {
        Time.timeScale = 1f;
        StageObjectManager.isTitle = true;
        StageObjectManager.isDead = false;
        // リロード
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void OnRankingClick()
    {
        // ランキング処理
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(StageObjectManager.highScore);
    }

    public void OnWholeClick()
    {
        Time.timeScale = 1f;
        StageObjectManager.isDead = false;
        // リロード
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }
}
