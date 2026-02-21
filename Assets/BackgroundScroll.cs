using UnityEngine;

public class BackgroundScroll : MonoBehaviour{
    public float speed = 1f;
    private float width;

    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;

        if (transform.position.x > 0)
            transform.position = new Vector3(width, transform.position.y, transform.position.z);
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= -width)
        {
            transform.position += Vector3.right * width * 2f;
        }
    }
}
