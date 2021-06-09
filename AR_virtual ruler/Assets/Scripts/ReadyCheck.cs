using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ReadyCheck : MonoBehaviour
{
    /*
     * 캔버스 내의 오브젝트들 온오프, 액티브 처리
     */
    [SerializeField] private Text readyText; //평면인식중 대기 텍스트
    [SerializeField] private Toggle readyToggle;
    [SerializeField] private Button okBtn;
    [SerializeField] private Button cancelBtn;

    private ARMainScript mainscripts;

    public bool BigPlaneToggle
    {
        get => readyToggle.isOn;
        set
        {
            readyToggle.isOn = value;
            CheckIfAllAreTrue();
        }
    }

    private void OnEnable()
    {
        mainscripts = FindObjectOfType<ARMainScript>();
        mainscripts.OnBigPlaneFound += () => BigPlaneToggle = true;
    }

    private void OnDisable()
    {
        mainscripts.OnBigPlaneFound -= () => BigPlaneToggle = true;
    }

    private void CheckIfAllAreTrue()
    {
        if (BigPlaneToggle)
        {
            okBtn.interactable = true;
            cancelBtn.interactable = true;
            readyText.gameObject.SetActive(false);
            readyToggle.gameObject.SetActive(false);
        }
    }


    void Awake()
    {
        //arPlaneManager = mainscripts.arPlaneManager;
        //arPlanes = mainscripts.arPlanes; //추가할 평면
    }
    
}
