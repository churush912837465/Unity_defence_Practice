using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; //싱글톤 변수
    public GameObject standardTurretPrefab; //설치할 Turret 프리팹 
    public GameObject missileLauncherPrefab;
    
    [SerializeField] GameObject turretToBuild; // 처음엔 null 


    //BuildManager을 싱글톤으로
    private void Awake()
    {
        if (instance != null) 
        {
            Debug.LogError("BuildManager 싱글톤 오류남");
            return;
        }
        instance = this;
    }

    public void SetTurretToBuid(GameObject turret) 
    {
        turretToBuild = turret;
    }


    public GameObject GetTurretToBuild() // GameMager에 드래그 된 turret이 <- 이 메서드를 호출하면 반환됨
    {
        return turretToBuild;
    }
}
