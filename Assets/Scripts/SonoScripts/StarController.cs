using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField]
    float period;

    float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = Random.Range(0, period);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= period)
        {
            transform.localEulerAngles += Vector3.forward * 60;
            elapsedTime = 0;
        }

        if (transform.position.y <= -10)
        {
            Destroy(gameObject);
        }
    }
}
