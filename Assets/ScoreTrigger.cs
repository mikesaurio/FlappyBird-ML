using UnityEngine;

public class ScoreTrigger : MonoBehaviour{
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            Debug.Log("Paso por la tuberia!");

            FlappyAgent agent = collision.GetComponent<FlappyAgent>();

            if (agent != null){
                agent.AddReward(2f);
            }

            ScoreManager.instance.AddPoint();
        }
    }
}