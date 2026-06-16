using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public GameObject hitF;
    public GameObject avgDown;

    void Start()
    {
        hitF.SetActive(false);
        avgDown.SetActive(false);

        if (GameOverReason.hitF)
        {
            hitF.SetActive(true);
        }
        else
        {
            avgDown.SetActive(true);
        }
    }
}