using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
{
    Debug.Log("Algo entró al trigger");

    if (collision.CompareTag("Player"))
    {
        Debug.Log("Es el Player!");
        ScoreManager.instance.AddPoint();
    }
}
}
