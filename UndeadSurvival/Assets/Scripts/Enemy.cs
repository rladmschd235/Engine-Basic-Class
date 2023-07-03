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

    private bool isLive = true; // ���� ��������
    
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

    // ������ �̵�
    private void FixedUpdate()
    {
        if(!isLive)
        {
            return;
        }

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; // �������� �ӵ��� ������ ���� �ʰ� �Ѵ�
    }

    private void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }

        // ���͵� ������ ��, �÷��̾� ���⺸���� �ٲٱ�
        sprite.flipX = target.position.x < rigid.position.x;
        // filpX�� true�� ����
    }

    // �ʱ� �Ӽ� ���� �Լ�
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
