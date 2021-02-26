using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 惑星の表面上に配置されるオブジェクト
/// </summary>

public class BaseStageObject : MonoBehaviour
{
    // 座標 (x = [0,360), y = [0, ∞))
    public Vector2 Position { private set; get; }

    // 速度
    [field: SerializeField]
    public Vector2 Velocity { protected set; get; }

    // 加速度
    [field: SerializeField]
    public Vector2 Accel { protected set; get; }

    [SerializeField]
    private Vector2 maxAbsSpeed = new Vector2(10.0f, 0.5f);

    public bool Destoryed { private set; get; }

    public void Init(Vector2 pos, Vector2 center)
    {
        Position = pos;
        Destoryed = false;

        // 正しい座標へと変換
        transform.position = center + new Vector2(Mathf.Cos(Position.x * Mathf.Deg2Rad), Mathf.Sin(Position.x * Mathf.Deg2Rad)) * Position.y;

        // 角度も合わせる
        transform.eulerAngles = new Vector3(0f, 0f, Position.x - 90.0f);
    }

    // 座標を更新する centerは惑星の中心座標
    public void Proc(Vector2 center, float rotateSpeed)
    {
        // x軸の移動
        float newPosX = Position.x - Velocity.x * rotateSpeed + rotateSpeed;
        while (newPosX < 0f) newPosX += 360.0f;
        while (newPosX > 360f) newPosX -= 360.0f;

        // y軸の移動
        //float newPosY = Position.y + Velocity.y * rotateSpeed;
        float newPosY = Vector2.Distance(transform.position, center) + Velocity.y * rotateSpeed; // 最新のy座標を使う
        //if(newPosY < 7.0f) // とりあえずこうなったら接置判定
        //{
        //    Speed = new Vector2(Speed.x, 0.0f);
        //    newPosY = 7.0f;
        //}

        Position = new Vector2(newPosX, newPosY);

        // 正しい座標へと変換
        transform.position = center + new Vector2(Mathf.Cos(Position.x * Mathf.Deg2Rad), Mathf.Sin(Position.x * Mathf.Deg2Rad)) * Position.y;

        // 角度も合わせる
        transform.eulerAngles = new Vector3(0f, 0f, Position.x - 90.0f);

        Velocity += Accel * rotateSpeed / 45.0f;
        Velocity = new Vector2(Mathf.Clamp(Velocity.x, -maxAbsSpeed.x, maxAbsSpeed.x), Mathf.Clamp(Velocity.y, -maxAbsSpeed.y, maxAbsSpeed.y));

        if (Position.x >= 200.0f && Position.x <= 250.0f) Destoryed = true;
    }
}
