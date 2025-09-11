using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;

    public void NoteHit()
    {
        score += 1;
        UpdateUI();
    }

    public void NoteMiss()
    {
        score -= 1;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = score.ToString();
    }
}