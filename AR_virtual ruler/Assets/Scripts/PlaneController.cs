using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private Button _onoffBtn; //화면 onoff 버튼

    //private static ARMainScript mainscript;

    //private Toggle verticalTo = mainscript._verticalToggle; //수직
    //private Toggle hotizontalTo = mainscript._horizontalToggle; //수평

    public Toggle _verticalToggle; //수직
    public Toggle _horizontalToggle; //수평

    public ARPlaneManager _arPlaneManager;
    //private ARPlaneManager planeManager = mainscript._arPlaneManager;

    //plane on/off 버튼 클릭 횟수
    public int clickNum = 0;


    void Awake()
    {
        //mainscript = GameObject.Find("MainSystem").GetComponent<ARMainScript>();
    }

    private void Update()
    {
        _verticalToggle.onValueChanged.AddListener(PlaneToggleCheck);
        _horizontalToggle.onValueChanged.AddListener(PlaneToggleCheck);
    }


    //plane on/off 버튼 클릭
    public void PlaneOnOff()
    {
        clickNum += 1;
        if (clickNum % 2 == 1)
        {
            SetAllPlanesActive(false);
            _onoffBtn.GetComponent<Image>().sprite = Resources.Load("switch-on", typeof(Sprite)) as Sprite;
        }
        else
        {
            SetAllPlanesActive(true);
            _onoffBtn.GetComponent<Image>().sprite = Resources.Load("switch-off", typeof(Sprite)) as Sprite;
        }
    }

    void SetAllPlanesActive(bool value)
    {
        foreach (var plane in _arPlaneManager.trackables)
            plane.gameObject.SetActive(value);
    }

    //설정탭 평면인식 토글
    public void PlaneToggleCheck(bool _bool)
    {
        if (_verticalToggle.isOn && _horizontalToggle.isOn) //둘다 온
        {
            _arPlaneManager.detectionMode = (PlaneDetectionMode)3; //everything
            foreach (var plane in _arPlaneManager.trackables)
                plane.gameObject.SetActive(true);
        }
        else if (_verticalToggle.isOn && !_horizontalToggle.isOn) //수직만 온
        {
            _arPlaneManager.detectionMode = PlaneDetectionMode.Vertical; //2
            foreach (var plane in _arPlaneManager.trackables)
            {
                if (plane.alignment == PlaneAlignment.Vertical)
                {
                    // Disable the entire GameObject.
                    plane.gameObject.SetActive(true);
                }
                else
                {
                    plane.gameObject.SetActive(false);
                }
            }
        }
        else if (!_verticalToggle.isOn && _horizontalToggle.isOn) //수평만 온
        {
            _arPlaneManager.detectionMode = PlaneDetectionMode.Horizontal; //1
            foreach (var plane in _arPlaneManager.trackables)
            {
                if ((plane.alignment == PlaneAlignment.HorizontalUp) || (plane.alignment == PlaneAlignment.HorizontalDown))
                {
                    // Disable the entire GameObject.
                    plane.gameObject.SetActive(true);
                }
                else
                {
                    plane.gameObject.SetActive(false);
                }
            }
        }
        else //둘다 오프
        {
            _arPlaneManager.detectionMode = PlaneDetectionMode.None; //0
            foreach (var plane in _arPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }
        UnityEngine.Debug.Log("Detection mode is now " + _arPlaneManager.detectionMode.ToString());
    }
}
