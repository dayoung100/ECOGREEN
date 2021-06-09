using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;  //필수 선언
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;



public class CameraPermission : MonoBehaviour
{
    [SerializeField] ARSession m_Session;
    [SerializeField] private Text positionTxt;

    IEnumerator Start()
    {
        //만약 카메라 퍼미션이 참이 아니면
        //HasUserAuthorizedPermission(Permission.권한) 함수는 true false 를 반환함.
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            //퍼미션 요청.
            //RequestUserPermission(Permission.권한)
            Permission.RequestUserPermission(Permission.Camera);
        }

        if ((ARSession.state == ARSessionState.None) || (ARSession.state == ARSessionState.CheckingAvailability))
        {
            positionTxt.text = "찾는중";
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported)
        {
            // Start some fallback experience for unsupported devices
            positionTxt.text = "못해";
        }
        else
        {
            // Start the AR session
            positionTxt.text = "찾음";
            m_Session.enabled = true;
        }
    }
}


