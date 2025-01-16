using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; // Referensi ke slider UI yang digunakan untuk mengatur volume
    // Start is called before the first frame update
    void Start()
    {
        // Periksa apakah kunci "musicVolume" sudah disimpan sebelumnya di PlayerPrefs
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            // Jika tidak ada, atur nilai default volume ke 1 (maksimal)
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load(); // Muat nilai default ke slider
        }

        else 
        {
            // Jika sudah ada, langsung muat nilai yang tersimpan
            Load();
        }
    }

    public void ChangeVolume() // Fungsi yang dipanggil saat slider volume diubah oleh pengguna
    {
        // Perbarui volume global menggunakan nilai slider
        AudioListener.volume = volumeSlider.value;
        Save(); // Simpan perubahan ke PlayerPrefs
    }

    private void Load() // Fungsi untuk memuat nilai volume yang tersimpan ke dalam slider
    {
        // Ambil nilai volume dari PlayerPrefs dan tetapkan ke slider
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save() // Fungsi untuk menyimpan nilai volume saat ini ke PlayerPrefs
    {
        // Simpan nilai slider ke PlayerPrefs dengan kunci "musicVolume"
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
