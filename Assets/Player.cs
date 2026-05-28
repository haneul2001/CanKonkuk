using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    //한칸 부드럽게 이동하는 시간
    float moveDuration = 0.12f;
    //한칸 이동하는 거리
    public float step = 1f;
    bool isMoving = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerMove();


    }
    
    void PlayerMove()
    {
         if(isMoving)
        {
            return;
        }

        Vector2 dir = Vector2.zero;
        if(Input.GetKeyDown(KeyCode.UpArrow)){ dir = Vector2.up;}
        
        else if(Input.GetKeyDown(KeyCode.DownArrow)){dir = Vector2.down;}
      
        else if(Input.GetKeyDown(KeyCode.LeftArrow)){dir = Vector2.left;}
        
        else if(Input.GetKeyDown(KeyCode.RightArrow)){dir = Vector2.right;}

        if(dir != Vector2.zero)
        {
            Vector2 target = rb.position + dir * step;
            StartCoroutine(MoveTo(target));
        }
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
