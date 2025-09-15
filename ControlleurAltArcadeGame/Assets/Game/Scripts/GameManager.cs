using TMPro;
using UnityEngine;
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
    
    [Header("Note Spawner")]
    public NoteSpawner noteSpawner;
    private int hp = 3;
    

    public void NoteHit(int scoreUp)
    {
        score += scoreUp;
        UpdateUI();
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
            hpSprite3.SetActive(false);
            StopCoroutine(noteSpawner.SpawnLoop());
            _savedScore = score;
        }
        
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = score.ToString();
    }
    
    
}