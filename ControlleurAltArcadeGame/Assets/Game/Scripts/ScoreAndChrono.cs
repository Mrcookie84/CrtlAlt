using UnityEngine;
using TMPro;

public class ScoreAndChrono : MonoBehaviour
{
    [SerializeField] float timer;
    private int[] score;
    private int playerScore;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        DisplayTime(timer);
    }

    private void DisplayTime(float timeToDisplay)
    {
        var t0 = (int)timeToDisplay;

        var m = t0 / 60;

        var s = t0 - m * 60;
        var ms = (int)((timeToDisplay - t0) * 1000);

        timerText.text = $"{m:00}:{s:00}:{ms:000}";
    }
}

