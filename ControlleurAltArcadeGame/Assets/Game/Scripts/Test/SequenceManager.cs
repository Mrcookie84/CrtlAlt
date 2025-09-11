using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public static SequenceManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Sequence GenerateSequence(int playerHeight)
    {
        int length = Mathf.Clamp(1 + playerHeight / 20, 1, 4); // plus haut -> plus long
        bool includeHold = Random.value < 0.3f;
        return new Sequence(length, includeHold);
    }
}