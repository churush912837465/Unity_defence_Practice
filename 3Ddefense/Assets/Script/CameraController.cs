using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] bool doMovement = true;

    public float panSpeed = 30f; //움직이는 속도
    public float panForderThickness = 10f;
    //화면에 어느정도까지 마우스를 가져다 대면 카메라가 움직이게 하는 범위

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
        // 카메라 스크롤 하기
        // input 설정에 가서 스크롤입력 하는 입력 
        // float 형태로 : 빠르게, 느리게, 조금, 많이 등의 스크롤을 하기위해서
        // scoll은 위로 스크롤하면 + , 아래로 스크롤 하면 -
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollspeed * Time.deltaTime; // 스크롤이 작아서? 1000곱하기
        pos.y = Mathf.Clamp(pos.y, minY, maxY); // Mathf.Cplam(위치, 최소, 최대) : pos.y는 max와 min사이로 결졍됨

        transform.position = pos;
    }

    // wasd , 마우스로 카메라 이동
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
        // w a s d 키 입력
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panForderThickness)
        {
            // 카메라가 현재 프레임 속도와 독립적이고 싶다! -> time.delatime곱함
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            // Vector(0f , 0f , 1f) * panSpeed = forward * panSpeed
            // Space.World -> world좌표에서 카메라 이동 (카메라의 회전을 무시함?)
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
