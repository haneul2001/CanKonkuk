using UnityEngine;

public class CelebrationEffect : MonoBehaviour
{
    void Start()
    {
        GetComponent<ParticleSystem>().Play();
    }
}