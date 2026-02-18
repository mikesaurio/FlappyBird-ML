using UnityEngine;

public class PipeSpawner : MonoBehaviour{
    public GameObject pipePrefab;
    public float spawnRate = 2f;
    public float minHeight = -1f;
    public float maxHeight = 2f;
    private float timer;

    void Update(){
        timer += Time.deltaTime;
        if (timer >= spawnRate){
            SpawnPipe();
            timer = 0f;
        }
    }

    void SpawnPipe(){
        float randomY = Random.Range(minHeight, maxHeight);
        Vector3 spawnPos = new Vector3(6f, randomY, 0f);

        Instantiate(pipePrefab, spawnPos, Quaternion.identity);
    }
}
