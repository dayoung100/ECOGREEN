# ECOGREEN
 캡스톤디자인 프로젝트 에코그린
 
# 에코그린 유튜브 채널
 
# 시연영상 링크
에코그린 시연영상(DeepLearning + Virtual Ruler + UI) :   
 
# 대형폐기물 배출 안내 어플리케이션
## 에코그린 Organization
https://github.com/dayoung100/ECOGREEN

## 프로젝트 레포지토리 정리
https://github.com/hyojin2/Android-Studio : android studio UI 구현  

## 프로젝트의 목적
이사/폐업/가구 교체 등의 상황에서 많은 대형폐기물이 발생하고 있으나 대형폐기물 배출요령에 대해서 정확히 파악하고 있는 경우는 드물다. 흔하게 경험할 수 있는 일이 아닐 뿐더러 절차가 복잡하고, 무엇보다 대형폐기물을 처리할 때 드는 수수료의 부과기준이 지역, 품목, 크기별로 달라 매번 찾아보기 번거롭기 때문이다.
우리는 이러한 문제를 해결하기 위해 대형폐기물 배출 절차를 단순화 시켜줄 어플리케이션을 제작하고자 했다. 
이를 위해 대형 폐기물 배출 안내 어플리케이션에서는 아래의 세 가지 주 기능을 제공하고자 한다.
1. 사진을 통한 품목 인식
2. Virtual Ruler를 이용한 크기 측정
3. 신고필증 자동 작성
 
## 시나리오 설계
1. 앱 가입을 통한 사용자 정보 받아오기(이름, 주소, GPS 등)
2. 대형폐기물 - 폐기 / 재활용 여부에 따라서 해당하는 버튼 클릭
3. 재활용 선택: 해당 사용자 인근에 있는 재활용센터 홈페이지 연결
4. 폐기 선택: 대형 폐기물 품목 입력하기 - 3가지 방법 제공(카메라/갤러리/직접 입력)   
        카메라: 카메라 촬영을 통한 품목 자동 인식 및 virtual ruler를 통한 규격 측정 기능 제공   
        갤러리: 갤러리에서 업로드하는 이미지의 품목 자동 인식   
        직접 입력: 품목 및 규격 목록 제공을 통한 직접 입력   
5. 폐기물 처리할 품목 선택 완료 시   
6. 체크리스트 페이지로 이동 - 처리할 품목 리스트(품목명, 규격) 및 수수료 금액 제공   
7. 해당 리스트를 바탕으로 한 신고필증 작성 및 pdf 제공   
 
## 프로젝트 예상 아키텍처
<img width="1792" alt="Screen Shot 2020-12-08 at 7 44 35 PM" src="https://user-images.githubusercontent.com/34567074/101474395-740bc600-398e-11eb-9cfb-a11eb3600723.png">
 
## Technology TODO   
### [Android Studio]
- [x] Firebase 연동 및 계정 설정 구현
- [x] 기본 화면 구성
- [x] 권한 얻어오기
- [ ] 데이터베이스 연결   
- [ ] 품목 분석 내역 보여주기, 분석한 내용에 맞는 수수료 안내 및 신고필증 작성 등 세부기능 구현  
- [ ] 딥러닝 모델 tflite 이용하여 안드로이드 스튜디오에 올리기   
(*앱이 많이 무거워지는 경우 flask나 Django를 통한 웹서버 구축 및 웹서버에 올려서 활용 예정)  
- [ ] AR - virtual ruler 연동  
- [ ] UI 개선  
- [ ] 앱 최적화  
    
    
## Reference
- Android Studio(패스트캠퍼스 온라인 강의 - 올인원 패키지: 모바일 앱 개발)    
https://github.com/changja88

   

## 기술블로그
### [Android Studio]
Firebase 프로젝트 생성: https://jeeny-yap.tistory.com/9   
Firebase 프로젝트 연결: https://jeeny-yap.tistory.com/10   
Firebase를 활용한 인증 구현 및 계정설정 기능: https://jeeny-yap.tistory.com/12   
메인 홈 화면 및 DISUSE 화면 구성: https://jeeny-yap.tistory.com/13   


## 오픈소스 라이선스
Apache License 2.0

