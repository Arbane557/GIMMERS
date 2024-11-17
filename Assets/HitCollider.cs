using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    [SerializeField] private GameObject Lanerends;
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color normal;
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
        collision.gameObject.GetComponent<CandleBehaviour>().HealthPoints -= 1;
        collision.gameObject.GetComponent<SpriteRenderer>().color = hurtColor;
        yield return new WaitForSeconds(0.1f);
        collision.gameObject.GetComponent<SpriteRenderer>().color = normal;

    }
}
