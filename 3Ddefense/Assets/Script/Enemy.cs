using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField] Transform target;
    [SerializeField] int wavepointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
        //처음에는
        // Waypoint스크립트의 points이라는 Transform 배열
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position; //타겟(waypoint)의 위치와 자기자신(enemy)의 위치 사이의 거리 
        transform.Translate( dir.normalized * speed * Time.deltaTime , Space.World); // Translate : 오브젝트 이동

        // enemy의 위치와  타겟(waypoint)의 위치의 거리가 일정거리 이하가 되면?
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }

    }

    void GetNextWayPoint() 
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)  //index가 배열의 인덱스보다 커지면
        {
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];

    }

}
