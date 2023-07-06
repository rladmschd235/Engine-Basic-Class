using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("# Game Control")]
    public Player player;
    public PoolManager poolManager;

    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; //10, 30, 60

    [Header("# Game Object")]
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

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
