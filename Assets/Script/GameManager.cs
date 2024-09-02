using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject story;
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
        isDialogue = false;
        ChatBox.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            story.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
}
