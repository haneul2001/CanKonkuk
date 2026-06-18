using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bookSound;
    public AudioClip sojuSound;
    public SpriteRenderer sr;
    public Sprite frontPlayer;
    public Sprite backPlayer;
    public Sprite rightPlayer;
    public Sprite leftPlayer;
    Rigidbody2D rb;
    public TileManager tileManager;
    //한칸 부드럽게 이동하는 시간
    float moveDuration = 0.12f;
    //한칸 이동하는 거리
    public float step = 1f;
    bool isMoving = false;
    public bool isDrunk = false;
    public int bookCount = 0;
    bool useBookMove = false;
    public bool canMove = false;
    Coroutine drunkCoroutine;
    public GameObject sojuEffect;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        sojuEffect.SetActive(false);
    }
    void Update()
    {
        if (!canMove) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseBook();
        }

        PlayerMove();


    }
    public void UseBook()
    {
        if(bookCount<=0) return;
        bookCount--;
        useBookMove = true;
        Debug.Log($"남은 책 : {bookCount}");
        audioSource.PlayOneShot(bookSound);
    }
  void PlayerMove()
{
    if(isMoving)
    {
        return;
    }

    Vector2 dir = Vector2.zero;

    if(Input.GetKeyDown(KeyCode.UpArrow))
    {
        dir = Vector2.up;
    }
    else if(Input.GetKeyDown(KeyCode.DownArrow))
    {
        dir = Vector2.down;
    }
    else if(Input.GetKeyDown(KeyCode.LeftArrow))
    {
        dir = Vector2.left;
    }
    else if(Input.GetKeyDown(KeyCode.RightArrow))
    {
        dir = Vector2.right;
    }

    if(isDrunk)
    {
        dir *= -1;
    }

    // 실제 이동 방향 기준 스프라이트 변경
    if(dir == Vector2.up)
    {
        sr.sprite = backPlayer;
    }
    else if(dir == Vector2.down)
    {
        sr.sprite = frontPlayer;
    }
    else if(dir == Vector2.left)
    {
        sr.sprite = leftPlayer;
    }
    else if(dir == Vector2.right)
    {
        sr.sprite = rightPlayer;
    }

    if(dir != Vector2.zero)
    {
        float currentStep = step;

        if (useBookMove)
        {
            currentStep = 2f;
            useBookMove = false;
        }

        Vector2 target = rb.position + dir * currentStep;

        Vector2Int targetGrid =
            Vector2Int.RoundToInt(target);

        if (tileManager.IsTileActive(targetGrid))
        {
            StartCoroutine(MoveTo(target));
        }
    }
}
       public void MobileMove(Vector2 dir)
{
    if (isMoving)
    {
        return;
    }

    if (isDrunk)
    {
        dir *= -1;
    }

    // 실제 이동 방향 기준 스프라이트 변경
    if(dir == Vector2.up)
    {
        sr.sprite = backPlayer;
    }
    else if(dir == Vector2.down)
    {
        sr.sprite = frontPlayer;
    }
    else if(dir == Vector2.left)
    {
        sr.sprite = leftPlayer;
    }
    else if(dir == Vector2.right)
    {
        sr.sprite = rightPlayer;
    }

    float currentStep = step;

    if (useBookMove)
    {
        currentStep = 2f;
        useBookMove = false;
    }

    Vector2 target = rb.position + dir * currentStep;

    Vector2Int targetGrid =
        Vector2Int.RoundToInt(target);

    if (tileManager.IsTileActive(targetGrid))
    {
        StartCoroutine(MoveTo(target));
    }
}
    public void DrinkSoju()
    {
        if(drunkCoroutine != null)
        {
            StopCoroutine(drunkCoroutine);
        }
        drunkCoroutine = StartCoroutine(DrunkCoroutine());
        audioSource.PlayOneShot(sojuSound);
    }
    IEnumerator DrunkCoroutine()
    {
        sojuEffect.SetActive(true);
        Time.timeScale=0.6f;
        Debug.Log("취함 시작");
        isDrunk = true;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale=1f;
        isDrunk = false;
        drunkCoroutine = null;
        sojuEffect.SetActive(false);
        Debug.Log("취함 종료");
    }
    IEnumerator MoveTo(Vector2 target)
    {
        isMoving = true;
        if(moveDuration <= 0f)
        {
            rb.MovePosition(target);
            isMoving = false;
            yield break;
        }

        Vector2 start = rb.position;
        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            rb.MovePosition(Vector2.Lerp(start, target, elapsed / moveDuration));
            yield return null;
        }
       rb.MovePosition(target);
        isMoving = false;

    }

   

}
