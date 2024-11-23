using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryHandler : MonoBehaviour
{
    [SerializeField] private string[] Story;
    [SerializeField] private TextMeshProUGUI storyText;
    private int index;
    private Coroutine displayStory;
    [SerializeField] private GameObject Eyes;
    [SerializeField] private GameObject Fade;
    private void Start()
    {
        displayStory = StartCoroutine(typeStory("Where am I..."));
        index = 0;   
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (displayStory != null) { 
                StopCoroutine(displayStory);
            }
            displayStory = StartCoroutine(typeStory(Story[index]));
            if (index == 11) Eyes.SetActive(true);
            if (index == 18) StartCoroutine(loadnextScene());
            index++;
        }
    }
    IEnumerator typeStory(string line)
    {
        storyText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            storyText.text += letter;
            yield return new WaitForSeconds(0.06f);
        }
    }
    IEnumerator loadnextScene()
    {
        Fade.SetActive(true);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(2);
    }
}
