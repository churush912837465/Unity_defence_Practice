using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    BuildManager buildManeger;

    void Start()
    {
        buildManeger = BuildManager.instance;
    }

    public void SelecetStandardTurret()  
    {
        Debug.Log("Standard Turret ����!");

        buildManeger.SelecetTurretToBuid(standardTurret);
    }

    public void SelecetMissileLauncher()
    {
        Debug.Log("Missile ����!");

        buildManeger.SelecetTurretToBuid(missileLauncher);
    }


}
