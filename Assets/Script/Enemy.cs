using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField] Transform target;
    [SerializeField] int wavepointIndex = 0;

    public int health=100;
    public int value = 50;

    void Start()
    {
        target = Waypoints.points[0];
        //ó������
        // Waypoint��ũ��Ʈ�� points�̶�� Transform �迭
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position; //Ÿ��(waypoint)�� ��ġ�� �ڱ��ڽ�(enemy)�� ��ġ ������ �Ÿ� 
        transform.Translate( dir.normalized * speed * Time.deltaTime , Space.World); // Translate : ������Ʈ �̵�

        // enemy�� ��ġ��  Ÿ��(waypoint)�� ��ġ�� �Ÿ��� �����Ÿ� ���ϰ� �Ǹ�?
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }

    }

    void GetNextWayPoint() 
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)  //index�� �迭�� �ε������� Ŀ����
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];

    }
    
    //��ΰ� ������
    void EndPath() 
    {
        PlayerStats.Lives--; // PlayerStats��ũ��Ʈ�� static�� Lives����
        Destroy(gameObject);
    }


    //������ �Լ�
    public void TakeDamage(int amount)
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

}
