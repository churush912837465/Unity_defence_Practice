using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform target;

    [Header("General")]
    public float range = 15f; //���ݹ���
    public string enemyTag = "Enemy";
    [SerializeField] Enemy targetEnemy;

    [Header("Rotate")]
    public Transform partToRotate;
    public float trunSpeed = 10f;

    [Header("Shoot")]
    public float fireRate = 1f;
    [SerializeField] float fireCountdown = 0f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

    }

    void Update()
    {
        if(target == null) 
        {
            return;
        }

        tarretRotate(); // �ͷ� ȸ��

        if (fireCountdown <= 0f) 
        {
            Shoot();
            fireCountdown = 1f / fireRate; // 1�ʿ� N�� �߻�
        }
        fireCountdown -= Time.deltaTime;
    }

    //���
    void Shoot() 
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>(); //Bullet ��ũ��Ʈ ��������

        if(bullet != null) 
        {
            bullet.Seek(target); // Turret �� ã�� target�� ������ bullet���� �ѱ�
        }
        
    }


    //ȸ�� ����
    void tarretRotate() 
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation , lookRotation , Time.deltaTime * trunSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void UpdateTarget() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // �迭�ȿ��� �ּ� �Ÿ��� ����
        foreach (GameObject enemy in enemies) 
        {
            // enemies �迭�ȿ� �ִ� ���ӿ�����Ʈ��� , �ڱ��ڽ��� �Ÿ��� ����
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance) 
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy; 
            }
        }

        // target�� ���� ����� traget�� transform������
        // Enemy���� targetEnemy�� ���� ����� enemy�� Enemy ��ũ��Ʈ�� ������  
        if (nearestEnemy != null & shortestDistance <= range) // ���ݹ����ȿ� ������
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else 
        {
            target = null;
        }
    }

    void OnDrawGizmosSelected() // Select�� ��쿡�� Gizmos�� �׸���
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position , range);
    }
}
