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

    // Referencia al agente
    public FlappyAgent agent;

    void Update()
    {
        if (!isSpawning) return;
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnPipe();
            timer = 0f;
        }

        // ✅ Actualizar la referencia de la tubería más cercana
        UpdateNextPipe();
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

    // ✅ Método para encontrar la tubería más cercana al jugador
    private void UpdateNextPipe()
    {
        if (agent == null || activePipes.Count == 0) return;

        GameObject closestPipe = null;
        float minDistance = float.MaxValue;

        foreach (GameObject pipe in activePipes)
        {
            if (pipe == null) continue;

            float dx = pipe.transform.position.x - agent.transform.position.x;

            // Solo considerar tuberías que estén adelante del jugador
            if (dx > 0 && dx < minDistance)
            {
                minDistance = dx;
                closestPipe = pipe;
            }
        }

        if (closestPipe != null)
        {
            agent.nextPipe = closestPipe.transform;
        }
    }

    // ✅ Método para remover tuberías ya pasadas
    public void RemovePipe(GameObject pipe)
    {
        if (activePipes.Contains(pipe))
        {
            activePipes.Remove(pipe);
        }
    }
}