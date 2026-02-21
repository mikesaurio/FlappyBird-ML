using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private FlappyAgent agent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<FlappyAgent>();
    }

    public void Jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce;
    }
}