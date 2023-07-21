using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Ű����� ĳ���� �����̱�
    public Vector2 inputVec;
    public float speed; 
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animatorControllers;
   
    SpriteRenderer spriter;
    Rigidbody2D rigid;
    Animator anim; 

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true); // true�� ������ ��Ȱ��ȭ�� ������Ʈ�� ������Ʈ �������� ����
    }

    private void OnEnable()
    {
        speed *= Character.speed;
        anim.runtimeAnimatorController = animatorControllers[GameManager.instance.playerId];
    }

    private void Update() // ������ ���� �ֱ�
    {
        if(!GameManager.instance.isLive)
        {
            return;
        }
        
        // Ű���� �Է� �ޱ�
        inputVec.x = Input.GetAxisRaw("Horizontal"); // GetAxisRaw: -1, 0, 1 
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    // 1. �� addForce
    // 2. �ӵ� ���� velocity

    private void FixedUpdate() // ������ ���� �ֱ�
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        // ������ ����
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;

        // 3. ��ġ �̵� movePosition
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate() // �������� ����Ǳ� ���� ����
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        anim.SetFloat("Speed", inputVec.magnitude); // magnitude: ������ ũ��(����)

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!GameManager.instance.isLive)
        {
            return;
        }

        GameManager.instance.health -= Time.deltaTime * 10;

        if(GameManager.instance.health < 0)
        {
            for(int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
