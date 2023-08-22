using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] bool doMovement = true;

    public float panSpeed = 30f; //�����̴� �ӵ�
    public float panForderThickness = 10f;
    //ȭ�鿡 ����������� ���콺�� ������ ��� ī�޶� �����̰� �ϴ� ����

    public float scrollspeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    void Update()
    {
        cameraMove();
        scrollCamera();

    }
    void scrollCamera()
    {
        // ī�޶� ��ũ�� �ϱ�
        // input ������ ���� ��ũ���Է� �ϴ� �Է� 
        // float ���·� : ������, ������, ����, ���� ���� ��ũ���� �ϱ����ؼ�
        // scoll�� ���� ��ũ���ϸ� + , �Ʒ��� ��ũ�� �ϸ� -
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollspeed * Time.deltaTime; // ��ũ���� �۾Ƽ�? 1000���ϱ�
        pos.y = Mathf.Clamp(pos.y, minY, maxY); // Mathf.Cplam(��ġ, �ּ�, �ִ�) : pos.y�� max�� min���̷� ������

        transform.position = pos;
    }

    // wasd , ���콺�� ī�޶� �̵�
    void cameraMove() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            doMovement = !doMovement;
        }
        if (!doMovement)
        {
            return;
        }
        // w a s d Ű �Է�
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panForderThickness)
        {
            // ī�޶� ���� ������ �ӵ��� �������̰� �ʹ�! -> time.delatime����
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            // Vector(0f , 0f , 1f) * panSpeed = forward * panSpeed
            // Space.World -> world��ǥ���� ī�޶� �̵� (ī�޶��� ȸ���� ������?)
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panForderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panForderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panForderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

    }

}
