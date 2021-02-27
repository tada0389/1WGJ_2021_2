using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotater : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 1.0f;

    private bool isRotating = false;

    // Update is called once per frame
    void Update()
    {
        if(isRotating)
            transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z + rotateSpeed * Time.deltaTime);   
    }

    public void StartRotate()
    {
        isRotating = true;
    }

    public void StopRotate()
    {
        isRotating = false;
    }
}
