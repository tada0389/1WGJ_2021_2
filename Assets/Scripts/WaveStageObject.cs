using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStageObject : BaseStageObject
{
    [SerializeField]
    private Vector2 yMoveRange = new Vector2(0.0f, 3.0f);

    [SerializeField]
    private float movePeriod = 1.0f;

    private float time = 0.0f;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        Velocity = new Vector2(Velocity.x, Mathf.Sin(time * Mathf.PI / movePeriod) * yMoveRange.y);
    }
}
