using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private TextMeshProUGUI currentPlayerScoreText;
    [SerializeField] [Tooltip("Top 3 Names and Scores Text")] private TextMeshProUGUI[] topPlayersAndScoresTextList;
    [SerializeField] private List<string> topPlayerNamesList = new List<string>();
    [SerializeField] private List<float> topPlayerScoresList = new List<float>();
    [SerializeField] private GameObject scores;

    private string currentPlayerName = "TON NOM";
    private float currentPlayerScore;

    private const int MaxScores = 3;

    void Start()
    {
        scores.SetActive(false);

        LoadScores();       
        InitializeLeaderboard();
        DisplayScoreBoard();
    }

    void Update()
    {
        UpdateScore();
        DisplayScore();
    }

    private void UpdateScore()
    {
        currentPlayerScore = playerGameObject.transform.position.y;
    }

    private void DisplayScore()
    {
        currentPlayerScoreText.text = $"{currentPlayerName} : {currentPlayerScore:F0}";
    }

    private void InitializeLeaderboard()
    {
        if (topPlayerNamesList.Count == 0)
        {
            for (int i = 0; i < MaxScores; i++)
            {
                topPlayerNamesList.Add("VIDE");
                topPlayerScoresList.Add(0);
            }
        }
    }

    private void DisplayScoreBoard()
    {
        for (int i = 0; i < MaxScores; i++)
        {
            topPlayersAndScoresTextList[i].text = $"{topPlayerNamesList[i]} : {topPlayerScoresList[i]:F0}";
        }
    }

    public void AddPlayerName(string _name)
    {
        currentPlayerName = _name;
    }

    public void EndGame()
    {
        scores.SetActive(true);

        for (int i = 0; i < MaxScores; i++)
        {
            if (currentPlayerScore > topPlayerScoresList[i])
            {
                for (int j = MaxScores - 1; j > i; j--)
                {
                    topPlayerScoresList[j] = topPlayerScoresList[j - 1];
                    topPlayerNamesList[j] = topPlayerNamesList[j - 1];
                }

                topPlayerScoresList[i] = currentPlayerScore;
                topPlayerNamesList[i] = currentPlayerName;
                break;
            }
        }

        SaveScores();       
        DisplayScoreBoard();
    }

    private void SaveScores()
    {
        for (int i = 0; i < MaxScores; i++)
        {
            PlayerPrefs.SetString($"Name{i}", topPlayerNamesList[i]);
            PlayerPrefs.SetFloat($"Score{i}", topPlayerScoresList[i]);
        }
        PlayerPrefs.Save();
        Debug.Log("Leaderboard Saved");
    }

    private void LoadScores()
    {
        topPlayerNamesList.Clear();
        topPlayerScoresList.Clear();

        for (int i = 0; i < MaxScores; i++)
        {
            string name = PlayerPrefs.GetString($"Name{i}", "VIDE");
            float score = PlayerPrefs.GetFloat($"Score{i}", 0);

            topPlayerNamesList.Add(name);
            topPlayerScoresList.Add(score);
        }
        Debug.Log("Leaderboard Loaded");
    }
}