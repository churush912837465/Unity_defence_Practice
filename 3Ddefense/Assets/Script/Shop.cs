using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManeger;

    void Start()
    {
        buildManeger = BuildManager.instance;
    }

    public void PurchaseStandardTurret() 
    {
        Debug.Log("Standard Turret 선택!");

        buildManeger.SetTurretToBuid(buildManeger.standardTurretPrefab);
    }

    public void PurchaseMissileLauncher()
    {
        Debug.Log("Missile 선택!");

        buildManeger.SetTurretToBuid(buildManeger.missileLauncherPrefab);
    }


}
