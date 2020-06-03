using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayDialog : MonoBehaviour
{
    public Text Name;
    public Text Message;
    List<Phrase> Phrases;
    public Canvas Canvas;
    private int currentMessageNumber;

    public void Start()
    {
        Canvas.enabled = false;
    }

    public void StartDialog(List<Phrase> phrases)
    {     
        Phrases = phrases;
        Canvas.enabled = true;        
        currentMessageNumber = 0;
        ShowMessage();
    }

    public void ShowMessage()
    {
        Phrase currentPhrase;

        if (Phrases.Count > currentMessageNumber)
            currentPhrase = Phrases[currentMessageNumber];
        else
        {
            Canvas.enabled = false;
            return;
        }

        Name.text = currentPhrase.SpeakerName;
        Message.text = currentPhrase.Message;        
    }
    
    public void NextMessage()
    {
        currentMessageNumber++;
        ShowMessage();
    }
}
