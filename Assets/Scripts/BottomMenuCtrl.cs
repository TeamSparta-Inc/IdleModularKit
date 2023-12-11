using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomMenuCtrl : MonoBehaviour
{
    [Header("버튼과 패널")]
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] panels;

    private void Start()
    {
        // 각 버튼에 이벤트 리스너 할당
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // 현재 인덱스 캡처
            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    // 버튼 클릭 시 호출되는 메서드
    private void OnButtonClicked(int index)
    {
        // 모든 패널을 순회하면서 상태 설정
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == index);
        }
    }
}
