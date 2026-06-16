using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Timer : MonoBehaviour
{
    public GameManager gameManager;
    Text timerText;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText = GetComponent<Text>();
        StartCoroutine(TimerUpdate());
    }
    IEnumerator TimerUpdate()
    {
        int lastSecond = -1;
        while (true)
        {
            int currentTime = 
            Mathf.FloorToInt(gameManager.TimeLimit);

            if(currentTime !=lastSecond)
            {
                lastSecond = currentTime;

                int minute = currentTime/60;
                int second = currentTime%60;
            
            

            timerText.text = $"남은 시간 : {minute:00}:{second:00}";
            }
            yield return null;
        }
    }

}
