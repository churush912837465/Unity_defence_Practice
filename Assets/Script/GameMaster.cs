using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // Brakeys강의에서는 GameManager 스크립트로
    // 종료 / 다시 불러오기 등 기능

    private bool gameEnded = false;

    // Update is called once per frame
    void Update()
    {
        if (gameEnded) 
        {
            return;
        }

        if (PlayerStats.Lives <= 0) 
        {
            EndGame();

        }

    }

    void EndGame() 
    {
        gameEnded = true;
        Debug.Log("게임오버");

    }


}
