using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;

    private void Awake()
    {
        points = new Transform[transform.childCount]; // transform으로 자식 오브젝트를 찾아온다 'transform.childCount'
        for (int i = 0; i < points.Length; i++) 
        {
            points[i] = transform.GetChild(i);
            // points 배열 -> 총 13개의 자식 게임오브젝트의 Transform을 가지고 있음
        }
    }


}
