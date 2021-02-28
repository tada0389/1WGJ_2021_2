using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimController : MonoBehaviour
{
    [SerializeField]
    TMProAnimator titleText;
    [SerializeField]
    float interval = 3f;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        titleText.StartAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t >= interval)
        {
            t -= interval;
            titleText.StopAnimation();
            titleText.StartAnimation();
        }
    }
}
