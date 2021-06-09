# ECOGREEN
 캡스톤디자인 프로젝트 에코그린<br/><br/>

# 대형 폐기물 배출 어플리케이션 BARO
## 대형 폐기물 BARO로 바로 버리자!!

## 시연영상 (유튜브 채널)
https://www.youtube.com/channel/UCT70bDJj1z-kksHOvijeBCg?view_as=subscriber


## 프로젝트 배경
이사/폐업/가구 교체 등의 상황에서 많은 대형폐기물이 발생하고 있으나 대형폐기물 배출처리는 여전히 어렵습니다. 흔하게 경험할 수 있는 일이 아닐 뿐더러 절차가 복잡하고, 무엇보다 대형폐기물을 처리할 때 드는 수수료의 부과기준이 지역, 품목, 크기별로 달라 매번 찾아보기 번거롭기 때문입니다.
저희는 이러한 문제를 해결하기 위해 대형폐기물 배출 절차를 단순화 시켜줄 어플리케이션을 제작하였습니다.


## 프로젝트 목적
대형 폐기물 배출 어플리케이션 BARO에서는 아래의 세 가지 주 기능을 제공합니다.
1. 이미지 탐지 기술을 이용한 품목 인식
2. Virtual Ruler를 이용한 크기 측정
3. 신고필증 제공 및 이메일 전송


## 프로젝트 폴더 설명
Android Studio : 안드로이드 스튜디오를 활용한 앱 구현 코드    
AR_virtual ruler : 유니티를 활용한 virtual ruler 구현 코드      
Deep Learning : YOLO를 활용한 객체 탐지 모델 구현 코드    
Web Server : Google Cloud Platform을 활용한 Flask 서버 구축(딥러닝 모델 업로드), Node.js, firebase를 활용한 관리자 웹페이지 구현 코드  


## 시나리오 설계
1. 앱 가입을 통한 사용자 정보 받아오기(위치정보, 전화번호 등)
2. 대형폐기물 - 폐기 / 재활용 여부에 따라서 해당하는 버튼 클릭
3. 재활용 선택(REUSE): 해당 사용자 인근에 있는 재활용센터 홈페이지 연결
4. 폐기 선택(DISUSE)
  대형 폐기물 품목 입력 - 카메라/갤러리/직접 입력의 방법으로 품목 입력   
  대형 폐기물 규격 측정 - virtual ruler를 활용한 규격 측정
5. 체크리스트 페이지로 이동 - 처리할 품목 리스트(품목명, 규격) 및 수수료 금액 제공
6. 배출 일자 및 배출 장소 입력 후 결제 안내 페이지로 이동
7. 관리자페이지에서 승인을 하면 pdf 형식의 신고필증 제공 및 이메일 전송   


## 프로젝트 아키텍처
<img src="https://user-images.githubusercontent.com/68267278/121214160-d308b400-c8b9-11eb-9bbc-d17e01987c8d.png" width=600>

## Technology TODO  
### [Deep Learning]
- [x] 이미지 기반 물체 인식 기능  
- [x] 구글 웹크롤링, 이미지넷을 통해 데이터 수집   
- [x] 훈련 데이터셋 전처리 및 데이터 확대  
- [x] YOLOv4 Object Detection 모델 구축 및 학습   
- [x] Google Cloud Platform 이용해서 모델 Flask 서버에 업로드하여 안드로이드와 통신

### [AR: virtual ruler]
- [x] 유니티를 이용해 ARFoundation 사용
- [x] 평면 인식 후 터치하는 위치의 좌표 인식 기능 구현
- [x] Virtual Ruler 구현(길이 측정 기능)
- [x] 수직면, 수평면 인식 설정 기능, 점 지우는 기능, 안내 화면 등의 세부 기능 구현
- [x] 유니티로 만든 앱을 최종적으로 안드로이드 앱과 연동

### [Android Studio]
- [x] Firebase 연동 및 계정 설정 구현
- [x] 기본 화면 구성
- [x] 권한 얻어오기
- [x] 데이터베이스 연결(firebase의 realtime database 이용, 내부 sqlite database 이용)
- [x] 품목 인식, 규격 측정 후 그에 맞는 수수료 안내 후 배출 신청 처리
- [x] 관리자 페이지에서의 승인 후 신고필증 제공 및 이메일 전송
- [x] Deep Learning model의 서버와 통신
- [x] AR - virtual ruler 연동

### [Web]
- [x] 관리자 웹페이지 프론트엔드 제작
- [x] 관리자 웹페이지 nodejs, firebase 활용하여 백엔드 구현


## Reference
- how to train YOLO v3, v4 for custom objects detection | using colab free GPU    
https://www.youtube.com/watch?v=hTCmL3S4Obw
- Inflearn 딥러닝 웹서비스 프로젝트1 - 기본편. Object Detect 불량품 판별
https://www.inflearn.com/course/%EB%94%A5%EB%9F%AC%EB%8B%9D-%EC%9B%B9%EC%84%9C%EB%B9%84%EC%8A%A4-%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8-1/dashboard
- Quickstart for Android | ARCore    
https://developers.google.com/ar/develop/unity/quickstart-android    
- Explore the HelloAR sample app code | ARCore    
https://developers.google.com/ar/develop/unity/tutorials/hello-ar-sample    
- AR Measure | Camera | Unity Asset Store    
https://assetstore.unity.com/packages/tools/camera/ar-measure-145104       
- AR Core Ruler - Master ARCore 1.3 Unity SDK - Build 6 Augmented Reality Apps    
https://subscription.packtpub.com/video/application_development/9781789537413/70319/70320/ar-core-ruler    
- Android Studio(패스트캠퍼스 온라인 강의 - 올인원 패키지: 모바일 앱 개발)    
https://github.com/changja88   
- Firebase Android Authentication Tutorial | Login With Google Facebook and OTP in Android   
https://www.youtube.com/watch?v=_EgsrEmWwR8   
- Android How to Build Intro Slider for your App   
https://www.androidhive.info/2016/05/android-build-intro-slider-app/   
- 안드로이드 영수증 pdf 만들기   
https://www.youtube.com/watch?v=264FmC9AyYM   <br/><br/>


## 기술블로그
### [Deep Learning]
[web crawling] 파이썬 selenium 이미지 크롤링 : https://syeong622.tistory.com/2<br/>
[딥러닝] 이미지 데이터 전처리 : https://syeong622.tistory.com/3<br/>
[딥러닝] 합성곱 신경망(Convolutional Neural Network, CNN) : https://syeong622.tistory.com/4<br/>
[딥러닝] 케라스(Keras) CNN을 활용한 장롱 이미지 분류하기 : https://syeong622.tistory.com/5<br/>
[딥러닝] 이미지넷 데이터로 CNN과 YOLO를 활용하여 장롱 이미지 찾아내기 : https://syeong622.tistory.com/23  

### [AR: Virtual Ruler]
AR Foundation으로 Virtual Ruler 구현하기: https://iszero-tech.tistory.com/6  


### [Android Studio]
Firebase 프로젝트 생성: https://jeeny-yap.tistory.com/9   
Firebase 프로젝트 연결: https://jeeny-yap.tistory.com/10   
Firebase를 활용한 인증 구현 및 계정설정 기능: https://jeeny-yap.tistory.com/12   
메인 홈 화면 및 DISUSE 화면 구성: https://jeeny-yap.tistory.com/13   
[Kotlin으로 앱 개발하기] Custom Listview를 이용하여 다양한 정보를 리스트로 보여주기: https://jeeny-yap.tistory.com/14    <br/><br/>


## 오픈소스 라이선스
Apache License 2.0
