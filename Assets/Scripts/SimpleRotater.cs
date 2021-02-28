using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotater : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 1.0f;

    private bool isRotating = false;

    private float power = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if(isRotating)
            transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z + rotateSpeed * power * Time.deltaTime);   
    }

    public void Rotate(float _power = 1.0f)
    {
        isRotating = true;
        power = _power;
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
