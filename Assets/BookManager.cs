using UnityEngine;
using UnityEngine.UI;
public class BookManager : MonoBehaviour
{
    public Text bookText;
    public Player player;

    void Update()
    {
        bookText.text = "x : " + player.bookCount.ToString();
    }

}
