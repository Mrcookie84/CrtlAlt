using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField]private GameObject player;
    private int randomNumber;
    private int inputNumber;
    
    public GameObject[] Buttons;

    private void Start()
    {
        ChoseNumber();
    }
    private void ChoseNumber()
    {
        randomNumber = Random.Range(0, 4);
        Coordination();
    }

    private void Coordination()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            inputNumber = 0;
            Buttons[inputNumber].SetActive(true);   
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            inputNumber = 1;
            Buttons[inputNumber].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            inputNumber = 2;
            Buttons[inputNumber].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            inputNumber = 3;
            Buttons[inputNumber].SetActive(true);
        }

        Move();
    }

    private void Move()
    {
        if (randomNumber == inputNumber)
        {
            player.transform.Translate(Vector2.up);
        }
        else
        { 
            player.transform.Translate(Vector2.down);
        }

        ResetButtons();
    }

    private void ResetButtons()
    {
        foreach (GameObject button in Buttons)
        {
            button.SetActive(false);
        }
        ChoseNumber();
    }
    
    
    
    




}
