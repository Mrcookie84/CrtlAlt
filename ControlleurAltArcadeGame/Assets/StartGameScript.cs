using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class StartGameScript : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject startScreen;
    public GameObject gameScreen;
    public GameObject endScreen;


    private void Start()
    {
        startScreen.SetActive(true);
        gameScreen.SetActive(false);
        endScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKeyDown && startScreen.activeSelf)
        {
            startScreen.SetActive(false);
            StartCoroutine(StartGameCorout());
        }
    }

    private IEnumerator StartGameCorout()
    {
        yield return new WaitForSeconds(1f);
        gameManager.StartTheGame();

    }
    
}
