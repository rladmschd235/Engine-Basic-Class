using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animatorController;

    public Rigidbody2D target;
    private Rigidbody2D rigid;

    private bool isLive = true; // 몬스터 생존여부
    
    private SpriteRenderer sprite;
    private Animator animator;

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
        
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // 물리적 이동
    private void FixedUpdate()
    {
        if(!isLive)
        {
            return;
        }

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; // 물리적인 속도가 영향을 주지 않게 한다
    }

    private void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }

        // 몬스터도 움직일 때, 플레이어 방향보도록 바꾸기
        sprite.flipX = target.position.x < rigid.position.x;
        // filpX가 true면 왼쪽
    }

    // 초기 속성 적용 함수
    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animatorController[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = maxHealth;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Bullet"))
        {
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;

        if(health > 0)
        {
            // hit action
            
        }
        else
        {
            // die
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
