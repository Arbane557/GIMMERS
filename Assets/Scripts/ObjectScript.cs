using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    [SerializeField]
    private Sprite newSprite; // Sprite baru untuk objek ini (tidak digunakan dalam skrip ini, tetapi mungkin relevan di masa depan)
    private GameObject candle; // Referensi ke objek lilin dalam permainan

    private void Start()
    {
        // Memulai korutine untuk menghancurkan objek secara otomatis setelah waktu tertentu
        StartCoroutine(decay());
    }
    private void OnMouseDown()
    {
        // Dipanggil saat objek diklik dengan mouse
        // Mencari objek dengan tag "candle" dan memicu efek penyembuhan
        candle = GameObject.FindGameObjectWithTag("candle");
        Picked();
    }
    // Fungsi untuk memproses logika saat objek diambil
    public void Picked()
    {
        // Menambahkan poin penyembuhan ke lilin melalui komponen CandleBehaviour
        candle.GetComponent<CandleBehaviour>().currentHeal += 5;
        // Menghancurkan objek ini setelah diambil
        Destroy(this.gameObject);
    }
    IEnumerator decay()
    {
        yield return new WaitForSeconds(3); // Tunggu selama 3 detik
        Destroy(this.gameObject); // Hancurkan objek ini
    }
}
