using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroyonLoad : MonoBehaviour
// Menjaga objek tetap ada meskipun scene berganti
{void Awake(){DontDestroyOnLoad(this.gameObject);}}
