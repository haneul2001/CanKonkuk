using UnityEngine;

public class Grade : MonoBehaviour
{
    public float scoreValue;
    public float speed = 5f;
    public Vector2 direction;
    public GameObject hitEffectPrefab;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        
        if (Mathf.Abs(transform.position.x) > 10f || Mathf.Abs(transform.position.y) > 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)    
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity); //파티클 생성

            ScoreManager.instance.AddGrade(scoreValue);
            Destroy(gameObject);
        }
    }
}