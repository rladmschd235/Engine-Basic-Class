using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float speed
    {
        get { return GameManager.instance.playerId == 0 ? 1.1f : 1f; }
    }

    public static float weaponSpeed
    {
        get { return GameManager.instance.playerId == 1 ? 1.1f : 1f; }
    }
    
    public static float weaponRate
    {
        get { return GameManager.instance.playerId == 1 ? 0.9f : 1f; }
    }

    public static float Damage
    {
        get { return GameManager.instance.playerId == 2? 1.2f : 1f; }
    }

    public static int Count
    {
        get { return GameManager.instance.playerId == 3 ? 1 : 0; }
    }
}
