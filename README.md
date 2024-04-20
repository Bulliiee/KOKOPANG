# README.md
---
[유니티 프로젝트 파일](https://drive.google.com/file/d/1MvcaUwIF-0CgRPlmbpSrGK4I-SAaM1Lj/view?usp=sharing)
---
### 목차
[1️⃣ Description](#1️⃣-description)

[2️⃣ How to play](#2️⃣-how-to-play)

[3️⃣ Web Page](#3️⃣-web-page)

[4️⃣ About the Project](#4️⃣-about-the-project)

[5️⃣  Unity Game Project](#5️⃣-unity-game-project)

[6️⃣ Asynchronous TCP Server](#6️⃣-asynchronous-tcp-server)

[7️⃣ Data Server](#7️⃣-data-server)

[8️⃣ 배포](#8️⃣-배포)

[9️⃣ About Team KOKO](#9️⃣-about-team-koko)

# 🌳 KOKOPANG 🌳

![koko](https://img.shields.io/badge/made_by-teamKOKO-pink)
![ssafy](https://img.shields.io/badge/with-SSAFY-skyblue)
![unity](https://img.shields.io/badge/Unity-2021.3.33f1-white?logo=unity)
![react](https://img.shields.io/badge/React-18.2.0-skyblue?logo=React)
![SpringBoot](https://img.shields.io/badge/springboot-3.2.4-6DB33F?logo=springboot)

![Main](ReadmeImg/Main.png)


# 1️⃣ Description


경비행기를 타고 여행을 하던 주인공과 그의 친구들. 하지만 난기류를 만나 경비행기는 이름 모를 섬에 추락하고 만다. 비행기를 조금만 고치면 다시 작동할 수 있을 것 같다. 하지만 이 섬은 20분 후에 가라앉는다. 제한된 시간 내에 재료를 모아 비행기를 수리한 후 섬을 탈출해야 한다.

**이 게임는 SSAFY 10기 특화 프로젝트로 제작되었습니다.**

# 2️⃣ How to play


## ① 불시착 비행기 정보 수집

| 위치 탐색                         | 수리 재료 확인                          |
|-------------------------------|-----------------------------------|
| ![비행기찾기](ReadmeImg/비행기찾기.gif) | ![비행기수리.gif](ReadmeImg/비행기수리.gif) |


## ② 파밍

### **1) 자연물**

| 동물                    | 광물                              | 나무                              |
|-----------------------|---------------------------------|---------------------------------|
| ![소](ReadmeImg/소.gif) | ![구리캐기.gif](ReadmeImg/구리캐기.gif) | ![나무캐기.gif](ReadmeImg/나무캐기.gif) |
| | ![철캐기.gif](ReadmeImg/철캐기.gif)   | |
 | | ![돌깨기.gif](ReadmeImg/돌깨기.gif)   | |


### **2) 인공물**

| 오크통                       | 연료통                           |
|---------------------------|-------------------------------|
| ![오크통](ReadmeImg/오크통.gif) | ![연료통.gif](ReadmeImg/연료통.gif) |


## ③ 조합

| 조합 성공                   | 조합 실패                           |
|-------------------------|---------------------------------|
| ![조합](ReadmeImg/조합.gif) | ![조합실패.gif](ReadmeImg/조합실패.gif) |


## ④ 탈출

| 탈출 성공                       | 탈출 실패                           |
|-----------------------------|---------------------------------|
| ![아웃트로](ReadmeImg/Outro.gif) | ![탈출실패.gif](ReadmeImg/탈출실패.gif) |


# 3️⃣ Web Page


| 홈페이지                            | 랭킹 페이지                          |
|---------------------------------|---------------------------------|
| ![홈페이지](ReadmeImg/홈페이지.png) | ![랭킹.gif](ReadmeImg/랭킹.png) |


# 4️⃣ About the project


## 🛠 System Architecture 🛠

![사스템아키텍쳐](ReadmeImg/시스템아키텍쳐.png)

## 🛠 Tech Stack 🛠

1. 이슈 관리 : ![Jira](https://img.shields.io/badge/Jira_Software-2024.03-0052CC?logo=jirasoftware)
2. 형상 관리 : ![GitLab](https://img.shields.io/badge/GitLab-2024.03-FC6D26?logo=GitLab)
3. 커뮤니케이션 : ![Mattermost](https://img.shields.io/badge/Mattermost-2024.03-0058CC?logo=mattermost) ![Notion](https://img.shields.io/badge/Notion-2024.03-000000?logo=notion) ![Discord](https://img.shields.io/badge/Discord-2024.03-5865F2?logo=discord)
4. 개발 환경
    - Game Engine  : ![Unity](https://img.shields.io/badge/Unity-2021.3.33f1-ffffff?logo=unity)
    - IDE
        - ![IntelliJIDEA](https://img.shields.io/badge/IntelliJ_IDEA-2323.3.2-ffffff?logo=intellijidea)
        - ![VisualStudio](https://img.shields.io/badge/Visual_Studio-2022-5C2D91?logo=visualstudio)
    - Database : ![MySQL](https://img.shields.io/badge/MySQL-2024.03-4479A1?logo=mysql) ![Redis](https://img.shields.io/badge/Redis-2024.03-003B57?logo=redis)
    - Server : ![Amazon EC2](https://img.shields.io/badge/Amozon_EC2-2024.03-FF9900?logo=amazonec2) ![Ubuntu](https://img.shields.io/badge/Ubuntu-20.04.6_LTS-E95420?logo=ubuntu)

### 📃 상세 사용

- GAME
    - ![csharp](https://img.shields.io/badge/Csharp-9.0-512BD4?logo=Csharp)
    - ![unity](https://img.shields.io/badge/Unity-2021.3.33f1-white?logo=unity)
- Asynchronous TCP Server
    - ![OpenJDK](https://img.shields.io/badge/OpenJDK-17-000000?logo=OpenJDK)
    - ![SpringBoot](https://img.shields.io/badge/springboot-3.2.4-6DB33F?logo=springboot)
    - ![Gradle](https://img.shields.io/badge/Gradle-8.5-02303A?logo=gradle)
    - ![WebFlux](https://img.shields.io/badge/WebFlux-6.1.5-6DB33F?logo=spring)
    - ![Netty](https://img.shields.io/badge/ReactorNetty-1.1.16-5B606F)
- Data Server
    - ![OpenJDK](https://img.shields.io/badge/OpenJDK-17-000000?logo=OpenJDK)
    - ![SpringBoot](https://img.shields.io/badge/springboot-3.2.4-6DB33F?logo=springboot)
    - ![Security](https://img.shields.io/badge/Spring_Security-3.2.4-6DB33F?logo=springsecurity)
    - ![Gradle](https://img.shields.io/badge/Gradle-8.5-02303A?logo=gradle)
    - ![JPA](https://img.shields.io/badge/JPA-3.2.4-6DB33F) ![Lombok](https://img.shields.io/badge/Lombok-1.18.30-6DB33F)
- AWS EC2
    - ![Ubuntu](https://img.shields.io/badge/Ubuntu-20.04.6_LTS-E95420?logo=ubuntu)
    - ![Ubuntu](https://img.shields.io/badge/Jenkins-2.444-D24939?logo=jenkins)
    - ![Docker](https://img.shields.io/badge/Docker-26.0.0-2496ED?logo=docker)

# 5️⃣ Unity Game Project

## 📌 SCENE PROGRESS

![씬프로그레스](ReadmeImg/씬프로그레스.png)
## 📌 구현 내용

### ①  씨네머신(Cinemachine)

- 유니티 씨네머신을 이용한 인트로 아웃트로 영상 촬영

![인트로사진](ReadmeImg/인트로사진.png)


- Virtual Camera와 Dolly Camera 를 이용하여 비행기의 움직임에 따라 카메라의 이동 전환조정
- Look At과 Follow 설정을 이용하여 오브젝트의 움직임에 따른 자연스러운 카메라의 전환 적용

### ② 대기방 목록

- 이미지 에셋을 이용하여 게시판 이미지 생성
- 소켓 통신을 이용하여 게임에 참여한 사람들에 대한 정보 표현
- 같은 소켓을 사용하는 인원들이 채팅을 통해 정보를 주고 받을 수 있도록 설계

- 새로운 방 생성과 다른 방에 참가하는 기능 추가


| 방목록 조회                        | 새로운 방 생성 |
|-------------------------------|----------|
| ![대기방목록](ReadmeImg/대기방목록.png) | ![방생성](ReadmeImg/방생성.png)         |


### ③ 입장한 방

- 같은 방에 참여한 인원들이 통신을 주고 받을 수 있는 채팅창 구현

- 인게임 정보를 확인할 수 있는 튜토리얼 페이지 제작


| 방 입장                      | 아이템 설명                      | 조작법 설명                        |
|---------------------------|-----------------------------|-------------------------------|
| ![방내부](ReadmeImg/방내부.png) | ![방생성](ReadmeImg/아이템설명.png) | ![조작법설명](ReadmeImg/조작법설명.png) |


### ④ 메인 게임 씬

- ScriptableObject 스크립트를 통해서 아이템 데이터 컨테이너를 저장
- 저장된 아이템 데이터들을 이용하여 아이템 프리팹 생성

  
  | 아이템프리팹                           |
  |-------------------------------|
  | ![아이템프리팹](ReadmeImg/아이템프리팹.png)



- 오브젝트들에 Collider와 적용과 HP를 적용하여 Attack 을 했을 때 맨손은 HP—; 무기를 들었을 경우 HP -= 2; 로 적용 하여 오브젝트를 파괴하고 파밍

| 오브젝트                        | 오브젝트 파밍                   |
|-----------------------------|---------------------------|
| ![소오브젝트](ReadmeImg/소오브젝트.png) | ![오크통](ReadmeImg/오크통.gif) |

- 오브젝트들에 Collider와 적용과 HP를 적용하여 Attack 을 했을 때 맨손은 HP—; 무기를 들었을 경우 HP -= 2; 로 적용 하여 오브젝트를 파괴하고 파밍
- 크래프팅, 아이템 등 UI 이미지를 통해 구현 후 Slot transform을 계산하여 범위에 벗어났을 경우 템이 옮겨지지 않도록 구현
- 오브젝트들에 Collider와 적용과 HP를 적용하여 Attack 을 했을 때 맨손은 HP—; 무기를 들었을 경우 HP -= 2; 로 적용 하여 오브젝트를 파괴하고 파밍
- 크래프팅, 아이템 등 UI 이미지를 통해 구현 후 Slot transform을 계산하여 범위에 벗어났을 경우 템이 옮겨지지 않도록 구현

| 조합                      | 오브젝트 획득                   |
|-------------------------|---------------------------|
| ![조합](ReadmeImg/조합.gif) | ![연료통](ReadmeImg/연료통.gif) |

- 점수 획득 시스템을 통해 점수를 획득하고 아웃트로 씬이 재생되고 결과창을 통해 자신의 점수 확인 가능




### ⑤ 게임결과 창

- 게임에서 얻은 점수를 Score 관련 스크립트를 DontDestroyOnload로 유지하여 결과창에 점수를 띄움
- 유저 별 랭크를 표시하기 위해 게임에서 얻은 점수를 데이터에 PUT 하여 누적 점수를 기록
- 홈페이지를 통해 누적된 랭크를 통해 자신의 순위 확인 가능

| 점수판                           |
|-------------------------------|
| ![점수판.png](ReadmeImg/점수판.png) |

### ⑥ 로그인 & 회원가입

- 기본적인 기능을 할 수 있는 로그인 회원가입 페이지 구현
- 중복 검사 및  암호화 구현

| 로그인                       | 로그인 암호화                         | 회원가입                        |  회원가입 암호화 |
|---------------------------|---------------------------------|-----------------------------|---|
| ![로그인](ReadmeImg/로그인.png) | ![로그인암호화](ReadmeImg/로그인암호화.png) | ![회원가입](ReadmeImg/회원가입.png) | ![회원가입암호화](ReadmeImg/회원가입암호화.png)


# 6️⃣ Asynchronous TCP Server



## 📌 구현 내용

- Reactor Netty 라이브러리 활용
- Reactor Pattern으로 비동기 Non-Blocking TCP 서버 구현
- ChannelPipeline에 Handler 등록
    - LineBasedFrameDecoder
    - 사용자 정의 ChannelHandlerAdapter


| Method | Description |
| --- | --- |
| handlerAdded | client와 최초로 연결되었을 경우|
| handlerRemoved | client와의 연결이 끊겼을 경우 |
| exceptionCaught | 예외 처리|


- Data Stream 처리
    - JSONObject로 처리
    - 분기
        - 기준 key : “channel”
        - 분기 value : “lobby”, “channel”, “ingame”

|Channel|Description|
| --- |-----------------------------------------------------------------------------------------------------------|
| lobby | 1) lobby에 있는 session 목록 관리 <br> 2) 생성된 게임 channel 목록 관리 <br>  3) lobby 안 client 간 채팅                      |
| channel | 1) 해당 channel에 참가한 session 목록 관리  <br> 2) ready 상태 관리  <br>  3) client 간 채팅  <br>  4) 게임 시작 여부 관리 |
| ingame | channel 내 session 간 broadcasting   <br>  1) 게임 중  <br>  - 좌표  <br>  2) 게임 종료 후  <br> - 게임 clear 여부  <br> - score |

## 📌 CLIENT-SERVER 데이터 흐름

### ① LOBBY

1. 최초 접속

![로비최초접속](ReadmeImg/로비최초접속.png)

2. 클라이언트로부터 요청

![로비요청](ReadmeImg/로비요청.png)

### ② CHANNEL

![채널요청](ReadmeImg/채널요청.png)

### ③ INGAME

![인게임요청](ReadmeImg/인게임요청.png)

# 7️⃣ Data Server

## 📌 ERD

![erd](ReadmeImg/erd.png)

# 8️⃣ 배포

## ① Docker

서로 다른 도커 이미지로 저장되어 있어 각각의 이미지를 실행시킵니다.

필요한 이미지들의 설정은 docker-compose.yml 파일에 작성합니다.

docker 내부는 같은 네트워크로 묶어주기 위해 docker-compose로 실행합니다.

## ② Devops

`Jenkins`로 CI/CD를 구축하여 develop 브랜치에 `merge` 이벤트 발생시 build하도록 구성했습니다.

build 후 docker-compose를 실행하도록 했습니다.

# 9️⃣ About Team KOKO

### 팀원 소개

| 김선욱 `팀장`                                | 강승원                       | 이항우 `PM`                               | 김영일                       | 이주현                       | 장동재                       |
|----------------------------------------|---------------------------|---------------------------------------|---------------------------|---------------------------|---------------------------|
| ![김선욱](ReadmeImg/김선욱.png)              | ![강승원](ReadmeImg/강승원.png) | ![이항우](ReadmeImg/이항우.png)             | ![김영일](ReadmeImg/김영일.png) | ![이주현](ReadmeImg/이주현.jpg) | ![장동재](ReadmeImg/장동재.png) |
| `CLIENT`                               | `CLIENT`                  | `CLIENT` `INFRA`                      | `SERVER`| `SERVER`| `SERVER`|
| [GITHUB](https://github.com/Woogie-Gim) | [GITHUB](https://github.com/kangseungwon0529)                | [GITHUB](https://github.com/Bulliiee) | [GITHUB](https://github.com/012coding) | [GITHUB](https://github.com/LeeeJooo) | [GITHUB](https://github.com/djjang0925) |
