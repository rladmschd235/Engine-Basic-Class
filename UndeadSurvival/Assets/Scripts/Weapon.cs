using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // ���⸦ ��ġ

    public int id; // ���� id
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    private void Awake()
    {
        player = GameManager.instance.player;
    }

    private void Update()
    {
        // ȸ��
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;
            default: // ������
                // ����  �������� �߻�
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            LevelUp(20, 5);
        }
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for(int i = 0; i < GameManager.instance.poolManager.prefabs.Length; i++)
        {
            if(data.projectile == GameManager.instance.poolManager.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        switch (id)
        {
            case 0: // up, rigth
                speed = -150;
                Batch(); // ��ġ
                break;
            default:
                speed = 0.5f;
                break;
        }

        // Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);  
    }

    private void Batch()
    {
        for(int index = 0; index < count; index++)
        {
            Transform bulltet;

            if(index < transform.childCount)
            {
                bulltet = transform.GetChild(index);
            }
            else
            {
                bulltet = GameManager.instance.poolManager.Get(prefabId).transform;
                bulltet.parent = transform;
            }

            bulltet.localPosition = Vector3.zero;
            bulltet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 / count * index;
            bulltet.Rotate(rotVec);
            bulltet.Translate(bulltet.up * 1.5f, Space.World);

            bulltet.GetComponent<Bullet>().Init(damage, -1, Vector2.zero); // �ּ�: -1�� ����� ���� 
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
        {
            Batch();
        }
    }

    private void Fire()
    {
        // �Ѿ��� ���� ��
        // �Ѿ��� �� ��������? ���� ����� ���� 
        if(!player.scanner.nearestTarget)
        {
            return;
        }

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;

        // �Ѿ� ����
        Transform bullet = GameManager.instance.poolManager.Get(prefabId).transform;
        bullet.position = transform.position;
        // FromToRotation: ���� �� ���� �߽����� ��ǥ���� ȸ���ϴ� �Լ�
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

    }
}
