using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestoryObject : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 2.0f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(lifeTime * Time.timeScale);

        Destroy(gameObject);
    }
}
