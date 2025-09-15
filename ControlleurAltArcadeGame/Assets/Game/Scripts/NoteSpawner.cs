using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NoteSpawner : MonoBehaviour
{

    [Header("Game Manager")] public GameManager GameManager;
    
    [Header("Prefabs")]
    public GameObject tapNotePrefab;
    public GameObject holdNotePrefab;

    [Header("Lanes")]
    public Transform[] spawnPoints;

    [Header("Spawn Settings")]
    public float initialSpawnInterval = 10f;  // Intervalle de départ
    public float minSpawnInterval = 0.5f;    // Vitesse max (intervalle minimum)
    public float intervalDecrease = 0.02f;   // Réduction après chaque spawn
    public float holdNoteChance = 0.01f;      // % de chance qu'une hold note spawn (0.5 = 50%)

    private float currentSpawnInterval;
    
    private bool spawnLoopActive = false;
    private Coroutine spawnCoroutine;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
    }

    public void StartSpawnLoop()
    {
        if (spawnCoroutine == null)
        {
            spawnLoopActive = true;
            spawnCoroutine = StartCoroutine(SpawnLoop());
        }
    }

    public void StopSpawnLoop()
    {
        spawnLoopActive = false;

        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (spawnLoopActive)
        {
            SpawnRandomNote();

            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - intervalDecrease);

            yield return new WaitForSeconds(currentSpawnInterval);
        }

        spawnCoroutine = null; // Libère la référence une fois terminé
    }

    void SpawnRandomNote()
    {
        // Récupérer les lanes actuellement occupées par des HoldNotes
        HoldNote[] activeHoldNotes = FindObjectsOfType<HoldNote>();
        HashSet<int> blockedLanes = new HashSet<int>(activeHoldNotes.Select(hn => hn.lane));

        // Créer la liste des lanes disponibles
        List<int> availableLanes = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!blockedLanes.Contains(i))
                availableLanes.Add(i);
        }

        if (availableLanes.Count == 0)
        {
            Debug.Log("[NoteSpawner] Aucune lane dispo (toutes bloquées par des HoldNotes) → pas de note spawn.");
            return;
        }

        // Choisir une lane aléatoire parmi les lanes dispo
        int laneIndex = availableLanes[Random.Range(0, availableLanes.Count)];
        Transform spawnPoint = spawnPoints[laneIndex];

        // Décider aléatoirement entre tap ou hold
        GameObject prefabToSpawn = (Random.value < holdNoteChance) ? holdNotePrefab : tapNotePrefab;

        // Instancier la note
        GameObject note = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

        // Configurer seulement la lane
        Note noteScript = note.GetComponent<Note>();
        noteScript.lane = laneIndex;
    }
}
