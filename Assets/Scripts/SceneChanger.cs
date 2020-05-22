using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject player;


    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene("BattleScene");
        SceneManager.UnloadSceneAsync("Demo");
    }

    
    //void Start()
    //{
//
    //    if (GetComponent<BoxCollider2D>().IsTouching(player.GetComponent<BoxCollider2D>()))
    //    {
    //        SceneManager.LoadScene("BattleScene");
    //        SceneManager.UnloadSceneAsync("Demo");
    //    }
    //}
}
