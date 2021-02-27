using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundRotator : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 45.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z + rotateSpeed * Time.deltaTime);
    }
}
