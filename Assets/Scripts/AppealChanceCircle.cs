using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppealChanceCircle : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;

    private void Start()
    {
        renderer.enabled = false;
    }

    public void Show(float t)
    {
        renderer.enabled = true;
        transform.localScale = Vector3.one * (t * 10.0f + 1.0f);
    }

    public void Hide()
    {
        renderer.enabled = false;
    }
}
