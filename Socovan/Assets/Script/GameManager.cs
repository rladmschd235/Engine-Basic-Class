using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winText;
    
    // ��� ������ �ڽ��� �˾ƾ� ��
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
            SceneManager.LoadScene("Main"); // �Ǵ� ���� ��ȣ
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
            Debug.Log("���� �¸�");
        }
    }
}
