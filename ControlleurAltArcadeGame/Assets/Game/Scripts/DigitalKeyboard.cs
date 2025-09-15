using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DigitalKeyboardController : MonoBehaviour
{
    #region Parameters
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private int characterCount = 15;
    [SerializeField] private GameObject Menucanvas;
    private int characterLimit;
    private int currentIndex = 0;
    
    public Button[] keys;      
    public int keysPerRow = 10; 
    public ScoreManager scoreManagerscript;
    #endregion

    void Start()
    {
        HighlightKey(currentIndex);
        characterLimit = characterCount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveCursor(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveCursor(-1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveCursor(-keysPerRow);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveCursor(keysPerRow);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            PressKey();
        }
    }

    void MoveCursor(int step)
    {
        keys[currentIndex].GetComponent<Image>().color = Color.white; 
        currentIndex += step;

        if (currentIndex < 0)
        {
            currentIndex = 0;
        }
        if (currentIndex >= keys.Length)
        {
            currentIndex = keys.Length - 1;
        }
        HighlightKey(currentIndex);
    }

    void HighlightKey(int index)
    {
        keys[index].GetComponent<Image>().color = Color.yellow; 
    }

    public void PressKey()
    {
        string key; 
        if (keys[currentIndex].tag == "StartKey")
        {
            StartGame();
        }
        else if (keys[currentIndex].tag == "SpaceKey" && characterCount > 0)
        {
            key = " ";
            inputField.text += key;
            characterCount--;
            //Debug.Log("Input: " + key + ". Limit:" + characterCount);
        }
        else if (keys[currentIndex].tag == "SuprKey")
        {
            key = "";
            inputField.text = key;
            characterCount = characterLimit;
            //Debug.Log("Input: " + key + ". Limit:" + characterCount);
        }
        else if (characterCount > 0)
        {
            key = keys[currentIndex].GetComponentInChildren<TMP_Text>().text;
            inputField.text += key;
            characterCount--;
            //Debug.Log("Input: " + key + ". Limit:" + characterCount);
        }
    }

    public void StartGame()
    { 
        scoreManagerscript.AddPlayerName(inputField.text);
        Menucanvas.SetActive(false);
        //Debug.Log(inputField.text);
    }
}