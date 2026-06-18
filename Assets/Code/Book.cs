using UnityEngine;

public class Book : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip bookSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Player player = other.GetComponent<Player>();

        if (player != null)
            player.bookCount++;

        audioSource.PlayOneShot(bookSound);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, bookSound.length);
    }
}