
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    [SerializeField] float countdown = 2f;
    [SerializeField] float waveIdex = 0;

    public TextMeshProUGUI waveCountdownText;

    void Start()
    {
        
    }

    void Update()
    {
        if (countdown <= 0f) //시간이 0이하가 되면 wave시작 
        {
            StartCoroutine("SpawnWave"); // enemy 생성
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime; //시간이 계속 줄어들게
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00:00}" , countdown);
    }

    IEnumerator SpawnWave() 
    {
        Debug.Log("Wave is Comming!");
        waveIdex++;
        for (int i = 0; i < waveIdex; i++) // waveidex늘리면서 생성되는 enemy수도 하나씩 증가함
        {
            SpawnEnemy(); //생성 후 0.5f 대기 
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy() 
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
