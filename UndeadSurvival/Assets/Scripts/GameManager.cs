using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive;
    public Player player;
    public PoolManager poolManager;

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; //10, 30, 60

    [Header("# Game Object")]
    public float gameTime; // ���� �帣�� �ð�
    public float maxGameTime = 3 * 10f; // �ִ� ���� �ð�
    public LevelUp uiLevelUp;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;

        // �ӽý�ũ��Ʈ (ù��° ĳ���� ����)
        uiLevelUp.Select(0);
    }

    private void Update()
    {
        if(!isLive)
        {
            return;
        }
        
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

        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1; // 1�̻��� ���� ������ �׸�ŭ �� ������
    }
}
