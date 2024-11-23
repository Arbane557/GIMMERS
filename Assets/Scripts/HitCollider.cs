using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    [SerializeField] private GameObject Lanerends;
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color normal;
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip hurtsound;
    public bool isChase;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("candle"))
        {
            if (isChase)
            {
                Lanerends.SetActive(true);
            }
            else if (!isChase) {

                StartCoroutine(Hit(collision.gameObject));
            }           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("candle")) if (isChase) Lanerends.SetActive(false);
    }
    IEnumerator Hit(GameObject collision)
    {
        var cdl = collision.gameObject;
        cdl.GetComponent<CandleBehaviour>().currFire.GetComponent<SpriteRenderer>().color = hurtColor;
        yield return new WaitForSeconds(0.1f);
        cdl.GetComponent<CandleBehaviour>().currFire.GetComponent<SpriteRenderer>().color = normal;
        cdl.GetComponent<CandleBehaviour>().HealthPoints -= 1;
        aud.PlayOneShot(hurtsound);
        Debug.Log("hit");
        cdl.GetComponent<CandleBehaviour>().hitCounter += 1;

    }
}
