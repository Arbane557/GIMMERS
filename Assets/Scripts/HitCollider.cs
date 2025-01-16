using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    [SerializeField] private GameObject Lanerends; // Objek yang diaktifkan saat kondisi "chase" terjadi
    [SerializeField] private Color hurtColor; // Warna yang digunakan untuk menandai lilin saat terkena serangan
    [SerializeField] private Color normal; // Warna normal lilin
    [SerializeField] AudioSource aud; // Sumber audio untuk memutar suara saat terkena serangan
    [SerializeField] AudioClip hurtsound; // Klip suara yang dimainkan saat lilin terkena serangan
    public bool isChase; // Menentukan apakah kondisi pengejaran aktif
    // Dipanggil saat lilin masuk ke dalam collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah objek yang masuk memiliki tag "candle"
        if (collision.CompareTag("candle"))
        {
            if (isChase)
            {
                // Jika kondisi pengejaran aktif, aktifkan objek Lanerends
                Lanerends.SetActive(true);
            }
            else if (!isChase) {
                // Jika kondisi pengejaran tidak aktif, jalankan korutine untuk memproses serangan
                StartCoroutine(Hit(collision.gameObject));
            }           
        }
    }
    // Dipanggil saat lilin keluar dari collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Jika kondisi pengejaran aktif dan lilin keluar dari area collider, nonaktifkan objek Lanerends
        if (collision.CompareTag("candle")) if (isChase) Lanerends.SetActive(false);
    }
    IEnumerator Hit(GameObject collision)
    {
        // Referensi ke objek lilin
        var cdl = collision.gameObject;
        cdl.GetComponent<CandleBehaviour>().currFire.GetComponent<SpriteRenderer>().color = hurtColor; // Ubah warna nyala lilin menjadi warna "hurtColor" untuk menunjukkan bahwa lilin diserang
        yield return new WaitForSeconds(0.1f); // Tunggu selama 0.1 detik sebelum mengembalikan warna normal
        cdl.GetComponent<CandleBehaviour>().currFire.GetComponent<SpriteRenderer>().color = normal;
        cdl.GetComponent<CandleBehaviour>().HealthPoints -= 1; // Kurangi HealthPoints lilin sebanyak 1

        aud.PlayOneShot(hurtsound); // Putar suara saat lilin terkena serangan
        Debug.Log("hit"); // Tampilkan log di konsol untuk debugging
        cdl.GetComponent<CandleBehaviour>().hitCounter += 1; // Tambahkan hitCounter lilin sebanyak 1

    }
}
