using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
// Enemy������Ʈ�� �ڵ����� �߰�
public class EnemyMove : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] int wavepointIndex = 0;

    [SerializeField] Enemy enemy;

    void Start()
    {
        target = Waypoints.points[0];
        //ó������
        // Waypoint��ũ��Ʈ�� points�̶�� Transform �迭
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position; //Ÿ��(waypoint)�� ��ġ�� �ڱ��ڽ�(enemy)�� ��ġ ������ �Ÿ� 
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World); // Translate : ������Ʈ �̵�

        // enemy�� ��ġ��  Ÿ��(waypoint)�� ��ġ�� �Ÿ��� �����Ÿ� ���ϰ� �Ǹ�?
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }

        enemy.speed = enemy.startSpeed; //�������� ����� �����ӵ���

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


}
