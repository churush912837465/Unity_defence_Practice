using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    public GameObject turret; //설치할 터렛 (처음에는 null)

    [SerializeField] Renderer rend; //Node의 Renderer
    [SerializeField] Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    private void OnMouseDown() //마우스를 누를때
    {
        // Ui 요소 위로 마우스를 가져가는지 확인
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (!buildManager.CanBuild)
            return;

        if (turret != null) 
        {
            Debug.Log("Can not build here");
            return;
        }

        buildManager.builTrretOn(this); //Node 전달
        //터렛이 비어있을때만 설치가능 -> BuildManeger로 옮김
        /*
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild(); // BuildManager스크립트에서 가지고 있는 turret프리팹
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset , transform.rotation);
        */
    }

    private void OnMouseEnter() // 마우스가 hover 될때
    {
        // Ui 요소 위로 마우스를 가져가는지 확인
        if (EventSystem.current.IsPointerOverGameObject()) 
            return;


        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else 
        {
            rend.material.color = notEnoughMoneyColor;
        }

    }

    private void OnMouseExit() //마우스가 빠져나갈때
    {
        rend.material.color=startColor;
    }

    //노드위에 설치할 position가져오기 -> BuildManager에서 사용
    public Vector3 GetBuildPosition() 
    {
        return transform.position + positionOffset;
    }
}
