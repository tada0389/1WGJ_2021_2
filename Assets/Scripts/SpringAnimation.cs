using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject topPlate;
    [SerializeField]
    private GameObject SpringBody;

    // どれくらい沈むか
    [SerializeField]
    private float sinkStrength = 0.5f;

    // 抵抗率
    [SerializeField]
    private float resistibility = 0.1f;


}
