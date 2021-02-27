using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 惑星の回転速度と同じ速度で動く通常のオブジェクト
/// </summary>

public class StaticStageObject : BaseStageObject
{
    [SerializeField]
    private ParticleSystem breakEffPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "muteki")
        {
            // 吹き飛ぶ
            BlowedOff();
            var cols = transform.GetChild(0).GetComponents<Collider2D>(); // 無理やりだけど
            foreach(var col in cols)
            {
                //ふっとんだとき回転させる
                col.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 3600f), 1f, RotateMode.FastBeyond360);
                col.enabled = false;
            }
        }
    }

    // 吹き飛ばされる
    private void BlowedOff()
    {
        Instantiate(breakEffPrefab, transform.GetChild(0).position, Quaternion.identity);
        // y軸方向に＋
        Velocity = new Vector2(-0.5f, 0.5f);
    }
}
