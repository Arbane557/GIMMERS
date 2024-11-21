using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
   public Sprite[] sprites;
   private int oldSprite;
   private int newSprite;
   private List<int> availableSprites = new List<int>();

   public float hitPoints;

   void Start()
   {
    oldSprite = 0;

    for (int i = 0; i < sprites.Length; i++)
    {
        availableSprites.Add(i);
    }
   }

   public void Picked(float damage)
   {
    hitPoints -= damage;
    if (hitPoints <= 0)
    {
        Destroy(this.gameObject);
        GameObject.Find("SpawnHandler").GetComponent<SpawnHandler>().SpawnNewObject();
    }
    availableSprites.Remove(oldSprite);
    newSprite = availableSprites[Random.Range(0,availableSprites.Count)];

    GetComponent<SpriteRenderer>().sprite = sprites[newSprite];

    availableSprites.Add(oldSprite);
    oldSprite = newSprite;
   }
}
