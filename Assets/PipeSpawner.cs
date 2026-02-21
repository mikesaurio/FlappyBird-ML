using UnityEngine;
using System.Collections.Generic;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2f;
    public float minHeight = -1f;
    public float maxHeight = 2f;
    public bool isSpawning = true;

    private float timer;

    // 👇 Lista para guardar tuberías activas
    private List<GameObject> activePipes = new List<GameObject>();

    void Update()
    {
        if (!isSpawning) return;
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnPipe();
            timer = 0f;
        }
    }
    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    void SpawnPipe()
    {
        float randomY = Random.Range(minHeight, maxHeight);
        Vector3 spawnPos = new Vector3(6f, randomY, 0f);

        GameObject pipe = Instantiate(pipePrefab, spawnPos, Quaternion.identity);

        activePipes.Add(pipe); // 👈 guardamos referencia
    }

    // 🔥 MÉTODO CLAVE PARA ML-AGENTS
    public void ResetPipes()
    {
        StopSpawning();

        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");

        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
        }

        activePipes.Clear();

        timer = 0f;   // 👈 IMPORTANTE
    }
    
    private void Awake()
    {
        Debug.Log("Spawner creado: " + gameObject.GetInstanceID());
    }
}