using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float timer;
    private bool keepTiming;
    
    public TextMeshProUGUI[] scoreText;
    public TextMeshProUGUI timerText;
    
    void Start()
    {
        keepTiming = true;
    }

    void Update()
    {
        if (keepTiming = true)
        {
            UpdateTime();
        }
    }

    private void UpdateTime()
    {
        timer += Time.deltaTime;
        DisplayTime(timer);
    }

    private void DisplayTime(float timeToDisplay)
    {
        var t0 = (int)timeToDisplay;

        var m = t0 / 60;

        var s = t0 - m * 60;
        var ms = (int)((timeToDisplay - t0) * 1000);

        timerText.text = $"{m:00}:{s:00}:{ms:000}";
    }

    private void OnFinishLine()
    {
        keepTiming = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            OnFinishLine();
        }
    }
}

