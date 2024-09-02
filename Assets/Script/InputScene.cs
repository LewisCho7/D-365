using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InputScene : MonoBehaviour
{
    public TMP_InputField field;

    public void submit()
    {
        Name.Instance.playerName = field.text;
        SceneManager.LoadScene("SampleScene");
    }
}
