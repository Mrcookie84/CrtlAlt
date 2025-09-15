using UnityEngine;
using UnityEngine.SceneManagement;

public class InnactivityManager : MonoBehaviour
{
    [SerializeField]private float innactivityTimer;
    [SerializeField]private float innactivityTimerMax;
    [SerializeField]private bool isPlaying = true;
    void Start()
    {
        
    }

    void Update()
    {
        if (!isPlaying)
        {
            UpdateTime();
        }
        else
        {
            ResetTimer();
        }

        if (innactivityTimer > innactivityTimerMax)
        {
            RestartGame();
        }
    }

    private void UpdateTime()
    {
        innactivityTimer += Time.deltaTime;
    }

    private void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        innactivityTimer = 0;
        
    }

    private void ResetTimer()
    {
        innactivityTimer = 0;
    }
}
