using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public PoolManager poolManager;

    public float gameTime; // 실제 흐르는 시간
    public float maxGameTime = 3 * 10f; // 최대 게임 시간

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // 시간이 흐를 때 갱신. 최대 게임 시간이 넘어가면 게임 타임 고정하도록
        gameTime += Time.deltaTime;
        
        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
