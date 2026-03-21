using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogPanel;

    [TextArea(3, 10)]
    public string[] sentences; 
    public string characterName;
    public float typingSpeed = 0.05f;

    private int index;
    private bool isTyping;

    void Start()
    {
        dialogPanel.SetActive(false); 
    }

    public void StartDialogue()
    {
        dialogPanel.SetActive(true);
        nameText.text = characterName;
        index = 0;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        isTyping = true;
        dialogueText.text = "";
        
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        isTyping = false;
    }

    public void NextSentence()
    {
        if (isTyping) return; 

        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(TypeSentence());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogPanel.SetActive(false);
        
    }

    void Update()
    {
        // Apasă Space sau Click pentru următoarea replică
        if (dialogPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            NextSentence();
        }
    }
}