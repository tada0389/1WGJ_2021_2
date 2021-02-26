using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject cloudOrigin;

    [SerializeField]
    float minHeight;

    [SerializeField]
    float maxHeight;

    [SerializeField]
    float generationSpan;

    [SerializeField]
    float generationRate;

    float prevTime;

    void Start()
    {
        prevTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        if (currentTime - prevTime >= generationSpan)
        {
            if (Random.value < generationRate)
            {
                var cloud = Instantiate(cloudOrigin);
                cloud.transform.parent = transform;
                cloud.transform.position
                    = Vector3.right * Random.Range(minHeight, maxHeight);
                cloud.transform.eulerAngles = Vector3.back * 90;
                cloud.transform.position += transform.position;
            }
            prevTime = currentTime;
        }
    }
}
