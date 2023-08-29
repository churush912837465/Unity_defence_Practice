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

    public GameObject turret; //��ġ�� �ͷ� (ó������ null)

    [SerializeField] Renderer rend; //Node�� Renderer
    [SerializeField] Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    private void OnMouseDown() //���콺�� ������
    {
        // Ui ��� ���� ���콺�� ���������� Ȯ��
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (!buildManager.CanBuild)
            return;

        if (turret != null) 
        {
            Debug.Log("Can not build here");
            return;
        }

        buildManager.builTrretOn(this); //Node ����
        //�ͷ��� ����������� ��ġ���� -> BuildManeger�� �ű�
        /*
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild(); // BuildManager��ũ��Ʈ���� ������ �ִ� turret������
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset , transform.rotation);
        */
    }

    private void OnMouseEnter() // ���콺�� hover �ɶ�
    {
        // Ui ��� ���� ���콺�� ���������� Ȯ��
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

    private void OnMouseExit() //���콺�� ����������
    {
        rend.material.color=startColor;
    }

    //������� ��ġ�� position�������� -> BuildManager���� ���
    public Vector3 GetBuildPosition() 
    {
        return transform.position + positionOffset;
    }
}
