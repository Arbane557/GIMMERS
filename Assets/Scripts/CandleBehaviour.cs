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
    private float timeSpent;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private GameObject UImain;
    private float counter;
    private void Start()
    {
        counter = 5;
        healCooldown = 0;
        upScaled = transform.localScale * 1.2f;
        normalScale = transform.localScale;
        normalhealScale = HealBar.transform.localScale.x;
    }
  
    private void Update()
    {
        timeSpent += Time.deltaTime;
        //timerText.text = "" + timeSpent;
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
        if(CandleLight!=null)
        CandleLight.transform.position = new Vector2(transform.position.x, CandleLight.transform.position.y);
        if (healCooldown < 30)
        {
            healCooldown += Time.deltaTime;
            HealBar.transform.localScale = new Vector3(normalhealScale * healCooldown / 30, HealBar.transform.localScale.y, HealBar.transform.localScale.z);
        }
        if (HealthPoints <= 0) {          
            GetComponent<SpriteRenderer>().enabled = false; Destroy(CandleLight);
            ChangeBG.texture = BGStage[4];
            UImain.SetActive(false);
            EndScreen.SetActive(true);
            EndScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You are devoured by the corridor";
            counter -= Time.deltaTime; 
            EndScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Returning in " + Mathf.RoundToInt(counter);
            if (counter <= 0) SceneManager.LoadScene(0);
        }
        else if (HealthPoints <= 3) ChangeBG.texture = BGStage[3];
        else if (HealthPoints <= 5) { ChangeBG.texture = BGStage[2]; ChangeFG.SetActive(true); }
        else if (HealthPoints <= 7) { ChangeBG.texture = BGStage[1]; ChangeFG.SetActive(false); }
        else if (HealthPoints <= 9) ChangeBG.texture = BGStage[0];

        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isDragging)
        {
            TimeToHold += Time.deltaTime;
            if (TimeToHold > 2) { isDragging = false; TimeToHold = 0; }
            transform.localScale = upScaled;
            transform.position =  mousepos + offsetToCenter;
        }
        else transform.localScale = normalScale;

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            offsetToCenter = transform.position - mousepos;
            isDragging = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (healCooldown >= 30) { HealthPoints += 1; healCooldown = 0; }
        }
    }
    
    private void OnMouseUp()
    {
        isDragging = false;
    }
}
