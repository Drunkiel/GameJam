using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public static DialogController instance;

    public List<string> texts = new();
    public TMP_Text dialogText;

    private void Awake()
    {
        instance = this;
        ChangeText();
    }

    public void ChangeText()
    {
        dialogText.text = texts[Random.Range(0, texts.Count - 1)];
    }
}
