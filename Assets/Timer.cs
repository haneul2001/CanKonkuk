using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timerText;
    private float currentTime = 0f;
    public float MaxTime = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        MaxTime -= Time.deltaTime;

        int minute = Mathf.FloorToInt(MaxTime/60);
        int second = Mathf.FloorToInt(MaxTime%60);

        timerText.text = $"{minute:00}:{second:00}";
    }
}
