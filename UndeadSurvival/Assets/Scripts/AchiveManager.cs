using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    enum Achive { UnlockPotato, UnlockBean }
    Achive[] achives;

    private void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
    }

    private void Init()
    {
        // 간단한 저장기능을 제공하는 유니티 제공 클래스이다
        PlayerPrefs.SetInt("MyData", 1);  // SetInt 함수를 사용하여 key와 연결된 int 형 데이터를 저장합니다
        PlayerPrefs.SetInt("MyData", 0);  
        PlayerPrefs.SetInt("MyData", 0);  
    }
}
