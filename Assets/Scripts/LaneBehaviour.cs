using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LaneBehaviour : MonoBehaviour
{ 
    [System.Serializable]
    private class Lanes
    {
        public GameObject LaneRend;
        public GameObject LaneCol;
        public GameObject LaneAnim;
    }
    [SerializeField]
    private List<Lanes> Lane;
    [SerializeField] int redlaneCount = 0;
    [SerializeField] AudioClip[] atkwind;
    [SerializeField] AudioSource aud;
    private void Start()
    {
        StartCoroutine(generateLanes());
    }
    IEnumerator generateLanes()
    {
        while (true) {
            redlaneCount = 0;
            foreach (Lanes go in Lane)
            {
                bool val = (Random.Range(0, 2) == 0);            
                if (redlaneCount < 3 && val) {
                    redlaneCount++;
                    go.LaneRend.SetActive(val);                  
                }   
                else go.LaneRend.SetActive(false);
            }
            if (redlaneCount == 1)
            {
                foreach (Lanes col in Lane)
                {
                    col.LaneRend.SetActive(false);
                    col.LaneCol.SetActive(true);
                    col.LaneCol.GetComponent<HitCollider>().isChase = true;
                }
                yield return new WaitForSeconds(3);
                foreach (Lanes col in Lane)
                {
                    col.LaneCol.GetComponent<HitCollider>().isChase = false;
                    col.LaneCol.SetActive(false);
                }
                aud.PlayOneShot(atkwind[Random.Range(0, atkwind.Length)]);
                foreach (Lanes col in Lane)
                {
                    if (col.LaneRend.gameObject.activeSelf)
                    {
                        col.LaneRend.SetActive(false);
                        yield return new WaitForSeconds(0.5f);
                        col.LaneAnim.SetActive(true); col.LaneCol.SetActive(true);
                        yield return new WaitForSeconds(0.2f);
                        col.LaneCol.SetActive(false);
                        yield return new WaitForSeconds(1);
                        col.LaneAnim.SetActive(false);
                    }
                }
            }
            else
            {
                yield return new WaitForSeconds(1);
                foreach (Lanes col in Lane)
                {
                    if (col.LaneRend.gameObject.activeSelf) col.LaneRend.GetComponent<RawImage>().enabled = false;
                }
                if (redlaneCount > 0) aud.PlayOneShot(atkwind[Random.Range(0, atkwind.Length)]);
                yield return new WaitForSeconds(0.5f);
                foreach (Lanes col in Lane)
                {
                    if (col.LaneRend.gameObject.activeSelf)
                    { col.LaneCol.SetActive(true); col.LaneAnim.SetActive(true); }
                    col.LaneRend.GetComponent<RawImage>().enabled = true;
                    col.LaneRend.SetActive(false);
                }
                yield return new WaitForSeconds(0.2f);
                foreach (Lanes col in Lane) col.LaneCol.SetActive(false);
                yield return new WaitForSeconds(1f);
                foreach (Lanes col in Lane) col.LaneAnim.SetActive(false);
            }
            }
    }
 }
