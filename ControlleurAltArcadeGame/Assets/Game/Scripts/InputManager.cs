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
                Debug.Log($"[InputManager] Touche pressée : {keys[lane]} (Lane {lane})");
                PressLane(lane);
            }

            if (Input.GetKeyUp(keys[lane]))
            {
                Debug.Log($"[InputManager] Touche relâchée : {keys[lane]} (Lane {lane})");
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
            Debug.Log($"[InputManager] Note détectée dans Lane {lane} → HIT !");
            if (noteToHit is HoldNote holdNote)
                holdNote.StartHolding();
            else
                noteToHit.Pressed();
        }
        else
        {
            Debug.Log($"[InputManager] Aucune note frappable trouvée dans Lane {lane}");
        }
    }

    void ReleaseLane(int lane)
    {
        // Si on relâche une touche, on avertit les hold notes en cours
        HoldNote[] holdNotes = FindObjectsOfType<HoldNote>();
        foreach (var hn in holdNotes)
        {
            if (hn.lane == lane)
            {
                Debug.Log($"[InputManager] Relâche détecté sur une HoldNote Lane {lane} → Vérification maintien");
                hn.StopHolding();
            }
        }
    }
}