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
            AddReward(-0.5f);
            EndEpisode();
        }
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("=== REINICIANDO EPISODIO ===");

        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        if (pipeSpawner != null)
        {
            pipeSpawner.ResetPipes();
            pipeSpawner.StartSpawning();
        }

        ScoreManager.instance.ResetScore();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position.y);
        sensor.AddObservation(rb.linearVelocity.y);

        if (nextPipe != null)
        {
            sensor.AddObservation(nextPipe.position.x - transform.position.x);
            sensor.AddObservation(nextPipe.position.y - transform.position.y);
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
        // 🔥 Mini recompensa por mantenerse estable en Y
        float distanceY = Mathf.Abs(transform.position.y);
        AddReward(-0.001f * distanceY);

        // 🔥 Recompensa pequeña por seguir vivo
        AddReward(0.0005f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLISION DETECTADA CON: " + collision.gameObject.name);
        AddReward(-0.5f);
        EndEpisode();
    }
}