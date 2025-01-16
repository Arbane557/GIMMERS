using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LaneBehaviour : MonoBehaviour
{ 
    // Kelas Lanes adalah struktur data untuk menyimpan informasi tentang tiap jalur
    [System.Serializable]
    private class Lanes
    {
        public GameObject LaneRend; // Objek renderer untuk menampilkan jalur
        public GameObject LaneCol; // Collider untuk mendeteksi interaksi
        public GameObject LaneAnim; // Objek animasi untuk jalur
    }
    [SerializeField]
    private List<Lanes> Lane; // Daftar jalur yang akan dikelola
    [SerializeField] int redlaneCount = 0; // Hitungan jalur merah aktif
    [SerializeField] AudioClip[] atkwind; // Array klip suara untuk efek serangan
    [SerializeField] AudioSource aud; // Sumber audio untuk memutar klip suara
    private void Start()
    {
        // Memulai korutine untuk secara dinamis menghasilkan jalur
        StartCoroutine(generateLanes());
    }
    // Korutine untuk mengatur logika pengelolaan jalur
    IEnumerator generateLanes()
    {
        while (true) {
            redlaneCount = 0; // Reset jumlah jalur merah
            // Loop melalui daftar jalur untuk menentukan status masing-masing
            foreach (Lanes go in Lane) 
            {
                bool val = (Random.Range(0, 2) == 0); // Random boolean untuk menentukan apakah jalur aktif    
                if (redlaneCount < 3 && val) {
                    redlaneCount++;
                    go.LaneRend.SetActive(val); // Aktifkan renderer jalur jika kondisi terpenuhi                  
                }   
                else go.LaneRend.SetActive(false); // Nonaktifkan renderer jalur
            }
            // Jika hanya ada satu jalur merah yang aktif
            if (redlaneCount == 1)
            {
                foreach (Lanes col in Lane)
                {
                    col.LaneRend.SetActive(false); // Nonaktifkan renderer jalur
                    col.LaneCol.SetActive(true); // Aktifkan collider jalur
                    col.LaneCol.GetComponent<HitCollider>().isChase = true; // Tetapkan status pengejaran
                }
                // Tunggu beberapa detik sebelum memproses logika berikutnya
                yield return new WaitForSeconds(3);
                foreach (Lanes col in Lane)
                {
                    col.LaneCol.GetComponent<HitCollider>().isChase = false; // Matikan status pengejaran
                    col.LaneCol.SetActive(false); // Nonaktifkan collider
                }
                // Mainkan efek suara untuk serangan
                aud.PlayOneShot(atkwind[Random.Range(0, atkwind.Length)]);
                foreach (Lanes col in Lane)
                {
                    if (col.LaneRend.gameObject.activeSelf)
                    {
                        col.LaneRend.SetActive(false); // Nonaktifkan renderer jalur
                        yield return new WaitForSeconds(0.5f);
                        col.LaneAnim.SetActive(true); col.LaneCol.SetActive(true); // Aktifkan collider dan Aktifkan animasi jalur
                        yield return new WaitForSeconds(0.2f);
                        col.LaneCol.SetActive(false); // Nonaktifkan collider
                        yield return new WaitForSeconds(1);
                        col.LaneAnim.SetActive(false); // Nonaktifkan animasi
                    }
                }
            }
            else
            {
                // Logika untuk lebih dari satu jalur merah aktif
                yield return new WaitForSeconds(1);
                foreach (Lanes col in Lane)
                {
                    if (col.LaneRend.gameObject.activeSelf) col.LaneRend.GetComponent<RawImage>().enabled = false; // Sembunyikan gambar renderer
                }
                if (redlaneCount > 0) aud.PlayOneShot(atkwind[Random.Range(0, atkwind.Length)]); // Mainkan efek suara
                yield return new WaitForSeconds(0.5f);
                foreach (Lanes col in Lane)
                {
                    if (col.LaneRend.gameObject.activeSelf)
                    { col.LaneCol.SetActive(true); col.LaneAnim.SetActive(true); } // Aktifkan collider dan animasi
                    col.LaneRend.GetComponent<RawImage>().enabled = true; // Tampilkan gambar renderer
                    col.LaneRend.SetActive(false); // Nonaktifkan renderer
                }
                yield return new WaitForSeconds(0.2f);
                foreach (Lanes col in Lane) col.LaneCol.SetActive(false); // Nonaktifkan collider
                yield return new WaitForSeconds(1f);
                foreach (Lanes col in Lane) col.LaneAnim.SetActive(false); // Nonaktifkan animasi
            }
            }
    }
 }
