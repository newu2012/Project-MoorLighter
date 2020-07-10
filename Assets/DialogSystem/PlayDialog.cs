using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< Updated upstream
=======
using System;
using DragonBones;
using Transform = UnityEngine.Transform;
>>>>>>> Stashed changes

public class PlayDialog : MonoBehaviour
{
    public Text Name;
    public Text Message;
    List<Phrase> Phrases;
    public Canvas Canvas;
    private int currentMessageNumber;
<<<<<<< Updated upstream
=======
    public GameObject npc;
>>>>>>> Stashed changes

    public void Start()
    {
        Canvas.enabled = false;
    }

    public void StartDialog(List<Phrase> phrases)
    {     
        Phrases = phrases;
        Canvas.enabled = true;        
        currentMessageNumber = 0;
<<<<<<< Updated upstream
=======
        npc.GetComponent<UnityArmatureComponent>().animation.Play("Танец", 0);
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
            npc.GetComponent<UnityArmatureComponent>().animation.Play("PROSTOI", 0);
>>>>>>> Stashed changes
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
