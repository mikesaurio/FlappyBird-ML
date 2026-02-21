using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Sprite[] numberSprites;   // 0–9
    public GameObject numberPrefab;  // Prefab del número
    public Transform scoreParent;    // ScoreDisplay

    private int score = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateScore();
    }

    public void AddPoint()
    {
        score++;
        Debug.Log("Score actual: " + score);
        UpdateScore();
    }
    public void ResetScore()
    {
        score = 0;
        UpdateScore();
    }

    void UpdateScore()
    {
        // Borrar números anteriores
        foreach (Transform child in scoreParent)
        {
            Destroy(child.gameObject);
        }

        string scoreString = score.ToString();

        float spacing = 1f;
        float startX = -(scoreString.Length - 1) * spacing / 2f;

        for (int i = 0; i < scoreString.Length; i++)
        {
            int digit = scoreString[i] - '0';

            GameObject numberObj = Instantiate(numberPrefab, scoreParent);
            numberObj.GetComponent<SpriteRenderer>().sprite = numberSprites[digit];
            numberObj.transform.localPosition = new Vector3(startX + i * spacing, 0, 0);
        }
    }
}
