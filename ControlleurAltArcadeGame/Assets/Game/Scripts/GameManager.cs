using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    private static readonly int NoteTouched = Animator.StringToHash("NoteTouched");

    [Header("Link")]
    public int score = 0;
    public int debuffScoreLose = 0;
    public float debuffCooldownTime = 60f;
    private bool _debuffInCooldown;
    public int savedScore;
    public TMP_Text scoreText;
    public GameObject hpSprite1;
    public GameObject hpSprite2;
    public GameObject hpSprite3;
    public GameObject bonusPopUp;
    //public SpriteRenderer bonusPopUpSr;
    public UIScript ui;
    public AnimationManager animManager;
    
    
    [Header("Note Spawner")]
    public NoteSpawner noteSpawner;
    private int hp = 3;

    [Header("Bonus Bar")] public Slider bonusBar;
    public float doubleScoreBonusDurationTime = 10;
    public float invincibilityBonusDurationTime = 5;
    public int scoreToActivateBonus = 100;
    private bool _bonusActive = false;
    private bool _doubleScoreBonusActive = false;
    private bool _invincibilityBonusActive = false;

    private void Start()
    {
        _bonusActive = false;
        bonusBar.maxValue = scoreToActivateBonus;
        bonusBar.value = 0;
        bonusBar.value = score;
        _debuffInCooldown = false;
        bonusPopUp.SetActive(false);
        UpdateUI();
    }

    public void StartTheGame()
    {
        score = 0;
        hp = 3;
        UpdateUI();
        noteSpawner.StartSpawnLoop();
        
    }

    private void Update()
    {
        if (bonusBar.value >= scoreToActivateBonus)
        {
            bonusPopUp.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                Random rand = new Random();
                int randomInt = rand.Next(0, 100);
                
                if (randomInt <= 50)
                {
                    _bonusActive = true;
                    _invincibilityBonusActive = true;
                    Debug.Log("Invincibility");
                    StartCoroutine(InvincibilityBonusCorout());
                }
                else
                {
                    _bonusActive = true;
                    _doubleScoreBonusActive = true;
                    Debug.Log("Double score");
                    StartCoroutine(DoubleScoreBonusCorout());
                }
                
                bonusBar.value -= scoreToActivateBonus;
                bonusPopUp.SetActive(false);
                
            }
        }

        if (!_debuffInCooldown && Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            StartCoroutine(DebuffCorout());
        }

    }

    private IEnumerator DebuffCorout()
    {
        if ((score -= debuffScoreLose) <= 0)
        {
            score = 0;
            UpdateUI();
        }
        else
        {
            score -= debuffScoreLose;
            UpdateUI();
        }
        
        _debuffInCooldown = true;
        yield return new WaitForSeconds(debuffCooldownTime);
        _debuffInCooldown = false;
    }
    
    private IEnumerator DoubleScoreBonusCorout()
    {
        yield return new WaitForSeconds(doubleScoreBonusDurationTime);
        _doubleScoreBonusActive = false;
        _bonusActive = false;
    }
    
    private IEnumerator InvincibilityBonusCorout()
    {
        yield return new WaitForSeconds(invincibilityBonusDurationTime);
        _invincibilityBonusActive = true;
        _bonusActive = false;
    }

    public void NoteHit(int scoreUp)
    {
        if (_doubleScoreBonusActive)
        {
            score += scoreUp * 2;

        }
        else
        {
            if (!_bonusActive && !Mathf.Approximately(bonusBar.value, bonusBar.maxValue))
            {
                bonusBar.value += scoreUp;
            }
            
            score += scoreUp;
        }

        UpdateUI();
    }

    

    public void NoteMiss()
    {
        if (_invincibilityBonusActive)
        {
            // do nothing
        }
        else
        {
            hp -= 1;
        }
        
        
        if (hp == 0)
        {
            Death();
        }
        
        UpdateUI();
    }

    private void Death()
    {
        hpSprite3.SetActive(false);
        noteSpawner.StopSpawnLoop();
        savedScore = score;
        
        if (ui != null)
        {
            ui.ShowEndScreen();
        }

    }

    void UpdateUI()
    {
        scoreText.text = "Height : "+ score;
        
        if (hp == 3)
        {
            hpSprite1.SetActive(true);
            hpSprite2.SetActive(true);
            hpSprite3.SetActive(true);
        }
        else if (hp == 2)
        {
            hpSprite1.SetActive(false);
        }
        else if (hp == 1)
        {
            hpSprite2.SetActive(false);
        }
    }

}