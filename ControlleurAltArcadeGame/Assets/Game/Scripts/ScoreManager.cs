using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]private GameObject player;
    [SerializeField]private TextMeshProUGUI scoreText;
    [SerializeField]private TextMeshProUGUI[] topScoresAndPlayers;
    [SerializeField]private float[] topScores;
    [SerializeField]private string[] topPlayers;
    private string currentPlayer;
    
    public float playerScore;
    //public List<string> playerScoreList;  
    public DigitalKeyboardController digitalKeyboardScript;

    void Start()
    {
        DisplayScoreBoard();
    }
    void Update()
    {
        UpdateScore();
        DisplayScore();
    }

    public void UpdateScore()
    {
        playerScore = player.transform.position.y;
    }

    public void DisplayScore()
    {
        scoreText.text = playerScore.ToString();
    }

    #region TopScoresAndPlayers
    
    private void DisplayScoreBoard()
    {
        for (int i = 0; i < topScoresAndPlayers.Length; i++)
        {
            topScoresAndPlayers[i].text = topScores[i] + ":" + topPlayers[i];
        }
    }
    public void AddScoreAndName(string _name, float _score)
    {
        currentPlayer = _name + ":" + _score;
    }

    public void EndGame()
    {
        for (int i = 0; i < topScores.Length; i++)
        {
            if (topScores[i] < playerScore)
                {
                    if (i == 0)
                    {
                        //
                    }
                    topScoresAndPlayers[i-1].text = topScoresAndPlayers[i].ToString();
                    topScoresAndPlayers[i].text = currentPlayer;
                }
        }
        playerScore = 0;
    }
    #endregion
}
