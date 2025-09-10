using UnityEngine;
using UnityEngine.SceneManagement;

public class InnactivityManager : MonoBehaviour
{
    [SerializeField]private float innactivityTimer;
    private bool isPlaying = true;
    void Start()
    {
        
    }

    void Update()
    {
        if (!isPlaying)
        {
            UpdateTime();
        }

        if (innactivityTimer > 15)
        {
            ResetGame();
        }
    }

    private void UpdateTime()
    {
        innactivityTimer += Time.deltaTime;
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        innactivityTimer = 0;
    }
}
