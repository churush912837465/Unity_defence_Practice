using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; //�̱��� ����
    public GameObject standardTurretPrefab; //��ġ�� Turret ������ 
    public GameObject missileLauncherPrefab;
    
    [SerializeField] GameObject turretToBuild; // ó���� null 


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

    public void SetTurretToBuid(GameObject turret) 
    {
        turretToBuild = turret;
    }


    public GameObject GetTurretToBuild() // GameMager�� �巡�� �� turret�� <- �� �޼��带 ȣ���ϸ� ��ȯ��
    {
        return turretToBuild;
    }
}
