using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public float score;

    public ScoreEntry(string name, float score)
    {
        this.playerName = name;
        this.score = score;
    }
}

public class TableauDeScoreManager : MonoBehaviour
{
    [Header("Références UI")]
    [SerializeField] private TextMeshProUGUI currentPlayerScoreText;   // Texte en direct (pendant la partie)
    [SerializeField] private TextMeshProUGUI[] topPlayersAndScoresTextList; // Top 3 affichés dans le menu
    [SerializeField] private GameManager gameManager;

    [Header("Paramètres")]
    [SerializeField] private int maxScores = 3;

    private string currentPlayerName = "PLAYER";
    private float currentPlayerScore;
    private List<ScoreEntry> scores = new List<ScoreEntry>();

    private void Start()
    {
        LoadScores();
        UpdateLeaderboardUI();
    }

    private void Update()
    {
        
        if (gameManager != null)
        {
            currentPlayerScore = gameManager.savedScore;
            if (currentPlayerScoreText != null)
            {
                currentPlayerScoreText.text = $"{currentPlayerName} : {currentPlayerScore:F0}m";
            }
        }
    }
    
    public void AddPlayerName(string name)
    {
        currentPlayerName = name;
    }


    public void AddScore(string playerName, int score)
    {
        scores.Add(new ScoreEntry(playerName, score));


        scores.Sort((a, b) => b.score.CompareTo(a.score));


        if (scores.Count > maxScores)
            scores.RemoveRange(maxScores, scores.Count - maxScores);

        SaveScores();
        UpdateLeaderboardUI();
    }
    
    private void UpdateLeaderboardUI()
    {
        for (int i = 0; i < topPlayersAndScoresTextList.Length; i++)
        {
            if (i < scores.Count)
                topPlayersAndScoresTextList[i].text = $"{scores[i].playerName} : {scores[i].score:F0}m";
            else
                topPlayersAndScoresTextList[i].text = "-";
        }
    }
    
    private void SaveScores()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetString($"Name{i}", scores[i].playerName);
            PlayerPrefs.SetFloat($"Score{i}", scores[i].score);
        }
        PlayerPrefs.Save();
    }
    
    private void LoadScores()
    {
        scores.Clear();
        for (int i = 0; i < maxScores; i++)
        {
            string name = PlayerPrefs.GetString($"Name{i}", "VIDE");
            float score = PlayerPrefs.GetFloat($"Score{i}", 0);
            if (!(name == "VIDE" && score == 0)) // Ignore les entrées vides
                scores.Add(new ScoreEntry(name, score));
        }
    }
}
