using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMainMenu : MonoBehaviour
{
    // Script gameover, di mana game akan berhenti total ketika player burung menabrak obstacle
    public void GameOver()
    {
        SceneManager.LoadSceneAsync(0);
    }

}