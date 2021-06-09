using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.UI; //button 사용을 위해 추가
using System;
using System.Linq;
using System.Diagnostics;

public class ARMainScript : MonoBehaviour
{
    /*
     * 길이 측정, 표시, 평면인식 준비 기능
     */

    //---------------선언부---------------
    //큰 평면의 기준
    [SerializeField] private Vector2 dimensionsForBigPlane; //BigPlane에 대한 x,y //직렬화하면 private여도 쓸 수 있음(유니티창에서도 보임)
    [SerializeField] private Text _lengthTxt; //길이표시 텍스트
    [SerializeField] private Text _positionTxt; //test 텍스트
    [SerializeField] private ARSession _arSession; //session

    //[SerializeField] private GameObject _centerPoint;

    public Transform _mainCam; //메인 카메라
    public Vector2 _centerVec; //중앙벡터
    public Transform _camPivot; //카메라의 중앙점
    public Transform _pivot; //측정되는 면 디버깅용 피벗

    //private bool _rulerEnable; //raycast가 평면에 닿았는지

    public event Action OnBigPlaneFound; //일정 크기 이상의 평면이 감지됨
    //plane manager
    public ARPlaneManager _arPlaneManager;
    public List<ARPlane> arPlanes;
    
    //Raycaster
    public ARRaycastManager _arRaycaster;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //instantiate gameobjects for the Points
    GameObject[] Points;
    public int p_index = 0;

    //instances of the Texts
    GameObject[] Texts;
    public int t_index = 0;

    /// A model to place when a raycast from a user touch hits a plane.
    public GameObject _prefabPoint, _prefabText;

    //prevent doubles
    bool exist = false;

    GameObject centerpoint;

    /// The rotation in degrees need to apply to model when the Andy model is placed.
    private const float k_ModelRotation = 180.0f;
    //---------------선언부---------------


    //---------------평면 인식 준비---------------
    //활성화
    private void OnEnable()
    {
        arPlanes = new List<ARPlane>(); //추가할 평면
        _arPlaneManager = FindObjectOfType<ARPlaneManager>();

        _arPlaneManager.planesChanged += OnPlanesChanged; //평면 변화 감지
    }

    //비활성화 - garbage많거나 전체를 느리게 할 필요가 있을때
    private void OnDisable()
    {
        _arPlaneManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null && args.added.Count > 0)
            arPlanes.AddRange(args.added);

        //x곱하기 y가 최소 0.1제곱미터
        foreach (ARPlane plane in arPlanes.Where(plane => plane.extents.x * plane.extents.y >= 0.1f))
        {
            if (plane.extents.x * plane.extents.y >= dimensionsForBigPlane.x * dimensionsForBigPlane.y)
            {
                //tell someone we found a big plane
                OnBigPlaneFound.Invoke();
                _positionTxt.text = "준비됨!";
            }
        }
    }
    //---------------평면 인식 준비---------------
    
    static bool WantsToQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        UnityEngine.Debug.Log("editor");
#else
        ProcessThreadCollection pt = Process.GetCurrentProcess().Threads;
        foreach (ProcessThread p in pt)
        {
             p.Dispose();
        }

        System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
        UnityEngine.Debug.Log("Player prevented from quitting.");
        return true;
    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        UnityEngine.Debug.Log("run on start");
        Application.wantsToQuit += WantsToQuit;
    }

    //---------------시작---------------
    private void Start()
    {
        //initialize arrays
        Points = new GameObject[10000];
        Texts = new GameObject[10000];
        _centerVec = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); //중앙점 위치 초기화
        centerpoint = Instantiate(_prefabPoint, _centerVec, Quaternion.identity);
        centerpoint.GetComponent<LineRenderer>().positionCount = 0;
        UnityEngine.Debug.Log(_centerVec);
    }

    //---------------시작---------------


    //---------------메인---------------
    /*
     * ok누르면 그떄의 center에 점 프리팹 만들고, 위치 기록(아니면 프리팹을 기록)
     * 그 프리팹부터 센터(의 레이)까지 라인 프리팹 실시간으로 길이 바뀌게(Update)
     * 라인의 길이 계산해서 띄움(Update)
     * 한번 더 ok누르면 그때의 center에 점 프리팹 또 만들기
     * 그리고 스타트포인트의 좌표 갱신
     * 이전의 라인에 최종길이 남겨두기(텍스트프리팹), 새로운 길이는 새로운 스타트포인트로 다시 계산
     * 삭제버튼 누르면 점 프리팹과 스타트 포인트 좌표 리뉴얼
     */

    // Update is called once per frame
    void Update()
    {
        //종료버튼 누르면 종료됨(뒤로가기)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //_positionTxt.text = "뒤로가기!";

            Application.Quit();
            //UnityEngine.Debug.Log("quit");
        }

        /*마우스로 확인용
        if (Input.GetMouseButtonDown(0))
        {
            
        }*/

        /*
        if(Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);

        if(touch.phase != TouchPhase.Began)
        {
            return;
        }
        */

        //버튼 중복 클릭 방지
        if (exist) exist = false;

        
        if (_arRaycaster.Raycast(_centerVec, hits, TrackableType.PlaneWithinPolygon))
        {

            Pose hitPose = hits[0].pose; //레이가 부딪힌 위치(센터)
            

            _pivot.rotation = Quaternion.Lerp(_pivot.rotation, hitPose.rotation, 0.2f);
            centerpoint.transform.position = hitPose.position;



            if (p_index > 0)
            {
                //라인의 길이 조절
                Vector3 dir = hitPose.position - Points[p_index - 1].transform.position;  //벡터

                Points[p_index - 1].GetComponent<LineRenderer>().positionCount = 2;
                Points[p_index - 1].GetComponent<LineRenderer>().SetPosition(0, Points[p_index - 1].transform.position);//시작점
                Points[p_index - 1].GetComponent<LineRenderer>().SetPosition(1, hitPose.position);//끝점


                //길이 계산
                _lengthTxt.text = Mathf.Round(dir.magnitude * 1000) / 10 + "cm";  //유니티의 기본 단위는 m(미터)
            }
        }
        else
        {
            Quaternion tRot = Quaternion.Euler(90f, 0, 0);
            _pivot.rotation = Quaternion.Lerp(_pivot.rotation, tRot, 0.5f);
        }
    }

    //ok 버튼 클릭
    public void okBtnClicked()
    {
        /*
         * 처음 누르면 startpoint에 저장
         * 두번째는 endpoint에 저장, startpoint와의 거리 계산해서 update에 출력
         * 세번째는 endpoint에 있던거 startpoint로 넘겨주고, endpoint에 새로 저장, 둘 사이에서 계산
         */

        if (_arRaycaster.Raycast(_centerVec, hits) && !exist)
        {
            Pose hitPose = hits[0].pose; //레이가 부딪힌 위치(센터)

            //점 생성(라인은 여기 붙어있음)
            Points[p_index] = Instantiate(_prefabPoint, hitPose.position, hitPose.rotation);
            Points[p_index].transform.Rotate(0, k_ModelRotation, 0, Space.Self);

            //점이 2개 이상
            if (p_index > 0)
            {
                Vector3 exdir = Points[p_index].transform.position - Points[p_index - 1].transform.position;  //방향벡터
                Vector3 center = Points[p_index - 1].transform.position + exdir / 2;  //중간

                //두 점 사이에 길이 텍스트 생성
                Texts[t_index] = Instantiate(_prefabText, center, hitPose.rotation);
                if (exdir.x > 0)
                {
                    Texts[t_index].transform.rotation = Quaternion.LookRotation(exdir, Points[p_index].transform.up);
                }
                else
                {
                    Texts[t_index].transform.rotation = Quaternion.LookRotation(-exdir, Points[p_index].transform.up);
                }
                Text txScp = Texts[t_index].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();  //Texts-canvas-text
                txScp.text = Mathf.Round(exdir.magnitude * 1000) / 10 + "cm";  //유니티의 기본 단위는 m(미터)

                t_index += 1;
            }
            exist = true; 
            p_index += 1;
            //positionTxt.text = "p:"+p_index+" t: "+t_index;
        }
    }

    //cancel 버튼 클릭
    public void EraseButtonClick()
    {
        if (p_index > 0)
        {
            if (t_index < 1)
            {
                Destroy(Points[p_index - 1]); //가장 최근의 점 삭제

                p_index = 0;
                t_index = 0;
            }
            else
            {
                Destroy(Points[p_index - 1]); //가장 최근의 점 삭제
                Destroy(Texts[t_index - 1]);  //가장 최근의 글자 삭제

                p_index -= 1;
                t_index -= 1;
            }
        }
        //_positionTxt.text = "t:" + t_index + " p: " + p_index;
    }

    

    //종료버튼 클릭시
    void OnApplicationQuit()
    {
        //Application.CancelQuit();
        /*
#if !UNITY_EDITOR
        //AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //activity.Call<bool>("moveTaskToBack" , true);        
        ProcessThreadCollection pt = Process.GetCurrentProcess().Threads;
        foreach (ProcessThread p in pt)
        {
             p.Dispose();
        }

        System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
        //UnityEngine.Debug.Log("quit");
        //어플리케이션 종료
        //Application.Quit();
        //Application.CancelQuit();
        //UnityEngine.Debug.Log("quit");
        /*
        #if !UNITY_EDITOR
                System.Diagnostics.Process.GetCurrentProcess().Kill();
        #endif*/
        //Application.Quit();
        //UnityEngine.Debug.Log("process kill");
    }

}