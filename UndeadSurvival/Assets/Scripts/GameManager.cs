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
    public float gameTime; // 실제 흐르는 시간
    public float maxGameTime = 3 * 10f; // 최대 게임 시간
    public LevelUp uiLevelUp;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;

        // 임시스크립트 (첫번째 캐릭터 선택)
        uiLevelUp.Select(0);
    }

    private void Update()
    {
        if(!isLive)
        {
            return;
        }
        
        // 시간이 흐를 때 갱신. 최대 게임 시간이 넘어가면 게임 타임 고정하도록
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
        Time.timeScale = 1; // 1이상의 수를 넣으면 그만큼 더 빨라짐
    }
}
