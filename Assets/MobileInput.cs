using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public Player player;

    public void Up()
    {
        player.MobileMove(Vector2.up);
    }

    public void Down()
    {
        player.MobileMove(Vector2.down);
    }

    public void Left()
    {
        player.MobileMove(Vector2.left);
    }

    public void Right()
    {
        player.MobileMove(Vector2.right);
    }

    public void UseBook()
    {
        player.UseBook();
    }
}