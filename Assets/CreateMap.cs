using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject tilePrefab;
    public  int mapSize = 7;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Init()
    {
        for(int i=0; i<mapSize; i++)
        {
            for(int j=0; j < mapSize; j++)
            {
                float x = -(mapSize / 2) + j;
                float y = (mapSize / 2) - i;

                Vector2 pos = new Vector2(x,y);
                GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                tile.name = $"{i},{j}";
            }
        }
    }
}
