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

    private bool isLive = true; // ���� ��������
    
    public Rigidbody2D target;
    private Rigidbody2D rigid;
    private Collider2D coll;
    private SpriteRenderer sprite;
    private Animator animator;

    WaitForFixedUpdate wait;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    // ������ �̵�
    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        if (!GameManager.instance.isLive)
        {
            return;
        }

        if (!isLive)
        {
            return;
        }

        // ���͵� ������ ��, �÷��̾� ���⺸���� �ٲٱ�
        sprite.flipX = target.position.x < rigid.position.x;
        // filpX�� true�� ����
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true; // ������ٵ� ������ ��Ȱ��ȭ�� .simulated = false;
        sprite.sortingOrder = 2;
        animator.SetBool("Dead", false);
        health = maxHealth;
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
        if(!collision.CompareTag("Bullet") || !isLive)
        {
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if(health > 0)
        {
            // hit action
            animator.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else
        {
            // die
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false; // ������ٵ� ������ ��Ȱ��ȭ�� .simulated = false;
            sprite.sortingOrder = 1;
            animator.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
            if(GameManager.instance.isLive)
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
            }
        }
    }

    IEnumerator KnockBack()
    {
        // yield return null; // 1 ������ ����
        // yield return new WaitForSeconds(2f); // 2�� ����
        yield return wait; // ���� �ϳ��� ���� ������ ������

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
