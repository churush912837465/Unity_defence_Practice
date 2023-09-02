using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; //싱글톤 변수
    public GameObject buildEffect;

    [SerializeField] TurretBlueprint turretToBuild; // 처음엔 null 
    [SerializeField] Node selectNode; //select한 노드 - 지금은 작동안함 ㅠㅠ
    public NodeUi nodeUI;

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

    //지을 터렛
    public void SelecetTurretToBuid(TurretBlueprint turret) 
    {
        turretToBuild = turret;
        selectNode = null; //왜 null해줘야 하는거징
    }
    //노드 가져오기
    public void SelectNode(Node node)
    {
        selectNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }


    //Node에서 지을수 있는지 없는지 검사를 Manager에서 
    public bool CanBuild 
    {
        get { return turretToBuild != null;}
    }
    //충분한 돈이 있는지
    public bool HasMoney
    {
        get { return PlayerStats.Money >= turretToBuild.cost; }
    }

    //특정 node에 설치
    public void builTrretOn(Node node) 
    {
        //currency 관리
        if (PlayerStats.Money < turretToBuild.cost) //Playerstats라는 스크립트의 static변수
        {
            Debug.Log("Not Enough Money to build that");
            return;
        }
        PlayerStats.Money -= turretToBuild.cost;

        // turret설치
        GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition() , Quaternion.identity);
        node.turret = turret; // Node에 설치되어 있는 turret

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect , 5f);
    }


    /*
    public GameObject GetTurretToBuild() // GameMager에 드래그 된 turret이 <- 이 메서드를 호출하면 반환됨
    {
        return turretToBuild;
    }
    */
}
