using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;


public class SettingsMenuManager : MonoBehaviour
{
    public TextMeshProUGUI brightnessPercentage; // Referensi ke teks UI untuk menampilkan persentase kecerahan
    private SpriteRenderer[] spriteRenderers; // Array untuk menyimpan semua komponen SpriteRenderer di scene
    private void Start()
    {
        // Mencari semua komponen SpriteRenderer di scene dan menyimpannya di array
        spriteRenderers = FindObjectsOfType<SpriteRenderer>();
    }

    public void AdjustBrightness(float BrightnessValue) // Fungsi untuk menyesuaikan kecerahan sprite berdasarkan nilai yang diberikan
    {
        // Perbarui teks persentase kecerahan di UI
        brightnessPercentage.text = (BrightnessValue * 100).ToString("F0") + "%"; 

        // Iterasi melalui semua SpriteRenderer yang ditemukan
        foreach(SpriteRenderer spriteRenderer in spriteRenderers)
        {
            // Perbarui warna sprite berdasarkan nilai kecerahan yang diberikan
            // Kecerahan memengaruhi komponen RGB, tetapi tidak mengubah nilai alpha (transparansi)
            spriteRenderer.color= new Color(BrightnessValue, BrightnessValue, BrightnessValue, spriteRenderer.color.a);
        }
    }
}
