using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 키보드로 캐릭터 움직이기
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
        hands = GetComponentsInChildren<Hand>(true); // true를 넣으면 비활성화된 오브젝트의 컴포넌트 가져오기 가능
    }

    private void OnEnable()
    {
        speed *= Character.speed;
        anim.runtimeAnimatorController = animatorControllers[GameManager.instance.playerId];
    }

    private void Update() // 프레임 실행 주기
    {
        if(!GameManager.instance.isLive)
        {
            return;
        }
        
        // 키보드 입력 받기
        inputVec.x = Input.GetAxisRaw("Horizontal"); // GetAxisRaw: -1, 0, 1 
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    // 1. 힘 addForce
    // 2. 속도 제어 velocity

    private void FixedUpdate() // 물리적 실행 주기
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        // 움직일 벡터
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;

        // 3. 위치 이동 movePosition
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate() // 프레임이 종료되기 전에 실행
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        anim.SetFloat("Speed", inputVec.magnitude); // magnitude: 벡터의 크기(길이)

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
