using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject option; // Panel opsi yang ditampilkan saat permainan dijeda
    bool isPause; // Status apakah permainan sedang dijeda
    [SerializeField]
    private GameObject pauseButton; // Tombol jeda di UI
    [SerializeField]
    private Texture2D[] pauseSprite; // Array sprite untuk tombol jeda (ikon jeda dan lanjutkan)

    private void Update()
    {
        // Deteksi tombol Escape untuk mengaktifkan atau menonaktifkan jeda
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
 // Atur kecepatan waktu berdasarkan status jeda
        if (isPause) Time.timeScale = 0; // Hentikan waktu saat jeda
        else Time.timeScale = 1; // Lanjutkan waktu jika tidak jeda
    }
    // Fungsi untuk kembali ke layar judul (scene dengan indeks 0)
    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0); // Muat ulang scene utama
    }
    // Fungsi untuk mengaktifkan atau menonaktifkan jeda
    public void Pause()
    {
        // Aktifkan jeda: Ubah ikon tombol jeda dan tampilkan panel opsi dan else // Nonaktifkan jeda: Ubah ikon tombol jeda dan sembunyikan panel opsi
        if (!isPause) { pauseButton.GetComponent<RawImage>().texture = pauseSprite[0]; isPause = true; option.SetActive(true); } else { pauseButton.GetComponent<RawImage>().texture = pauseSprite[1]; isPause = false; option.SetActive(false); }
    }
    // Fungsi untuk memulai permainan (scene dengan indeks 1)
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1); // Muat scene permainan secara asinkron
    }
    // Fungsi untuk keluar dari permainan
    public void QuitGame() 
    {
        Application.Quit(); // Keluar dari aplikasi
    }

}
