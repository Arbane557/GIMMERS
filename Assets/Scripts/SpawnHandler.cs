using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public GameObject flare; // Referensi ke prefab objek yang akan di-spawn

    private void Start() 
    {
        // Memulai korutine untuk melakukan spawn objek secara berkala
        StartCoroutine(SpawnAfterTime());
    }
   
    IEnumerator SpawnAfterTime()
    {
        while (true) // Loop tanpa henti untuk spawn objek
        {   
        // Membuat instans baru dari prefab flare
            GameObject nb = Instantiate(flare);
            // Mengatur posisi flare secara acak di dalam rentang tertentu
            nb.transform.position = new Vector3(Random.Range(-6,6), Random.Range(1.5f,0.5f)); 
            yield return new WaitForSeconds(10); // Tunggu selama 10 detik sebelum spawn berikutnya
        }
    }
}