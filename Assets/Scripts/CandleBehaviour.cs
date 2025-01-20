using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.Experimental.Rendering.Universal; // Library untuk rendering Universal RP
using UnityEngine.Rendering.Universal; 
using System.Diagnostics.Tracing; // Library untuk event tracing
using TMPro; //Library untuk mengimpor TextMesh Pro
using Unity.VisualScripting; // Library Visual Scripting untuk Unity
using System; // Library untuk utilitas .NET
using UnityEngine.SceneManagement; // Berfungsi untuk manajemen scene di Unity
using UnityEngine.Rendering; // Library untuk rendering pipeline
using Unity.Mathematics; // Library untuk melakukan operasi matematika
public class CandleBehaviour : MonoBehaviour
{
// Deklarasi variabel untuk lilin dan status lainnya
    private Vector3 upScaled; // Skala lilin saat sedang di-drag
    private Vector3 normalScale; // Skala normal lilin
    private float normalhealScale; // Skala penyembuhan normal
    private Vector3 mousepos; // Posisi mouse di dunia
    public bool isDragging; // Status apakah lilin sedang di-drag
    private float TimeToHold; // Waktu untuk menghitung durasi drag
    public int HealthPoints = 10; // Poin kesehatan lilin
    [SerializeField]
    private List<Texture2D> BGStage; // Daftar tekstur background untuk berbagai tahap permainan
    [SerializeField]
    private RawImage ChangeBG; // Objek UI untuk mengganti background
    [SerializeField] private GameObject ChangeFG; // Foreground yang akan berubah
    [SerializeField] private GameObject HealBar; // Bar penyembuhan di UI
    public GameObject CandleLight; // Objek untuk representasi visual lilin
    private float healCooldown; // Cooldown untuk penyembuhan
    public float currentHeal; // Progres penyembuhan yang sedang berlangsung
    [SerializeField]
    private float timeSpent; // Waktu yang telah berlalu dalam permainan
    [SerializeField] private TextMeshProUGUI timerText; // Objek teks untuk menampilkan waktu
    [SerializeField] private GameObject EndScreen; // Layar akhir permainan
    [SerializeField] private GameObject UImain; // UI utama saat permainan berlangsung
    private float counter; // Hitung mundur untuk kembali ke menu utama
    public int hitCounter; // Menghitung jumlah pukulan atau interaksi
    private bool QTE; // Status Quick Time Event
    [SerializeField]
    private GameObject CamObj; // Referensi objek kamera
    [SerializeField] private RawImage heartFill; // Objek UI untuk tampilan health
    [SerializeField] private Texture2D fill1; // Tekstur hati dengan level kesehatan 1
    [SerializeField] private Texture2D fill2; // Tekstur hati dengan level kesehatan 2
    [SerializeField] private Texture2D fill3; // Tekstur hati dengan level kesehatan 3
    [SerializeField] private Texture2D fill4; // Tekstur hati dengan level kesehatan 4
    [SerializeField] private GameObject fire1; // Representasi api level 1
    [SerializeField] private GameObject fire2; // Representasi api level 2
    [SerializeField] private GameObject fire3; // Representasi api level 3
    public GameObject currFire; // Api aktif saat ini
    [SerializeField] private GameObject spawnFlare; // Objek untuk efek flare
    [SerializeField] private GameObject volumeBeat; // Objek untuk efek detak
    [SerializeField] AudioSource aud; // Objek untuk audio source
    [SerializeField] AudioClip clips; // Klip audio detak jantung
    [SerializeField] AudioClip clicks; // Klip audio klik mouse
    [SerializeField] AudioClip[] growl; // Array klip audio suara 

    private void Start()
    {
    // Inisialisasi variabel dan memulai efek audio detak jantung
        StartCoroutine(Beating()); 
        counter = 10; //Inisialisasi hitung mundur untuk ke menu utama
        healCooldown = -1; 
        currentHeal = 0;
        upScaled = transform.localScale * 1.2f; //Mengatur skala lilin saat di-drag
        normalScale = transform.localScale; //Mengatur skala normal lilin
        normalhealScale = HealBar.transform.localScale.x; // Mengambil nilai skala horizontal penyembuhan
    }
  
    private void Update()
    {
    // Periksa apakah Quick Time Event sedang aktif
        if (!QTE)
        {
            timerText.text = "" + timeSpent; // Menampilkan waktu yang telah berlalu pada UI
             // Periksa apakah waktu telah habis
            if (timeSpent >= 200)
            {
            // Logika akhir permainan
                spawnFlare.SetActive(false); //Menonaktifkan efek flare
                timeSpent = 200; //Waktu maksimum dibatasi ke 200
                currFire.GetComponent<SpriteRenderer>().enabled = false; // Mematikan sprite api saat ini
                CandleLight.GetComponent<SpriteRenderer>().enabled = false; // Mematikan sprite lilin
                UImain.SetActive(false); // Nonaktifkan UI utama
                EndScreen.SetActive(true);
                EndScreen.SetActive(true);
                EndScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You Survived The Corridor";
                counter -= Time.deltaTime;
                EndScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Returning in " + Mathf.RoundToInt(counter);
                if (counter <= 0) SceneManager.LoadScene(0); // Jika hitungan mundur selesai, kembali ke menu utama
            }
            if (CandleLight != null)
                CandleLight.transform.position = new Vector2(transform.position.x, CandleLight.transform.position.y);
            if (healCooldown < currentHeal)
            {
                healCooldown += Time.deltaTime*5;
                HealBar.transform.localScale = new Vector3(normalhealScale * healCooldown / 30, HealBar.transform.localScale.y, HealBar.transform.localScale.z);
            }
            if (HealthPoints <= 0)
            {
                spawnFlare.SetActive(false);
                currFire.GetComponent<SpriteRenderer>().enabled = false;
                CandleLight.GetComponent<SpriteRenderer>().enabled = false;
                ChangeBG.texture = BGStage[4];
                UImain.SetActive(false);
                EndScreen.SetActive(true);
                EndScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"You Only Survived {timeSpent} Hearbeats";
                counter -= Time.deltaTime;
                EndScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Returning in {Mathf.RoundToInt(counter)}";
                if (counter <= 0) SceneManager.LoadScene(0);
            }
            else if (HealthPoints <= 3) { aud.PlayOneShot(growl[UnityEngine.Random.Range(0,growl.Length)]); ChangeBG.texture = BGStage[3]; heartFill.texture = fill4; currFire = fire3; fire3.SetActive(true); fire2.SetActive(false); fire1.SetActive(false); }
            else if (HealthPoints <= 5) { aud.PlayOneShot(growl[UnityEngine.Random.Range(0, growl.Length)]); ChangeBG.texture = BGStage[2]; ChangeFG.SetActive(true); heartFill.texture = fill3; currFire = fire2; fire2.SetActive(true); fire1.SetActive(false); fire3.SetActive(false); }
            else if (HealthPoints <= 7) { aud.PlayOneShot(growl[UnityEngine.Random.Range(0, growl.Length)]); ChangeBG.texture = BGStage[1]; ChangeFG.SetActive(false); heartFill.texture = fill2; currFire = fire1; fire1.SetActive(true); fire2.SetActive(false); fire3.SetActive(false); }
            else if (HealthPoints <= 9) { ChangeBG.texture = BGStage[0]; heartFill.texture = fill1; }
            if (healCooldown >= 30 && HealthPoints < 10) { HealthPoints += 2; healCooldown = -1; currentHeal = 0; }
            mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (isDragging)
            {
                TimeToHold += Time.deltaTime;
                if (TimeToHold > 4) { isDragging = false; TimeToHold = 0; }
                transform.localScale = upScaled;
                transform.position = new Vector3(mousepos.x, mousepos.y, transform.position.z);
            }
            else transform.localScale = normalScale;
            if (Input.GetMouseButtonDown(0)) aud.PlayOneShot(clicks);
        }
    }
    IEnumerator Beating() // Coroutine untuk menghasilkan efek detak jantung
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            aud.PlayOneShot(clips);
            if (HealthPoints > 0) timeSpent += 1;
            volumeBeat.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            volumeBeat.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            volumeBeat.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            volumeBeat.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
    }
    void OnMouseDown() 
    {
    // Aktifkan status drag saat mouse ditekan
        TimeToHold = 0;
        isDragging = true;
    } 
    private void OnMouseUp()
    {
    // Nonaktifkan status drag saat mouse dilepas
        isDragging = false;
    }
}