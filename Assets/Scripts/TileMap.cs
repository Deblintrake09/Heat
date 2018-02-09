using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    
    public TileType[] tileTypes;

    int[,] tiles;

    int mapSizeX = 15;
    int mapSizeY = 15;

    private void Start()
    {
        tiles = new int[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                int tileToBe = Random.Range(0, 101);

                if (tileToBe >= 0 && tileToBe < 35)
                {
                    tileToBe = 0;
                }
                else if ((tileToBe >= 36 && tileToBe < 60))
                {
                    tileToBe = 1;
                }
                else if ((tileToBe >= 61 && tileToBe < 75 ))
                {
                    tileToBe = 2;
                }
                else
                {
                    tileToBe = 3;
                }
                tiles[x, y] = tileToBe;
            }
        }
        GenerateMapVisual();
    }

    void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[ tiles[x, y] ];
                GameObject tileObject = Instantiate(tt.tilePrefab, new Vector3(x, 0.5f, y), Quaternion.identity);
                tileObject.isStatic = true;
                tileObject.transform.SetParent(transform);
            }
        }
    }
}
