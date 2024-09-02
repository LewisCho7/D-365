using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public GameObject[] story;
    public GameObject[] endings;
    private GameObject currentday;
    private GameObject buttonDay;
    public List<GameObject> buttonDayList;
    public GameObject[] background;
    
    int index;
    int buttonIndex;
    bool buttonPressed;
    public Queue<string> sentences;
    public Queue<Sprite> sprites;
    public GameObject[] spriteObject;
    public StatManager statManager;
    bool isStillDialogue;

    private void Awake()
    {
        index = 0;
        buttonIndex = 0;
        sentences = new Queue<string>();
        sprites = new Queue<Sprite>();
        currentday = story[index];
        buttonPressed = false;
        isStillDialogue = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {

        ChangeBackground();
        sentences.Clear();
        sprites.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (Sprite sprite in dialogue.sprite) {
            sprites.Enqueue(sprite);

        }
        DisplayNextSentence();
    }

    public void ChangeBackground()
    {
        if(index == 3 || index == 4 || index == 18) {
            for (int i = 0; i < background.Length; i++) {
                if (i == 4)
                {
                    background[i].SetActive(true);
                }
                else {
                    background[i].SetActive(false);

                }
            }
        }

        if (index == 9 || index == 13 )
        {
            for (int i = 0; i < background.Length; i++)
            {
                if (i == 3)
                {
                    background[i].SetActive(true);
                }
                else
                {
                    background[i].SetActive(false);

                }
            }
        }

        if (index == 1 || index == 5 || index == 16)
        {
            for (int i = 0; i < background.Length; i++)
            {
                if (i == 2)
                {
                    background[i].SetActive(true);
                }
                else
                {
                    background[i].SetActive(false);

                }
            }
        }

        if (index == 6 || index == 8|| index == 14 || index == 15 || index == 20 || index == 21)
        {
            for (int i = 0; i < background.Length; i++)
            {
                if (i == 1)
                {
                    background[i].SetActive(true);
                }
                else
                {
                    background[i].SetActive(false);

                }
            }
        }

        if (index == 0 || index == 10|| index == 12 || index == 19)
        {
            for (int i = 0; i < background.Length; i++)
            {
                if (i == 0)
                {
                    background[i].SetActive(true);
                }
                else
                {
                    background[i].SetActive(false);

                }
            }
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();

            return;
        }
        string sentence = sentences.Dequeue();
        Sprite sprite = sprites.Dequeue();
        GameManager.instance.showDialog(sentence);
        showSprite(sprite);

    }
    public void showSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            spriteObject[0].SetActive(false);
            spriteObject[1].SetActive(false);
            spriteObject[2].SetActive(false);
            spriteObject[3].SetActive(false);
            return;
        }
        if (sprite.name.Equals("1") || sprite.name.Equals("2") || sprite.name.Equals("3") || sprite.name.Equals("4"))
        {
            spriteObject[0].SetActive(true);
            spriteObject[1].SetActive(false);
            spriteObject[2].SetActive(false);
            spriteObject[3].SetActive(false);
            spriteObject[0].GetComponent<Image>().sprite = sprite;
        }
        else if (sprite.name.Equals("5"))
        {
            spriteObject[0].SetActive(false);
            spriteObject[1].SetActive(true);
            spriteObject[2].SetActive(false);
            spriteObject[3].SetActive(false);
        }
        else if (sprite.name.Equals("6"))
        {
            spriteObject[0].SetActive(false);
            spriteObject[1].SetActive(false);
            spriteObject[2].SetActive(true);
            spriteObject[3].SetActive(false);
        }
        else if (sprite.name.Equals("7"))
        {
            spriteObject[0].SetActive(false);
            spriteObject[1].SetActive(false);
            spriteObject[2].SetActive(false);
            spriteObject[3].SetActive(true);
        }
    }

    private void EndDialogue()
    {
        GameManager.instance.closeDialog();
        if(index == 7 || index == 11 || index == 17)
        {
            checkStat();
            if (GameManager.instance.gameOver) {
                showEnding();
                return;
            }
        }


        if ((currentday.transform.childCount > 0 || index == 21) && !buttonPressed)
        {
            foreach(GameObject button in buttonDayList)
            {
                if (button.name.Equals(currentday.name)){
                    button.SetActive(true);
                    buttonDay = button;
                }
            }
        }
        else
        {
            index++;
            currentday = story[index];
            buttonPressed = false;
        }

    }
    //transform.childCount > 0

    public void buttonNum(int a)
    {
        buttonIndex = a;
        buttonPressed = true;
        spriteObject[0].SetActive(false);
        spriteObject[1].SetActive(false);
        spriteObject[2].SetActive(false);
        spriteObject[3].SetActive(false);
        buttonDay.SetActive(false);
        if(index == 21)
        {
            if (buttonIndex == 1)
            {
                if (statManager.getAffectionLevel() <= 3) {
                    GameManager.instance.gameOverNum = 2;
                
                }
                else if(statManager.getAffectionLevel() == 5)
                {
                    GameManager.instance.gameOverNum = 4;
                }
                else
                {
                    GameManager.instance.gameOverNum = 3;
                }
            }
            else if (buttonIndex == 2) { 
                if(statManager.getAffectionLevel() < 5)
                {
                    GameManager.instance.gameOverNum = 1;
                }
                else
                {
                    if(statManager.getTrustLevel() >= 4)
                    {
                        GameManager.instance.gameOverNum = 6;
                    }
                    else
                    {
                        GameManager.instance.gameOverNum = 5;
                    }
                }

            }
            GameManager.instance.gameOver = true;
            showEnding();
            return;
        }
        currentday.transform.GetChild(buttonIndex - 1).GetComponent<DialogueTrigger>().TriggerDialogue();

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

            }

            if (!GameManager.instance.isDialogue)
            {
                currentday.GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }

    }

   private void checkStat()
    {
        if(statManager.getAffectionLevel() < 3)
        {
            // ending1
            GameManager.instance.gameOver = true;
            GameManager.instance.gameOverNum = 0;
        }
        else if (statManager.getTrustLevel() < 3)
        {
            // ending2
            GameManager.instance.gameOver = true;
            GameManager.instance.gameOverNum = 1;

        }
    }

    private void showEnding()
    {
        for (int i = 0; i < endings.Length; i++) {
            if (i == GameManager.instance.gameOverNum) {
                endings[i].GetComponent<DialogueTrigger>().TriggerDialogue();
                Debug.Log(i);
                return;
            }
        }
    }
}
