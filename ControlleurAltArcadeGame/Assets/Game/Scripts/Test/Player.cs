using UnityEngine;

public class Player : MonoBehaviour
{
    public int CurrentHeight { get; private set; } = 0;

    public void Climb(int meters)
    {
        CurrentHeight += meters;
        transform.position += Vector3.up * meters;
    }

    public void Fall(int meters)
    {
        CurrentHeight -= meters;
        if (CurrentHeight < 0) CurrentHeight = 0;
        transform.position -= Vector3.up * meters;
    }
}