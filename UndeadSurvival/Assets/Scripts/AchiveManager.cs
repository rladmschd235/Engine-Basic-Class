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
        // ������ �������� �����ϴ� ����Ƽ ���� Ŭ�����̴�
        PlayerPrefs.SetInt("MyData", 1);  // SetInt �Լ��� ����Ͽ� key�� ����� int �� �����͸� �����մϴ�
        PlayerPrefs.SetInt("MyData", 0);  
        PlayerPrefs.SetInt("MyData", 0);  
    }
}
