using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DigitalKeyboardController : MonoBehaviour
{
    #region Parameters
    [SerializeField] private TMP_Text text;
    [SerializeField] private int characterCount = 8;
    [SerializeField][Tooltip("Canvas du Keyboard")] private GameObject Keyboardcanvas;
    private int characterLimit;
    private int currentIndex = 0;
    
    public Button[] keys;      
    public int keysPerRow = 10; 
    public TableauDeScoreManager tableauDeScoreManagerscript;
    #endregion

    void Start()
    {
        HighlightKey(currentIndex);
        characterLimit = characterCount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            MoveCursor(1);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            MoveCursor(-1);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            MoveCursor(-keysPerRow);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            MoveCursor(keysPerRow);
        }
        else if (Input.GetKeyDown(KeyCode.X))
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
        if (keys[currentIndex].CompareTag("StartKey"))
        {
            EnterName();
            text.text = null;
        }
        else if (keys[currentIndex].CompareTag("SpaceKey") && characterCount > 0)
        {
            Debug.Log("Espace ajouté");
            key = " ";
            text.text += key;
            characterCount--;
        }
        else if (keys[currentIndex].CompareTag("SuprKey"))
        {
            Debug.Log("Suppression !");
            key = "";
            text.text = key;
            characterCount = characterLimit;
        }
        else if (characterCount > 0)
        {
            key = keys[currentIndex].GetComponentInChildren<TMP_Text>().text;
            Debug.Log("Ajout de la lettre : " + key);
            text.text += key;
            characterCount--;
        }
        else
        {
            Debug.Log("Limite atteinte, pas de saisie possible");
        }
    }

    public void EnterName()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            tableauDeScoreManagerscript.AddScore(text.text, gm.savedScore);
        }

        Keyboardcanvas.SetActive(false);

        UIScript ui = FindObjectOfType<UIScript>();
        if (ui != null)
        {
            ui.BackToStartMenu();
        }
    }
}