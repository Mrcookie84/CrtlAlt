
/*
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // ⚠️ pour Slider et Image

public class ToucheScript : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject touchePanel;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Slider holdProgressSlider;

    private enum Direction { Left, Up, Right, Down, HoldLeft, HoldUp, HoldRight, HoldDown }

    private List<Direction> currentCombination = new List<Direction>();
    private int currentInputIndex = 0;

    private float difficultyTimer = 0f;
    private int difficultyLevel = 1;
    public float timeToIncreaseDifficulty = 20f;

    public int lives = 3;
    private bool isOnCooldown = false;
    private float baseCooldown = 1.5f;
    private float cooldownDuration;
    
    public float holdTimeRequired = 1.0f; 
    private float currentHoldTime = 0f;
    private bool holding = false;

    private void Start()
    {
        touchePanel.SetActive(true);
        cooldownDuration = baseCooldown;

        if (holdProgressSlider != null)
            holdProgressSlider.gameObject.SetActive(false);

        GenerateNewCombination();
    }

    private void Update()
    {
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer >= timeToIncreaseDifficulty && difficultyLevel < 4)
        {
            difficultyLevel++;
            difficultyTimer = 0f;
            cooldownDuration = Mathf.Max(0.6f, baseCooldown - (difficultyLevel - 1) * 0.3f);
        }

        if (!isOnCooldown)
            HandlePlayerInput();

        
        if (holding && holdProgressSlider != null)
        {
            holdProgressSlider.value = currentHoldTime / holdTimeRequired;
        }
    }

    private void HandlePlayerInput()
    {
        if (currentInputIndex >= currentCombination.Count) return;

        Direction expected = currentCombination[currentInputIndex];

        // 🔹 Gestion des flèches simples
        if (expected == Direction.Left)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) CheckInput(Direction.Left);
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                WrongKey();
        }
        else if (expected == Direction.Up)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) CheckInput(Direction.Up);
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                WrongKey();
        }
        else if (expected == Direction.Right)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) CheckInput(Direction.Right);
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                WrongKey();
        }
        else if (expected == Direction.Down)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)) CheckInput(Direction.Down);
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                WrongKey();
        }
        
        if (expected == Direction.HoldLeft)  HandleHold(KeyCode.LeftArrow);
        if (expected == Direction.HoldUp)    HandleHold(KeyCode.UpArrow);
        if (expected == Direction.HoldRight) HandleHold(KeyCode.RightArrow);
        if (expected == Direction.HoldDown)  HandleHold(KeyCode.DownArrow);
    }
    
    private void WrongKey()
    {
        LoseLife();
        StartCoroutine(NextCombinationWithDelay()); 
    }

    private void HandleHold(KeyCode key)
    {
        if (Input.GetKey(key))
        {
            if (!holding)
            {
                holding = true;
                currentHoldTime = 0f;

                if (holdProgressSlider != null)
                {
                    holdProgressSlider.gameObject.SetActive(true);
                    holdProgressSlider.value = 0f;
                }
            }

            currentHoldTime += Time.deltaTime;

            if (currentHoldTime >= holdTimeRequired)
            {
                holding = false;
                currentHoldTime = 0f;

                if (holdProgressSlider != null)
                    holdProgressSlider.gameObject.SetActive(false);

                currentInputIndex++;
                if (currentInputIndex >= currentCombination.Count)
                {
                    player.PlayerMoveUp();
                    StartCoroutine(NextCombinationWithDelay());
                }
            }
        }
        else if (holding)
        {
            holding = false;
            currentHoldTime = 0f;

            if (holdProgressSlider != null)
                holdProgressSlider.gameObject.SetActive(false);

            LoseLife();
        }
    }

    private void CheckInput(Direction input)
    {
        if (input == currentCombination[currentInputIndex])
        {
            currentInputIndex++;
            if (currentInputIndex >= currentCombination.Count)
            {
                player.PlayerMoveUp();
                StartCoroutine(NextCombinationWithDelay());
            }
        }
        else
        {
            LoseLife();
        }
    }

    private void LoseLife()
    {
        lives--;
        player.PlayerMoveDown();

        if (lives <= 0) FailGame();
        else StartCoroutine(NextCombinationWithDelay());
    }

    private System.Collections.IEnumerator NextCombinationWithDelay()
    {
        isOnCooldown = true;
        text.text = "...";
        yield return new WaitForSeconds(cooldownDuration);

        GenerateNewCombination();
        isOnCooldown = false;
    }

    private void GenerateNewCombination()
    {
        currentCombination.Clear();
        currentInputIndex = 0;

        for (int i = 0; i < difficultyLevel; i++)
        {
            bool isHold = Random.value < 0.2f; // 20% chance
            int rand = Random.Range(0, 4);
            Direction dir = (Direction)rand;

            if (isHold)
            {
                switch (dir)
                {
                    case Direction.Left: dir = Direction.HoldLeft; break;
                    case Direction.Up: dir = Direction.HoldUp; break;
                    case Direction.Right: dir = Direction.HoldRight; break;
                    case Direction.Down: dir = Direction.HoldDown; break;
                }
            }

            currentCombination.Add(dir);
        }

        UpdateCombinationDisplay();
    }

    private void UpdateCombinationDisplay()
    {
        string display = "";
        foreach (Direction dir in currentCombination)
        {
            switch (dir)
            {
                case Direction.Left: display += "←"; break;
                case Direction.Up: display += "↑"; break;
                case Direction.Right: display += "→"; break;
                case Direction.Down: display += "↓"; break;
                case Direction.HoldLeft: display += "[←]"; break;
                case Direction.HoldUp: display += "[↑]"; break;
                case Direction.HoldRight: display += "[→]"; break;
                case Direction.HoldDown: display += "[↓]"; break;
                
            }
        }
        text.text = display.Trim();
    }

    private void FailGame()
    {
        touchePanel.SetActive(false);
        text.text = "Game Over !";
        Debug.Log("Game Over - plus de vies");
    }
}
*/
