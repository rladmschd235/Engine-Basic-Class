using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winText;
    
    // 모든 아이템 박스를 알아야 함
    public ItemBox[] itemBoxs;
    public bool isGameOver;

    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main"); // 또는 씬의 번호
        }

        if(isGameOver == true)
        {
            return;
        }
        
        int count = 0;

        for(int i = 0; i < itemBoxs.Length; i++)
        {
            if(itemBoxs[i].isOveraped == true)
            {
                count++;
            }
        }

        if(count >= itemBoxs.Length)
        {
            winText.SetActive(true);
            Debug.Log("게임 승리");
        }
    }
}
