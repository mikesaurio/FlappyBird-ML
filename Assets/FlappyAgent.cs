using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class FlappyAgent : Agent
{
    private Rigidbody2D rb;

    public PipeSpawner pipeSpawner;
    public Transform nextPipe;

    private Vector3 startPosition;

    public float jumpForce = 5f;

    // 🔥 límites verticales
    public float upperLimit = 5f;
    public float lowerLimit = -5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        RequestDecision();
    }
    private void Update()
    {
        if (transform.position.y > upperLimit || transform.position.y < lowerLimit)
        {
            Debug.Log("Murió por salir de límites");
            AddReward(-1f);
            EndEpisode();
        }
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("=== REINICIANDO EPISODIO ===");

        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        BackgroundScroll[] backgrounds = FindObjectsOfType<BackgroundScroll>();
        foreach (BackgroundScroll bg in backgrounds)
        {
            bg.ResetPosition();
        }

        if (pipeSpawner != null){
            pipeSpawner.ResetPipes();
            pipeSpawner.StartSpawning();
        }

        ScoreManager.instance.ResetScore();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float maxHeight = 5f;
        float maxDistanceX = 10f;
        float maxVelocity = 10f;

        // Altura normalizada
        sensor.AddObservation(transform.position.y / maxHeight);

        // Velocidad vertical normalizada
        sensor.AddObservation(rb.linearVelocity.y / maxVelocity);

        if (nextPipe != null)
        {
            float dx = (nextPipe.position.x - transform.position.x) / maxDistanceX;
            float dy = (nextPipe.position.y - transform.position.y) / maxHeight;

            sensor.AddObservation(dx);
            sensor.AddObservation(dy);
        }
        else
        {
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] == 1)
        {
            rb.linearVelocity = Vector2.up * jumpForce;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLISION DETECTADA CON: " + collision.gameObject.name);
        AddReward(-1f);
        EndEpisode();
    }
}