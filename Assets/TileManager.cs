using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TileManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public Transform player;
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

                Vector2Int key = new Vector2Int((int)x,(int)y);
                tiles[key] = tile;
                tile.name = $"{key.x},{key.y}";

                activeTiles.Add(key);}
        }
    }

    public void DisableTile()
{
    if (activeTiles.Count == 0) return;

    List<Vector2Int> candidates =
        new List<Vector2Int>(activeTiles);

    Vector2Int selectedPos =
        candidates[Random.Range(0, candidates.Count)];

    activeTiles.Remove(selectedPos);

    StartCoroutine(RemoveTileCoroutine(selectedPos));
}


    public bool IsTileActive(Vector2Int pos)
    {
        if(!tiles.ContainsKey(pos)) return false;
        return tiles[pos].activeSelf;
    }

   IEnumerator RemoveTileCoroutine(Vector2Int pos)
{
    GameObject tile = tiles[pos];
    SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();

    sr.color = Color.red;

    float duration = 1f;
    float elapsed = 0f;
    Color startColor = sr.color;

    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;

        Color c = startColor;
        c.a = Mathf.Lerp(1f, 0f, elapsed / duration);

        sr.color = c;

        yield return null;
    }

    Vector2Int playerPos =
        Vector2Int.RoundToInt(player.position);

    if (playerPos == pos)
    {
        ScoreManager.instance.GameOver();
        yield break;
    }
    Collider2D[] hits =
    Physics2D.OverlapCircleAll(tile.transform.position, 0.1f);
    Debug.Log($"찾은 오브젝트 수 : {hits.Length}");

    foreach (Collider2D hit in hits)
    {
        if(hit.CompareTag("Item"))
        {
            Destroy(hit.gameObject);
        }
    }
    tile.SetActive(false);
}
}
