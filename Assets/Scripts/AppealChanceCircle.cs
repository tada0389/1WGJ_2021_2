using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppealChanceCircle : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;

    Color iniCol;
    [SerializeField]
    Color appealCol;

    private void Start()
    {
        iniCol = renderer.color;

        renderer.enabled = false;
    }

    public void Show(float t)
    {
        renderer.enabled = true;
        transform.localScale = Vector3.one * (t * 10.0f + 1.0f);
    }

    public void Hide()
    {
        IniColorChenge();
        renderer.enabled = false;
    }

    public void AppealColorChenge()
    {
        renderer.color = appealCol;
    }

    private void IniColorChenge() 
    {
        renderer.color = iniCol;
    }
}
