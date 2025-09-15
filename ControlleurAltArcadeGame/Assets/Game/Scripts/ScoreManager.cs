using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]private GameObject playerGameObject;
    [SerializeField]private TextMeshProUGUI currentPlayerScoreText;
    [SerializeField][Tooltip("Top 3 Names and Scores Text")]private TextMeshProUGUI[] topPlayersAndScoresTextList;
    [SerializeField]private List<String> topPlayerNamesList = new List<String>(3);
    [SerializeField]private List<float> topPlayerScoresList = new List<float>(3);

    private string currentPlayerName = "TON NOM";
    private float currentPlayerScore;
    
    void Start()
    {
        DisplayScoreBoard();
        topPlayerNamesList.Capacity = 3;
        topPlayerScoresList.Capacity = 3;
    }
    void Update()
    {
        UpdateScore();
        DisplayScore();
    }
 
    public void UpdateScore()
    {
        currentPlayerScore = playerGameObject.transform.position.y;
    }

    public void DisplayScore()
    {
        currentPlayerScoreText.text = currentPlayerName + " : " + currentPlayerScore;
    }
    private void DisplayScoreBoard()
    {
        Debug.Log("Displaying Score Board");
        
        if (topPlayerNamesList.Count != 0 &&  topPlayerScoresList.Count != 0)
        {
            for (int i = 0; i < topPlayerNamesList.Count; i++)
            {
                topPlayersAndScoresTextList[i].text = topPlayerNamesList[i] + " : " + topPlayerScoresList[i].ToString("F0");
            }
        }
        else
        {
            for (int i = 0; i < topPlayersAndScoresTextList.Length; i++)
            {
                topPlayerNamesList.Add("VIDE");
                topPlayerScoresList.Add(0);
                topPlayersAndScoresTextList[i].text = topPlayerNamesList[i] + " : " + topPlayerScoresList[i].ToString("F0");
                Debug.Log(topPlayersAndScoresTextList[i].text);
            }
        }
    }
    public void AddPlayerName(string _name)
    {
        currentPlayerName = _name;
    }

    public void EndGame()
    {
        
        for (int i = 0; i < topPlayerScoresList.Count; i++)
        {
            if (currentPlayerScore > topPlayerScoresList[i])
            {
                for (int j = topPlayerScoresList.Count - 1; j > i; j--)
                {
                    topPlayerScoresList[j] = topPlayerScoresList[j - 1];
                    topPlayerNamesList[j] = topPlayerNamesList[j - 1];
                }

                topPlayerScoresList[i] = currentPlayerScore;
                topPlayerNamesList[i] = currentPlayerName;

                break; 
            }
        }
        DisplayScoreBoard();

        currentPlayerScore = 0;
    }
}
