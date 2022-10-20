using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] GameObject[] tilePrefabs;
    [SerializeField] Transform MapPool;

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }



    private void Start()
    {
        CreateLevel();
        Vector3 playerPos = PlayerController.Instance.gameObject.transform.position;
        float a = UnityEngine.Random.Range(-3f, 3f);
        
    }
    void PlaceTile(string tileType, int x, int y, Vector3 worldStartPos)
    {
        int tileIndex = int.Parse(tileType);
        TileScript tile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();
        tile.SetUp(new Vector3(worldStartPos.x + TileSize * x, worldStartPos.y - TileSize * y, 0), MapPool);
    }
    void CreateLevel()
    {

        string[] mapData = ReadLevelText();


        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length - 1;

        Vector3 worldStartPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
                PlaceTile(newTiles[x].ToString(), x, y, worldStartPos);
            }
        }
    }

    string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);
        return data.Split('-');
    }
}
