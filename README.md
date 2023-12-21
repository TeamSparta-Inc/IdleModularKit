# IdleModularKit

이 프로젝트는 방치형게임의 Unity 기반 베이스 코드를 담고 있으며, Unity version은 2022.3.3f1 입니다.

## 작동 방법

1. 이 리포지토리를 클론합니다.
2. Unity version 2022.3.3f1 에서 프로젝트를 엽니다.
3. Assets/Scenes/SampleScene을 엽니다.

## 구조 및 아키텍처

### 기본 아키텍처
이 프로젝트는 기능별로 "기능 매니저 - UI - 객체"로 스크립트가 구성 되어있다.
기능 매니저 - 해당 기능의 전체적인 흐름을 매니징 하고 있는 스크립트이다.
UI - 해당 기능의 UI변화 감지 및 Scene의 상호작용을 담당하는 스크립트이다. 
객체 - 기능이 들어있는 객체들의 정보, 객체의 변화를 감지, 상호작용을 담당하는 스크립트이다.

### 주요 구성요소
- **GameManager**: 게임의 주요 흐름을 관리하고, 다른 Manager들의 초기화를 담당합니다.
- **CurrencyManager**: 게임 내의 모든 재화를 관리합니다.
- **OfflineRewardManager**: 오프라인 시스템을 관리합니다.
- **StatusUpgradeManager**: 플레이어 강화 기능의 전박적인 기능들을 관리합니다.
- **EquipmentManager**: 장비관련된 전반적인 기능들을 관리합니다.
- **Player**: 플레이어에 대한 전반적인 정보, 상호작용들을 관리합니다.
- **PlayerStatus**: 플레이어의 스텟 저장, 계산들을 관리합니다.
- **Equipment**: 장비에 필수적으로 들어가야할 정보와 기능들을 담고있고, 타입별로 Equipment를 상속받아 사용한다. (ex) WeaponInfo : Equipment)
- **EquipmentUI**: 장비에 대한 대부분의 UI를 제어, 상호작용을 관리한다.

## 사용 패키지
- **ES3**: 로컬로 저장하는 기능을 지원한다. 기존 PlayerPrefabs는 int, string bool 자료형 밖에 저장이 안되었지만, struct나 List, Dictionary등을 저장할 수 있게 해준다.
- **BigInteger**: int, long 보다 더 큰 수를 저장할 수 있게 해준다. 물론 C#에서 BigInteger를 지원해주지만, Unity에서는 C#에서 지원해주는 BigInteger를 사용할 수 없다.
- 
