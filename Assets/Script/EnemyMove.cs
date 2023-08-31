using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
// Enemy컴포넌트를 자동으로 추가
public class EnemyMove : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] int wavepointIndex = 0;

    [SerializeField] Enemy enemy;

    void Start()
    {
        target = Waypoints.points[0];
        //처음에는
        // Waypoint스크립트의 points이라는 Transform 배열
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position; //타겟(waypoint)의 위치와 자기자신(enemy)의 위치 사이의 거리 
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World); // Translate : 오브젝트 이동

        // enemy의 위치와  타겟(waypoint)의 위치의 거리가 일정거리 이하가 되면?
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }

        enemy.speed = enemy.startSpeed; //레이져를 벗어나면 원래속도로

    }

    void GetNextWayPoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)  //index가 배열의 인덱스보다 커지면
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];

    }

    //경로가 끝나면
    void EndPath()
    {
        PlayerStats.Lives--; // PlayerStats스크립트의 static형 Lives변수
        Destroy(gameObject);
    }


}
