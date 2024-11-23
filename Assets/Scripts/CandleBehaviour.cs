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
using Unity.PlasticSCM.Editor.WebApi;
public class CandleBehaviour : MonoBehaviour
{
    private Vector3 upScaled;
    private Vector3 normalScale;
    private float normalhealScale;
    private Vector3 offsetToCenter;
    private Vector3 mousepos;
    public bool isDragging;
    private float TimeToHold;
    public int HealthPoints = 10;
    [SerializeField]
    private List<Texture2D> BGStage;
    [SerializeField]
    private RawImage ChangeBG;
    [SerializeField] private GameObject ChangeFG;
    [SerializeField] private GameObject HealBar;
    public GameObject CandleLight;
    private float healCooldown;
    public float currentHeal;
    private float timeSpent;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private GameObject UImain;
    private float counter;
    public int hitCounter;
    private bool QTE;
    [SerializeField]
    private GameObject CamObj;
    [SerializeField] private RawImage heartFill;
    [SerializeField] private Texture2D fill1;
    [SerializeField] private Texture2D fill2;
    [SerializeField] private Texture2D fill3;
    [SerializeField] private Texture2D fill4;
    [SerializeField] private GameObject fire1;
    [SerializeField] private GameObject fire2;
    [SerializeField] private GameObject fire3;
    public GameObject currFire;
    [SerializeField] private GameObject spawnFlare;
    private void Start()
    {
        StartCoroutine(Beating());
        counter = 5;
        healCooldown = -1;
        currentHeal = 0;
        upScaled = transform.localScale * 1.2f;
        normalScale = transform.localScale;
        normalhealScale = HealBar.transform.localScale.x;
    }
  
    private void Update()
    {
        if (!QTE)
        {
            timerText.text = "" + timeSpent;
            if (timeSpent > 300)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                Destroy(CandleLight);
                UImain.SetActive(false);
                EndScreen.SetActive(true);
                EndScreen.SetActive(true);
                EndScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You survived the corridor";
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
                //Destroy(heartFill.gameObject.transform.parent.gameObject);
                GetComponent<SpriteRenderer>().enabled = false; Destroy(CandleLight);
                ChangeBG.texture = BGStage[4];
                UImain.SetActive(false);
                EndScreen.SetActive(true);
                EndScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You are devoured by the corridor";
                counter -= Time.deltaTime;
                EndScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Returning in " + Mathf.RoundToInt(counter);
                if (counter <= 0) SceneManager.LoadScene(0);
            }
            else if (HealthPoints <= 3) { ChangeBG.texture = BGStage[3]; heartFill.texture = fill4; currFire = fire3; fire3.SetActive(true); fire2.SetActive(false); fire1.SetActive(false); }
            else if (HealthPoints <= 5) { ChangeBG.texture = BGStage[2]; ChangeFG.SetActive(true); heartFill.texture = fill3; currFire = fire2; fire2.SetActive(true); fire1.SetActive(false); fire3.SetActive(false); }
            else if (HealthPoints <= 7) { ChangeBG.texture = BGStage[1]; ChangeFG.SetActive(false); heartFill.texture = fill2; currFire = fire1; fire1.SetActive(true); fire2.SetActive(false); fire3.SetActive(false); }
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

        }
    }
    IEnumerator Beating()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            timeSpent += 1;
            yield return new WaitForSeconds(1f);
        }
    }
    void OnMouseDown()
    {
        TimeToHold = 0;
        isDragging = true;
    }
    
    private void OnMouseUp()
    {
        isDragging = false;
    }
}
