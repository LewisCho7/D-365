using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject story;
    public bool gameOver;
    public int gameOverNum = -1;
    public bool isDialogue = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gameOver = false;
    }

    [SerializeField]
    private GameObject ChatBox;

    [SerializeField]
    private TextMeshProUGUI ChatText;

    public void showDialog(string dialogue)
    {
        isDialogue = true;
        ChatBox.SetActive(true);
        ChatText.SetText(dialogue);
    }
    
    public void closeDialog()
    {
        
        ChatBox.SetActive(false);

        StartCoroutine(IE_CloseDialogue());

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            story.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    IEnumerator IE_CloseDialogue()
    {
        yield return new WaitForSeconds(0.01f);
        isDialogue = false;

    }

}
