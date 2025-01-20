using System.Collections; // mengimport namespace yang menyediakan kelas untuk mengelola koleksi data
using System.Collections.Generic; //mengimport namespace yang menyediakan koleksi data generik 
using UnityEngine; // mengimport UnityEngine untuk mengakses fungsi dan kelas Unity
using UnityEngine.SceneManagement; // berfungsi untuk mengelola transisi dan kontrol scene dalam Unity

// Kelas yang bertanggung jawab untuk menangani aksi ketika game berakhir
public class GameOverMainMenu : MonoBehaviour
{
    // Script gameover, di mana game akan berhenti total ketika player burung menabrak obstacle
    public void GameOver()
    {
        SceneManager.LoadSceneAsync(0);
    }

}

