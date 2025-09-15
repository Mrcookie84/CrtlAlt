using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Link")]
    public int score = 0;
    private int _savedScore;
    public TMP_Text scoreText;
    public GameObject hpSprite1;
    public GameObject hpSprite2;
    public GameObject hpSprite3;
    public GameObject bonusPopUp;
    //public SpriteRenderer bonusPopUpSr;
    
    
    [Header("Note Spawner")]
    public NoteSpawner noteSpawner;
    private int hp = 3;

    [Header("Bonus Bar")] public Slider bonusBar;
    public float bonusDurationTime = 10;
    public int scoreToActivateBonus = 100;
    private bool bonusActive;

    private void Start()
    {
        bonusBar.value = 0;
        bonusActive = false;
        bonusPopUp.SetActive(false);
        UpdateUI();
        StartTheGame();
    }

    public void StartTheGame()
    {
        noteSpawner.StartSpawnLoop();
    }

    private void Update()
    {
        if (bonusBar.value >= scoreToActivateBonus)
        {
            bonusPopUp.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                bonusBar.value = 0;
                bonusActive = true;
                bonusPopUp.SetActive(false);
                StartCoroutine(BonusCorout());
            }
        }
    }

    public void NoteHit(int scoreUp)
    {
        if (bonusActive)
        {
            score += scoreUp * 2;
        }
        else
        {
            score += scoreUp;

            if (!bonusActive && !Mathf.Approximately(bonusBar.value, bonusBar.maxValue))
            {
                float increment = bonusBar.maxValue * 0.01f;
                bonusBar.value = Mathf.Min(bonusBar.value + increment, bonusBar.maxValue);
            }
        }

        UpdateUI();
    }

    private IEnumerator BonusCorout()
    {
        yield return new WaitForSeconds(bonusDurationTime);

        bonusActive = false;
    }

    public void NoteMiss()
    {
        hp -= 1;
        
        if (hp == 2)
        {
            hpSprite1.SetActive(false);
        }
        else if (hp == 1)
        {
            hpSprite2.SetActive(false);
        }
        else if (hp == 0)
        {
            Death();
        }
        
        UpdateUI();
    }

    private void Death()
    {
        hpSprite3.SetActive(false);
        noteSpawner.StopSpawnLoop();
        _savedScore = score;
    }

    void UpdateUI()
    {
        scoreText.text = "Height : "+ score;
    }
    
    
}