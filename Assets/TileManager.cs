using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TileManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public int mapSize = 7; //맵 크기

    public Dictionary<Vector2Int, GameObject> tiles
        = new Dictionary<Vector2Int, GameObject>();

    public List<Vector2Int> activeTiles =
        new List<Vector2Int>();

    void Start()
    {
        CreateTiles();
    }

    void CreateTiles() //게임 시작 시 타일 생성 
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                float x = -(mapSize / 2) + j;
                float y = (mapSize / 2) - i;

                Vector2 pos = new Vector2(x, y);

                GameObject tile =
                    Instantiate(tilePrefab, pos, Quaternion.identity);

                Vector2Int key = new Vector2Int(i, j);
                tiles[key] = tile;
                tile.name = $"{i},{j}";

                activeTiles.Add(key);}
        }
    }

    public void DisableTile()
    {
        if(activeTiles.Count == 0) return;

        int randomIndex = 
            Random.Range(0, activeTiles.Count);
        
        Vector2Int pos =
            activeTiles[randomIndex];
        tiles[pos].SetActive(false);

        activeTiles.RemoveAt(randomIndex);
    }
}
