using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FlappyAgent agent = collision.GetComponent<FlappyAgent>();

            if (agent != null)
            {
                // ✅ Recompensa clara al pasar tubería
                agent.AddReward(1f);
                Debug.Log("Tubo pasado. Total: " + agent.GetCumulativeReward());


                // ✅ eliminar tubería pasada de la lista
                if (agent.pipeSpawner != null)
                {
                    agent.pipeSpawner.RemovePipe(transform.parent.gameObject);
                }
            }

            ScoreManager.instance.AddPoint();
        }
    }
}