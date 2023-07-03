using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �����յ��� ������ ����
    public GameObject[] prefabs;

    // Ǯ ����� �� �� �ִ� ����Ʈ��
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index) // � Ǯ�� ������Ʈ�� �����ñ��??
    {
        GameObject select = null;

        // ������ Ǯ�� ��� �ִ� ���� ������Ʈ ����
        // �߰��ϸ� select ������ �Ҵ�

        // �迭, ����Ʈ�� ��ҵ��� �����Ҷ�
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
