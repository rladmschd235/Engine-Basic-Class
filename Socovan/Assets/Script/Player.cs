using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    
    float speed = 10f;
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //rigid.AddForce(0, 1000, 0);
    }

    void Update()
    {
        if(gameManager.isGameOver == true)
        {
            return;
        }

        float fallSpeed = rigid.velocity.y;

        float inputX = Input.GetAxis("Horizontal"); // GetAxis: -1~1까지 받아온다 소수점까지 받아옴 ㅇㅇ
        float inputZ = Input.GetAxis("Vertical"); // GetAxisRaw: -1, 0, 1만 받아온다 정수만 받아옴 ㅇㅇ 

        Vector3 velopcity = new Vector3(inputX, 0, inputZ) * speed; // velopcity: 속도(방향*속력), speed: 속력
        // velopcity = velopcity * speed;
        velopcity.y = fallSpeed;
        rigid.velocity = velopcity;

        //rigid.AddForce(inputX * speed, 0, inputZ * speed); // 힘이라서 관성 때문에 계속 움직인다.

        // w키 누르면 앞으로 이동
        //if(Input.GetKeyDown(KeyCode.W))
        //{
        //    rigid.AddForce(0, 0, speed);
        //}
    }
}
