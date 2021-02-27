using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 惑星の回転速度と同じ速度で動く通常のオブジェクト
/// </summary>

public class StaticStageObject : BaseStageObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "muteki")
        {
            // 吹き飛ぶ
            BlowedOff();
        }
    }

    // 吹き飛ばされる
    private void BlowedOff()
    {
        Debug.Log("called");
        // y軸方向に＋
        Velocity = new Vector2(-0.5f, 0.5f);
    }
}
