using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTrailController : MonoBehaviour
{
    [SerializeField]
    LineRenderer line;
    [SerializeField]
    float omega;
    List<Vector3> poses = new List<Vector3>();
    [SerializeField]
    int length = 60;
    [SerializeField]
    Transform center;
    [SerializeField]
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < length; i++)
        {
            poses.Add(target.position);
            line.SetPosition(i, target.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        var angleAxis = Quaternion.AngleAxis(omega * Time.time, Vector3.forward);
        Vector3 p = angleAxis * new Vector3(1, 0, 0);
        Debug.DrawLine(Vector3.zero, p, Color.black);
        */
        poses.RemoveAt(length - 1);
        poses.Insert(0, target.position);
        for (int i = 0; i < length; i++)
        {
            var angleAxis = Quaternion.AngleAxis(omega * Time.deltaTime, Vector3.forward);
            poses[i] -= center.position;
            poses[i] = angleAxis * poses[i];
            poses[i] += center.position;
            line.SetPosition(i, poses[i]);
        }
        for (int i = 0; i < length - 1; i++)
        {
            Debug.DrawLine(poses[i], poses[i + 1], Color.black);
        }
        Debug.DrawLine(target.position, center.position, Color.black);
    }
}
