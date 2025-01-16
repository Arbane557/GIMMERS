using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using Unity.Mathematics;
public class CandleBehaviour : MonoBehaviour
{
// Deklarasi variabel untuk  lilin dan status lainnya
    private Vector3 upScaled;
    private Vector3 normalScale;
    private float normalhealScale;
    private Vector3 mousepos;
    public bool isDragging; // Status apakah lilin sedang di-drag
    private float TimeToHold; // Waktu untuk menghitung durasi drag
    public int HealthPoints = 10; // Poin kesehatan lilin
    [SerializeField]
    private List<Texture2D> BGStage; // Background untuk berbagai tahap
    [SerializeField]
    private RawImage ChangeBG; // Gambar latar belakang yang akan diubah
    [SerializeField] private GameObject ChangeFG; // Foreground yang diubah
    [SerializeField] private GameObject HealBar; // UI Health Bar
    public GameObject CandleLight; // Objek lilin
    private float healCooldown; // Cooldown untuk penyembuhan
    public float currentHeal; // Progres penyembuhan
    [SerializeField]
    private float timeSpent; // Waktu yang telah berjalan
    [SerializeField] private TextMeshProUGUI timerText; // Teks untuk menampilkan waktu
    [SerializeField] private GameObject EndScreen; // UI layar akhir
    [SerializeField] private GameObject UImain; // UI utama
    private float counter; // Hitung mundur untuk kembali ke menu
    public int hitCounter; // Hitungan pukulan
    private bool QTE; // Status Quick Time Event
    [SerializeField]
    private GameObject CamObj; // Objek kamera
    [SerializeField] private RawImage heartFill; // Ikon pengisian hati
    [SerializeField] private Texture2D fill1; // Tekstur untuk level kesehatan 1
    [SerializeField] private Texture2D fill2; // Tekstur untuk level kesehatan 2
    [SerializeField] private Texture2D fill3; // Tekstur untuk level kesehatan 3
    [SerializeField] private Texture2D fill4; // Tekstur untuk level kesehatan 4
    [SerializeField] private GameObject fire1; // Level nyala api lilin 1
    [SerializeField] private GameObject fire2; // Level nyala api lilin 2
    [SerializeField] private GameObject fire3; // Level nyala api lilin 3
    public GameObject currFire; // Api aktif saat ini
    [SerializeField] private GameObject spawnFlare; // Efek flare
    [SerializeField] private GameObject volumeBeat; // Efek  beat
    [SerializeField] AudioSource aud; // Sumber audio
    [SerializeField] AudioClip clips; // Klip audio
    [SerializeField] AudioClip clicks;
    [SerializeField] AudioClip[] growl; // Array klip suara

    private void Start()
    {
    // Inisialisasi variabel dan memulai efek audio detak jantung
        StartCoroutine(Beating());
        counter = 10;
        healCooldown = -1;
        currentHeal = 0;
        upScaled = transform.localScale * 1.2f;
        normalScale = transform.localScale;
        normalhealScale = HealBar.transform.localScale.x;
    }
  
    private void Update()
    {
    // Periksa apakah Quick Time Event sedang aktif
        if (!QTE)
        {
            timerText.text = "" + timeSpent;
             // Periksa apakah waktu telah habis
            if (timeSpent >= 200)
            {
            // Logika akhir permainan
                spawnFlare.SetActive(false);
                timeSpent = 200;
                currFire.GetComponent<SpriteRenderer>().enabled = false;
                CandleLight.GetComponent<SpriteRenderer>().enabled = false;
                UImain.SetActive(false);
                EndScreen.SetActive(true);
                EndScreen.SetActive(true);
                EndScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You Survived The Corridor";
                counter -= Time.deltaTime;
                EndScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Returning in " + Mathf.RoundToInt(counter);
                if (counter <= 0) SceneManager.LoadScene(0);
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
    IEnumerator Beating()
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
    // Nonaktifkan status drag saat mouse dilepass
        isDragging = false;
    }
}