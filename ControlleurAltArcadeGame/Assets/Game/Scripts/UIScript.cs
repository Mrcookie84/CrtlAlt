using System.Collections;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject startScreen;
    public GameObject gameScreen;
    public GameObject endScreen; // Canvas du clavier

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
        gameScreen.SetActive(true);
        gameManager.StartTheGame();
    }

    public void ShowEndScreen()
    {
        gameScreen.SetActive(false);
        endScreen.SetActive(true);
    }

    public void BackToStartMenu()
    {
        endScreen.SetActive(false);
        startScreen.SetActive(true);
    }
}