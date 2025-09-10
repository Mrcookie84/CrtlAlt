using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]private GameObject player;
    [SerializeField]private TextMeshProUGUI scoreText;
    [SerializeField]private float playerScore;
    void Start()
    {
        
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
}
