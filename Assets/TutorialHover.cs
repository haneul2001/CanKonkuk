using UnityEngine;
using UnityEngine.EventSystems;
public class TutorialHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tutorialImage;
    void Start()
    {
        tutorialImage.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tutorialImage.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tutorialImage.SetActive(false);
    }
}
