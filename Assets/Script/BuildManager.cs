using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; //�̱��� ����
    public GameObject standardTurretPrefab; //��ġ�� Turret ������ 
    public GameObject missileLauncherPrefab;
    public GameObject buildEffect;

    [SerializeField] TurretBlueprint turretToBuild; // ó���� null 

    //BuildManager�� �̱�������
    private void Awake()
    {
        if (instance != null) 
        {
            Debug.LogError("BuildManager �̱��� ������");
            return;
        }
        instance = this;
    }

    public void SelecetTurretToBuid(TurretBlueprint turret) 
    {
        turretToBuild = turret;
    }


    //Node���� ������ �ִ��� ������ �˻縦 Manager���� 
    public bool CanBuild 
    {
        get { return turretToBuild != null;}
    }
    //����� ���� �ִ���
    public bool HasMoney
    {
        get { return PlayerStats.Money >= turretToBuild.cost; }
    }

    //Ư�� node�� ��ġ
    public void builTrretOn(Node node) 
    {
        //currency ����
        if (PlayerStats.Money < turretToBuild.cost) //Playerstats��� ��ũ��Ʈ�� static����
        {
            Debug.Log("Not Enough Money to build that");
            return;
        }
        PlayerStats.Money -= turretToBuild.cost;

        // turret��ġ
        GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition() , Quaternion.identity);
        node.turret = turret; // Node�� ��ġ�Ǿ� �ִ� turret

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect , 5f);
    }


    /*
    public GameObject GetTurretToBuild() // GameMager�� �巡�� �� turret�� <- �� �޼��带 ȣ���ϸ� ��ȯ��
    {
        return turretToBuild;
    }
    */
}
