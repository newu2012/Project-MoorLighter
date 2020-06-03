using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phrase
{
    public string SpeakerName;
    public string Message;

    public Phrase(string speacerName, string message)
    {
        SpeakerName = speacerName;
        Message = message;
    }
}

public class Dialog : MonoBehaviour
{
    public List<string> SpeakerNameInOrder;
    public List<string> PhrasesInOrder;
    public List<Phrase> Phrases;
    public GameObject Canvas;

    public void StartDialog()
    {
        Phrases = new List<Phrase>();
        for (var i = 0; i < SpeakerNameInOrder.Count; i++)
            Phrases.Add(new Phrase(SpeakerNameInOrder[i], PhrasesInOrder[i]));

        Canvas.GetComponent<PlayDialog>().StartDialog(Phrases);
    }
}
