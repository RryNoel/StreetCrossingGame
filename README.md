# StreetCrossingGame
 내일배움캠프 심화 주차 개인 과제   
 Unity를 활용한 3D 게임

# 🖥️ 프로젝트 소개

게임 개발 심화 주차 게임 선택으로 길건너 친구들을 선정함   
선정 이유는 지금까지 복습을 많이 못했기 때문에 복습할 겸 선정함

# 🕰️ 개발 기간

24.06.17 (월) - 24.06.18 (화)

# 🧑‍🤝‍🧑 팀원 구성

- 김정석

# ⚙️ Development Environment

- Language : C#
- Engine : Unity 2022.3.17f1
- IDE : Visual Studio 2022
- Framework : .NET 6.0

# 📌 주요 기술

## Input System, Animation

![InputSystem](https://github.com/RryNoel/StreetCrossingGame/assets/97824309/f13c63fb-364b-4802-9d89-f14541132c4a)

인풋 시스템을 통해서 길건너 친구들처럼 1블럭당 1칸씩 이동할 수 있게 만들고, 이동 애니메이션도 적용

## Prefab, EnvironmentManager

![image](https://github.com/RryNoel/StreetCrossingGame/assets/97824309/535d926d-52ad-45c2-93e5-24fba20f9437)
![EnvironmentManager](https://github.com/RryNoel/StreetCrossingGame/assets/97824309/b2d4e57f-5f96-4244-b8ce-49015cffbc27)

자동차나 땟목, 나무, 도로 등등 오브젝트들을 Prefeb화 시키고, EnvironmentManager를 통해서 자동 삭제 및 생성 가능

## Collider, Score UI System

![Collider](https://github.com/RryNoel/StreetCrossingGame/assets/97824309/a229c55c-72d3-4e98-aa89-5a861ac1d619)

꽃 아이템을 먹으면 왼쪽 위 UI에 점수가 올라가고, 자동차에 부딪히면 게임 끝 판넬이 나오면서 점수가 나옴

## Sound System

![image](https://github.com/RryNoel/StreetCrossingGame/assets/97824309/984d1bcf-9de8-45a7-b061-4a68c9083dfa)

강의를 바탕으로 사운드 매니저 구축 및 배경음악 탑재

## Scripatable Object

![image](https://github.com/RryNoel/StreetCrossingGame/assets/97824309/51a0a81a-25b9-40e4-9d6f-469bc7a6bf6a)

스크립터블 오브젝트를 사용해서 자동차의 정보를 받아옴   
하나의 프리팹으로 하려고 했지만, 다양한 자동차를 사용해야하다보니 자동차 프리팹이 나눠짐

## Object Pool

![image](https://github.com/RryNoel/StreetCrossingGame/assets/97824309/4dd95d55-598d-4bd5-95ab-c82bb33c171c)
![image](https://github.com/RryNoel/StreetCrossingGame/assets/97824309/8f5d10bc-8d25-41bc-8a47-e3631ec1fb5f)

오브젝트 풀은 태그를 받아오는 형식으로 구성하였고, 자동차들을 오브젝트 풀로 구현하였다.


# ⚒️ 트러블 슈팅

![Bug](https://github.com/RryNoel/StreetCrossingGame/assets/97824309/94bf8b88-8a94-4d3a-abe5-185fb4d2387f)

땟목을 타고 내렸을 때 플레이어의 X 값이 땟목을 타기전의 X값으로 이동되는 현상 발생   
이거는 플레이어의 목표 X값을 갱신해주어 해결

![Bug2](https://github.com/RryNoel/StreetCrossingGame/assets/97824309/9f551a14-2b8e-4569-81a2-82544a25a7ba)

땟목을 타고 내리면 이동때문인지 몰라도 플레이어의 X값과 Z값이 틀어지는 현상이 발생함   
이로 인해서 충돌이 되면 안되는 오브젝트에 플레이어가 끼이는 현상이 발생 (현재진행형)

![Restart](https://github.com/RryNoel/StreetCrossingGame/assets/97824309/98b1bbb7-8e50-4aa1-9678-3da6d6164348)

재시작하는데 있어서 로직상으로는 문제가 없고, 디버그도 찍어본 결과 정상적으로 작동하는데, 맵이 생성되지 않는 문제가 발생함 (현재진행형)
