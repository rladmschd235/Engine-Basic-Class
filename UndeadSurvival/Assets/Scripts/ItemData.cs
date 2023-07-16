using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal };

    [Header("# Main Info")]
    public ItemType itemType; // 아이템 종류
    public int itemId; // 아이템 아이디
    public string itemName; // 아이템 이름
    [TextArea]
    public string itemDesc; // 아이템 설명
    public Sprite itemIcon; // 아이템 아이콘

    [Header("# Level Data")]
    public float baseDamage; // 기본 피격 데미지
    public int baseCount; // 근거리: 기본 생성 개수, 원거리: 기본 관통 횟수
    public float[] damages;
    public int[] counts; 

    [Header("# Weapon")]
    public GameObject projectile; // 프리팹
    public Sprite hand;
}
