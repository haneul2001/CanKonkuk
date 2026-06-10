using UnityEngine;

public class Grade : MonoBehaviour
{
    public float scoreValue;
    public float speed = 5f;
    public Vector2 direction;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // 3. Z축 대신 Y축(위아래)을 검사하도록 수정 완료
        if (Mathf.Abs(transform.position.x) > 10f || Mathf.Abs(transform.position.y) > 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)     // 플레이어와 충돌 시 오브젝트 삭제 함수
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.AddGrade(scoreValue);
            Destroy(gameObject);                // 충돌 물체가 플레이어 오브젝트 일 시 삭제
        }
    }
}