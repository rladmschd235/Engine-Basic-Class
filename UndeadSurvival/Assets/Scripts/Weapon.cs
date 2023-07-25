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
        if (!GameManager.instance.isLive)
        {
            return;
        }

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

    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage;
        this.count += count;

        if (id == 0)
        {
            Batch();
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property set
        id = data.itemId;
        damage = data.baseDamage * Character.Damage;
        count = data.baseCount + Character.Count;

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
                speed = 150 * Character.weaponSpeed;
                Batch(); // ��ġ
                break;
            default:
                speed = 0.5f * Character.weaponRate;
                break;
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

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

            bulltet.GetComponent<Bullet>().Init(damage, -100, Vector2.zero); // �ּ�: -100�� ����� ���� 
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

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
