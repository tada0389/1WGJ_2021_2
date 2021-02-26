using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPattern
{
    [SerializeField]
    private List<int> indicies;

    private int cur = 0;

    public int Next()
    {
        UnityEngine.Assertions.Assert.IsTrue(indicies.Count != 0, "足りない");
        if (cur == indicies.Count) return -1;
        return indicies[cur++];
    }

    public void Reset()
    {
        cur = 0;
    }
}

public class StageObjectManager : MonoBehaviour
{
    // 回転速度 per sec
    [SerializeField]
    private float rotateSpeed = 45.0f;

    // オブジェクトを生成する間隔角度
    [SerializeField]
    private float spawnIntervalDegree = 30.0f;

    // 生成するオブジェクトのプレハブリスト
    [SerializeField]
    private List<BaseStageObject> objectPrefabs;

    // 現在ステージにいるオブジェクト
    [SerializeField]
    private List<BaseStageObject> objects;

    [SerializeField]
    private List<ObjectPattern> patterns;

    private float rotateSum = 0.0f;

    private ObjectPattern curPattern;

    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;

    [SerializeField]
    private TMPro.TextMeshProUGUI highScoreText;

    private float score = 0.0f;
    private static float highScore = 0.0f;

    private void Start()
    {
        foreach(var obj in objects)
        {
            obj.Init(new Vector2(120f, 7.0f), transform.position);
        }

        curPattern = patterns[Random.Range(0, patterns.Count)];
        curPattern.Reset();
    }

    private void Update()
    {
        // オブジェクトを生成
        rotateSum += rotateSpeed * Time.deltaTime;
        score += rotateSpeed * Time.deltaTime;
        scoreText.text = score.ToString("F2") + "m";

        highScore = Mathf.Max(highScore, score);
        highScoreText.text = highScore.ToString("F2") + "m";

        if (rotateSum >= spawnIntervalDegree)
        {
            rotateSum -= spawnIntervalDegree;

            int id = curPattern.Next();
            while (id == -1)
            {
                // 終端に到達したので新しいパターンを使う
                curPattern = patterns[Random.Range(0, patterns.Count)];
                curPattern.Reset();
                id = curPattern.Next();
            }
            if (id >= 0)
            {
                var obj = (BaseStageObject)Instantiate(objectPrefabs[id]);
                obj.Init(new Vector2(270.0f, 0.0f), transform.position);
                objects.Add(obj);
            }
        }

        // 回転
        transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z + rotateSpeed * Time.deltaTime);

        foreach(var obj in objects)
        {
            obj.Proc(transform.position, rotateSpeed * Time.deltaTime);
            if (obj.Destoryed) Destroy(obj.gameObject);
        }
        objects.RemoveAll(p => p.Destoryed);
    }
}
