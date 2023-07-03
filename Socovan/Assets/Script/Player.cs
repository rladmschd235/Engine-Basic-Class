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

        float inputX = Input.GetAxis("Horizontal"); // GetAxis: -1~1���� �޾ƿ´� �Ҽ������� �޾ƿ� ����
        float inputZ = Input.GetAxis("Vertical"); // GetAxisRaw: -1, 0, 1�� �޾ƿ´� ������ �޾ƿ� ���� 

        Vector3 velopcity = new Vector3(inputX, 0, inputZ) * speed; // velopcity: �ӵ�(����*�ӷ�), speed: �ӷ�
        // velopcity = velopcity * speed;
        velopcity.y = fallSpeed;
        rigid.velocity = velopcity;

        //rigid.AddForce(inputX * speed, 0, inputZ * speed); // ���̶� ���� ������ ��� �����δ�.

        // wŰ ������ ������ �̵�
        //if(Input.GetKeyDown(KeyCode.W))
        //{
        //    rigid.AddForce(0, 0, speed);
        //}
    }
}
