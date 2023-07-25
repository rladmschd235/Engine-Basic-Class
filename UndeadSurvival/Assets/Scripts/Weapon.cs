using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 무기를 배치
    public int id; // 무기 id
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

        // 회전
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;
            default: // 나머지
                // 일정  간격으로 발사
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
                Batch(); // 배치
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

            bulltet.GetComponent<Bullet>().Init(damage, -100, Vector2.zero); // 주석: -100은 관통력 무한 
        }
    }

    private void Fire()
    {
        // 총알을 가져 옴
        // 총알을 쏨 누구한테? 가장 가까운 몬스터 
        if(!player.scanner.nearestTarget)
        {
            return;
        }

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;

        // 총알 생성
        Transform bullet = GameManager.instance.poolManager.Get(prefabId).transform;
        bullet.position = transform.position;
        // FromToRotation: 지정 된 축을 중심으로 목표까지 회전하는 함수
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
