using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public GameObject[] story;
    private GameObject currentday;
    public List<GameObject> buttonDay;
    int index;

    public Queue<string> sentences;

    private void Awake()
    {
        index = 0;
        sentences = new Queue<string>();
        currentday = story[index];
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();

            return;
        }
        string sentence = sentences.Dequeue();
        GameManager.instance.showDialog(sentence);

    }

    private void EndDialogue()
    {
        GameManager.instance.closeDialog();
        if (currentday.transform.childCount > 0)
        {
            foreach(GameObject button in buttonDay)
            {
                if (button.name.Equals(currentday.name)){
                    button.SetActive(true);
                }
            }
        }
        else
        {
            index++;
            currentday = story[index];
        }

    }
    //transform.childCount > 0

    public int buttonNum(int num)
    {
        return num;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.instance.isDialogue)
            {
                DisplayNextSentence();
                SoundManager.instance.ClickSound();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.instance.isDialogue)
            {
                currentday.GetComponent<DialogueTrigger>().TriggerDialogue();
                SoundManager.instance.ClickSound();
            }
        }
    }

}
