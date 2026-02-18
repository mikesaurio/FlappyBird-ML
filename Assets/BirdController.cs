using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour{
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isDead = false;
    public TextMeshProUGUI gameOverText;
    public float rotationSpeed = 5f;
    public float maxUpAngle = 30f;
    public float maxDownAngle = -90f;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if (isDead){
            RestartInput();
            return;
        }
            
        if (Input.GetMouseButtonDown(0)){
            rb.linearVelocity = Vector2.up * jumpForce;
        }

        float targetAngle = rb.linearVelocity.y * rotationSpeed;
        targetAngle = Mathf.Clamp(targetAngle, maxDownAngle, maxUpAngle);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0, 0, targetAngle),
            Time.deltaTime * 5f
        );
    }
    
    void OnCollisionEnter2D(Collision2D collision){
         Die();
    }
    
    void Die(){
        isDead = true;
        gameOverText.gameObject.SetActive(true);
        transform.rotation = Quaternion.Euler(0, 0, -90f);
        Debug.Log("GAME OVER - Bird is dead");
        Time.timeScale = 0f;
    } 

    void RestartInput(){
        if (Input.GetMouseButtonDown(0)){
            Time.timeScale = 1f;
            Debug.Log("Bird is alive");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
}

}