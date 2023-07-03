using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public PoolManager poolManager;

    public float gameTime; // ���� �帣�� �ð�
    public float maxGameTime = 3 * 10f; // �ִ� ���� �ð�

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // �ð��� �带 �� ����. �ִ� ���� �ð��� �Ѿ�� ���� Ÿ�� �����ϵ���
        gameTime += Time.deltaTime;
        
        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
