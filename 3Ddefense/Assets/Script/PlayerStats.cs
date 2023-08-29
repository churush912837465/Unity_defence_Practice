using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header ("Money")]
    public static int Money;
    public int startMoney = 400;

    [Header("Live")]
    public static int Lives;
    public int startLives = 12;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }

    
}
