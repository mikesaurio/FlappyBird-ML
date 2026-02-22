using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float speed = 0.8f;

    private float width;
    private Vector3 startPosition;

    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= -width + 0.01f)
        {
            transform.position += Vector3.right * width * 2f;
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }
}