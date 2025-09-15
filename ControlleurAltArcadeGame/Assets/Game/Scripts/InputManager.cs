using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour
{
    private KeyCode[] keys = { KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N };

    void Update()
    {
        for (int lane = 0; lane < keys.Length; lane++)
        {
            if (Input.GetKeyDown(keys[lane]))
            {
                PressLane(lane);
            }

            if (Input.GetKeyUp(keys[lane]))
            {
                ReleaseLane(lane);
            }
        }
    }

    void PressLane(int lane)
    {
        // Cherche toutes les notes dans la scène
        Note[] allNotes = FindObjectsOfType<Note>();

        // Sélectionne la note la plus proche dans la bonne lane et frappable
        Note noteToHit = allNotes
            .Where(n => n.lane == lane && n.canBePressed)
            .OrderBy(n => Mathf.Abs(n.transform.position.y)) // la plus proche du centre
            .FirstOrDefault();

        if (noteToHit != null)
        {
            if (noteToHit is HoldNote holdNote)
                holdNote.StartHolding();
            else
                noteToHit.Pressed();
        }
    }

    void ReleaseLane(int lane)
    {
        HoldNote[] holdNotes = FindObjectsOfType<HoldNote>();
        foreach (var hn in holdNotes)
        {
            if (hn.lane == lane)
            {
                hn.StopHolding();
            }
        }
    }
}