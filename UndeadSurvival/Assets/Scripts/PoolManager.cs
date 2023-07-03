using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹들을 보관할 변수
    public GameObject[] prefabs;

    // 풀 담당을 할 수 있는 리스트들
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index) // 어떤 풀의 오브젝트를 가져올까요??
    {
        GameObject select = null;

        // 선택한 풀의 놀고 있는 게임 오브젝트 접근
        // 발견하면 select 변수에 할당

        // 배열, 리스트의 요소들을 접근할때
        foreach(GameObject Item in pools[index]) 
        {
            if(!Item.activeSelf)
            {
                select = Item;
                select.SetActive(true);
                break;
            }
        }

        if(select == null)
        {
            select = Instantiate(prefabs[index], transform);    
            pools[index].Add(select);
        }

        return select;
    }
}
