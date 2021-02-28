using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    GameObject titleText;

    [SerializeField]
    GameObject wholeButton;

    [SerializeField]
    GameObject mainUI;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Vector3 firstCameraPos;

    [SerializeField]
    Vector3 mainCameraPos;

    [SerializeField]
    float firstCameraSize;

    [SerializeField]
    float mainCameraSize;

    private CameraSpace.MultipleTargetCamera multipleTargetCamera;

    // Start is called before the first frame update
    void Start()
    {
        multipleTargetCamera = mainCamera.GetComponent<CameraSpace.MultipleTargetCamera>();

        if (StageObjectManager.isTitle)
        {
            mainCamera.transform.position = firstCameraPos;
            mainCamera.orthographicSize = firstCameraSize;
            multipleTargetCamera.enabled = false;
            mainUI.SetActive(false);
        }
        else
        {
            titleText.SetActive(false);
            wholeButton.SetActive(false);
            mainCamera.transform.position = mainCameraPos;
            mainCamera.orthographicSize = mainCameraSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWholeClick()
    {
        StageObjectManager.isTitle = false;
        titleText.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
        {
            titleText.SetActive(false);
        });
        wholeButton.SetActive(false);
        mainCamera.transform.DOMove(mainCameraPos, 1f).OnComplete(()=>
        {
            multipleTargetCamera.enabled = true;
            mainUI.SetActive(true);
        });
        mainCamera.DOOrthoSize(mainCameraSize, 1f);
    }
}
