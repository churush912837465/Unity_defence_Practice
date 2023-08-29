using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint LayerBeamer;
    BuildManager buildManeger;

    void Start()
    {
        buildManeger = BuildManager.instance;
    }

    public void SelecetStandardTurret()  
    {
        Debug.Log("Standard Turret 선택!");

        buildManeger.SelecetTurretToBuid(standardTurret);
    }

    public void SelecetMissileLauncher()
    {
        Debug.Log("Missile 선택!");

        buildManeger.SelecetTurretToBuid(missileLauncher);
    }

    public void SelecetLayerBeamer()
    {
        Debug.Log("Layer Beamer 선택!");

        buildManeger.SelecetTurretToBuid(LayerBeamer);
    }


}
