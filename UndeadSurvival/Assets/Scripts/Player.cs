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

    private void Update() // ������ ���� �ֱ�
    {
        // Ű���� �Է� �ޱ�
        inputVec.x = Input.GetAxisRaw("Horizontal"); // GetAxisRaw: -1, 0, 1 
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    // 1. �� addForce
    // 2. �ӵ� ���� velocity

    private void FixedUpdate() // ������ ���� �ֱ�
    {
        // ������ ����
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;

        // 3. ��ġ �̵� movePosition
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate() // �������� ����Ǳ� ���� ����
    {
        anim.SetFloat("Speed", inputVec.magnitude); // magnitude: ������ ũ��(����)

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}
