using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //다른 스크립트에서 TurretBlueprint형 변수를 만들면 변수들을 수정할수있음
// -> 현재 Shop에서 수정중

public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;
}
