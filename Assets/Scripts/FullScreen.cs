using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Kelas yang bertanggung jawab untuk menangani aksi ketika game berakhir
public class GameOverMainMenu : MonoBehaviour
{
    // Script gameover, di mana game akan berhenti total ketika player burung menabrak obstacle
    public void GameOver()
    {
        SceneManager.LoadSceneAsync(0); // Mengganti scene saat ini dengan scene utama secara asinkron
    }

}