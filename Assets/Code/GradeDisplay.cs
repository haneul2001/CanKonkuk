using UnityEngine;
using UnityEngine.UI;
public class GradeDisplay : MonoBehaviour
{
    public Image gradeImage;
    public Sprite aPlus;
    public Sprite a;
    public Sprite bPlus;
    public Sprite b;

    // Update is called once per frame
    void Update()
    {
        if(ScoreManager.instance == null) return;

        if(ScoreManager.instance.gradeCount == 0)
        {
            gradeImage.enabled = false;
            return;
        }
        gradeImage.enabled = true;

        float gpa = ScoreManager.instance.averageGPA;

        if (gpa >= 4.5f)
        {
            gradeImage.sprite = aPlus;
        }
        else if (gpa >= 4.0f)
        {
            gradeImage.sprite = a;
        }
        else if (gpa >= 3.5f)
        {
            gradeImage.sprite = bPlus;
        }
        else
        {
            gradeImage.sprite = b;
        }

    }
}
