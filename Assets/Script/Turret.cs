using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform target;

    [Header("General")]
    public float range = 15f; //공격범위
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

        tarretRotate(); // 터렛 회전

        if (fireCountdown <= 0f) 
        {
            Shoot();
            fireCountdown = 1f / fireRate; // 1초에 N번 발사
        }
        fireCountdown -= Time.deltaTime;
    }

    //사격
    void Shoot() 
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>(); //Bullet 스크립트 가져오기

        if(bullet != null) 
        {
            bullet.Seek(target); // Turret 이 찾은 target의 정보를 bullet에게 넘김
        }
        
    }


    //회전 설정
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

        // 배열안에서 최소 거리를 구함
        foreach (GameObject enemy in enemies) 
        {
            // enemies 배열안에 있는 게임오브젝트들과 , 자기자신의 거리를 구함
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance) 
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy; 
            }
        }

        // target을 가장 가까운 traget의 transform값으로
        // Enemy변수 targetEnemy를 가장 가까운 enemy의 Enemy 스크립트를 가져옴  
        if (nearestEnemy != null & shortestDistance <= range) // 공격범위안에 들어오면
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else 
        {
            target = null;
        }
    }

    void OnDrawGizmosSelected() // Select한 경우에면 Gizmos를 그린다
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position , range);
    }
}
