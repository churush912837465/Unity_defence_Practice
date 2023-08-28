
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
        if (countdown <= 0f) //�ð��� 0���ϰ� �Ǹ� wave���� 
        {
            StartCoroutine("SpawnWave"); // enemy ����
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime; //�ð��� ��� �پ���
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00:00}" , countdown);
    }

    IEnumerator SpawnWave() 
    {
        Debug.Log("Wave is Comming!");
        waveIdex++;
        for (int i = 0; i < waveIdex; i++) // waveidex�ø��鼭 �����Ǵ� enemy���� �ϳ��� ������
        {
            SpawnEnemy(); //���� �� 0.5f ��� 
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy() 
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
