using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailTest : MonoBehaviour
{
    [SerializeField]
    LineRenderer line;
    List<Vector3> poses = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            poses.Add(Vector3.zero);
            line.SetPosition(i, Vector3.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
