using UnityEngine;

public class Book : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        Player player = other.GetComponent<Player>();
        if(player!=null) player.bookCount++;
        Destroy(gameObject);
    }
}
