using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotater : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z + rotateSpeed * Time.deltaTime);   
    }
}
