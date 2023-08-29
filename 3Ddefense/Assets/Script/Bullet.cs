using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Transform target;

    public float speed = 70f;
    public GameObject effect;

    public float explosionRadius = 0f; // 폭발범위
    public int damage = 15; //총알 데미지

    public void Seek(Transform _target) // turret이 찾은 target의 정보를 가져옴
    {
        target = _target;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) 
        {
            Destroy(gameObject);
            return; // return이 있고없고 차이가 있나본데?
        }


        Vector3 dir = target.position - transform.position; //목표 vector3
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) // 여기서 '충돌감지'를 함!!!! 어떻게 하는거지?
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget() 
    {
        GameObject effectins = (GameObject)Instantiate(effect , transform.position , transform.rotation);
        Destroy(effectins ,5f);

        //폭발 범위 안에 있는 enemy에게 데미지를 줌
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else 
        {
            //범위안에 없으면? only 타겟에게만 데미지
            Damage(target);
        }


        Destroy(gameObject);
    }


    void Explode() 
    {
        Collider[] coliiders = Physics.OverlapSphere(transform.position ,explosionRadius);
        // OverlapSphere : 구 반경 내의 collider을 탐지할수 있음!
        // OverlapSphere(구의 위치 , 감지할 범위 , (탐지할 레이어))

        foreach (Collider collider in coliiders) 
        {
            if (collider.CompareTag("Enemy")) 
            {
                Damage(collider.transform);
            }
        }
    }
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null) 
        {
            e.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}

