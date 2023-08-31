using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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


    [Header("Layser")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    public LineRenderer lineRenderer;
    public ParticleSystem impactImpact; // 파티클 Play와 Stop실행안됨 - 고쳐야함
    public float slowPercent = 0.5f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

    }

    void Update()
    {
        if (target == null)
        {
            //라인렌더러
            if (useLaser) 
            {
                if (lineRenderer.enabled) 
                {
                    lineRenderer.enabled = false;
                    impactImpact.Stop(); // 레이져 이펙트
                }
            }

            return;
        }

        tarretRotate(); // 터렛 회전

        if (useLaser)
        {
            if (targetEnemy != null) 
            {
                Debug.Log("Laser함수 실행!");
                Laser();
            }
        }
        else 
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate; // 1초에 N번 발사
            }
            fireCountdown -= Time.deltaTime;
        }


    }

    // 레이져
    // 라인렌더러 사용
    void Laser() 
    {
        //데미지 주기
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPercent);

        if (!lineRenderer.enabled) 
        {
            lineRenderer.enabled = true;
            impactImpact.Play(); //레이져 이펙트 
        
        }

        lineRenderer.SetPosition(0 , firePoint.position);
        lineRenderer.SetPosition(1 , target.position);

        // 레이져가 나가서 enemy에 닿이는 방향으로 impact가 생성되야함
        Vector3 dir = firePoint.position - target.position; //layser을 향해 다시 가는 dir
        impactImpact.transform.position = target.position + dir.normalized * 0.5f;
        impactImpact.transform.rotation = Quaternion.LookRotation(dir); //impact에서 회전은 터렛을 바라봄

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
