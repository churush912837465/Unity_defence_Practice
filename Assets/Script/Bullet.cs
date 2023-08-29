using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Transform target;

    public float speed = 70f;
    public GameObject effect;

    public float explosionRadius = 0f; // ���߹���
    public int damage = 15; //�Ѿ� ������

    public void Seek(Transform _target) // turret�� ã�� target�� ������ ������
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
            return; // return�� �ְ���� ���̰� �ֳ�����?
        }


        Vector3 dir = target.position - transform.position; //��ǥ vector3
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) // ���⼭ '�浹����'�� ��!!!! ��� �ϴ°���?
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

        //���� ���� �ȿ� �ִ� enemy���� �������� ��
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else 
        {
            //�����ȿ� ������? only Ÿ�ٿ��Ը� ������
            Damage(target);
        }


        Destroy(gameObject);
    }


    void Explode() 
    {
        Collider[] coliiders = Physics.OverlapSphere(transform.position ,explosionRadius);
        // OverlapSphere : �� �ݰ� ���� collider�� Ž���Ҽ� ����!
        // OverlapSphere(���� ��ġ , ������ ���� , (Ž���� ���̾�))

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

