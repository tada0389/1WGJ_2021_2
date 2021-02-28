using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotator : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 45.0f;

    private bool isAnimationStop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StageObjectManager.isDead)
        {
            if (!isAnimationStop)
            {
                isAnimationStop = true;
                GetComponent<Animator>().enabled = false;
                GetComponent<BackGroundObjectGenerator>().enabled = false;
            }
            return;
        }

        rotateSpeed = StageObjectManager.staticRotateSpeed * 0.3f;
        transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z + rotateSpeed * Time.deltaTime);
    }
}
