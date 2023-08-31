using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float health=100f;
    public int value = 50;

    void Start()
    {
        speed = startSpeed;
    }

    //������ �Լ�
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        PlayerStats.Money += value; //PlayerStats ��ũ��Ʈ�� static�� Monvey�� �� ���ϱ�
        Destroy(gameObject);
    }

    public void Slow(float percent) 
    {
        speed = startSpeed * (1f-percent);
    }
}
