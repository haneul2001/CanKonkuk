using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialPanel;

    void Start()
    {
        tutorialPanel.SetActive(false);
    }
    public void ToggleTutorial()
    {
        tutorialPanel.SetActive(!tutorialPanel.activeSelf);
    }
    public void OpenTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}