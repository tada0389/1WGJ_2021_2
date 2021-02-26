using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField]
    float amplitude;

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
        elapsedTime %= period;
        transform.localPosition
            += Vector3.up * amplitude * Mathf.Cos(2 * Mathf.PI * elapsedTime / period);

        if (transform.position.y<=-10)
        {
            Destroy(gameObject);
        }
    }
}
