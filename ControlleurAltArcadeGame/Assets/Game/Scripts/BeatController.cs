/*

using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public AudioSource musicSource;
    public float songOffset = 0f;
    public NoteSpawner spawner;
    public TextAsset beatMapFile;

    private float songPosition;
    private float dspSongTime;
    private BeatEvent[] beatEvents;
    private int nextEventIndex = 0;
    private float songLength;

    void Start()
    {
        dspSongTime = (float)AudioSettings.dspTime;
        musicSource.PlayScheduled(AudioSettings.dspTime + 0.2f);
        songLength = musicSource.clip.length;
        LoadBeatMap();
    }

    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime) + songOffset;

        // Lire tous les events qui sont arrivés
        while (nextEventIndex < beatEvents.Length && beatEvents[nextEventIndex].time <= songPosition)
        {
            spawner.SpawnSpecificNote(beatEvents[nextEventIndex]);
            nextEventIndex++;
        }

        // Si la musique est terminée, on relance tout
        if (songPosition >= songLength)
        {
            RestartSongAndBeatmap();
        }
    }

    void RestartSongAndBeatmap()
    {
        dspSongTime = (float)AudioSettings.dspTime;
        musicSource.Stop();
        musicSource.PlayScheduled(AudioSettings.dspTime + 0.2f);
        nextEventIndex = 0; // recommence à lire le BeatMap depuis le début
    }

    void LoadBeatMap()
    {
        string[] lines = beatMapFile.text.Split('\n');

        // Crée une liste temporaire pour ne pas avoir d'éléments null
        List<BeatEvent> eventsList = new List<BeatEvent>();

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) 
                continue; 

            string[] parts = line.Trim().Split(':');
            if (parts.Length < 3)
                continue; 

            BeatEvent ev = new BeatEvent();

            if (!float.TryParse(parts[0], System.Globalization.NumberStyles.Float, 
                    System.Globalization.CultureInfo.InvariantCulture, out ev.time))
                continue; 

            if (!int.TryParse(parts[1], out ev.lane))
                continue; 

            ev.type = parts[2];

            if (ev.type == "hold" && parts.Length >= 4)
            {
                if (!float.TryParse(parts[3], System.Globalization.NumberStyles.Float, 
                        System.Globalization.CultureInfo.InvariantCulture, out ev.duration))
                {
                    ev.duration = 0f; // met 0 si la durée est invalide
                }
            }

            eventsList.Add(ev);
        }

        beatEvents = eventsList.ToArray();
    }

}

[System.Serializable]
public struct BeatEvent
{
    public float time;
    public int lane;
    public string type;
    public float duration;
}

*/
