using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Tilemapgenorator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile wallTile, doorTile, chestTile, floorTile, plantTile; // Tile assets for different map elements

    private int lastmap = 0;

    // Start is called before the first frame update
    void Start()
    {
            string premade = LoadPremadeLevel();
            ConvertMapToTilemap(premade);
    }

    // Converts the generated map string into a Unity Tilemap
    public void ConvertMapToTilemap(string mapData)
    {
        tilemap.ClearAllTiles(); // Clear any existing tiles

        string[] lines = mapData.TrimEnd().Split('\n');
        int height = lines.Length;
        int width = lines[0].Length;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width - 1; x++) // Added - 1 as the tiles extended one past the limit giving us an index out of range error
            {
                Vector3Int tilePosition = new Vector3Int(x, height - y - 1, 0);
                char tileChar = lines[y][x];

                // Place tiles based on the character
                switch (tileChar)
                {
                    case '#':
                        tilemap.SetTile(tilePosition, wallTile);
                        break;
                    case 'O':
                        tilemap.SetTile(tilePosition, doorTile);
                        break;
                    case '$':
                        tilemap.SetTile(tilePosition, chestTile);
                        break;
                    case '.':
                        tilemap.SetTile(tilePosition, floorTile);
                        break;
                    case '!':
                        tilemap.SetTile(tilePosition, plantTile);
                        break;
                    default:
                        tilemap.SetTile(tilePosition, floorTile);
                        break;
                }
            }
        }

    }

    public bool IsDoorTile(Vector3Int position)
    {
        TileBase tile = tilemap.GetTile(position);
        return tile == doorTile;
    }

    public bool IsTilePassable(Vector3Int position)
    {
        TileBase tile = tilemap.GetTile(position);

        // Check if the tile at the given position is impassable
        return tile != wallTile && tile != doorTile && tile != chestTile && tile != null;
    }

    public string LoadPremadeLevel()
    {
        int thismap = Random.Range(1, 5); // Generates a random number 1 - 4

        while (thismap == lastmap)
        {
            thismap = Random.Range(1, 5);
        }

        lastmap = thismap;

        string mapFileName = $"Map{thismap}.txt";
        string mapFilePath = $"{Application.streamingAssetsPath}/MapHolder/Maps/{mapFileName}";

        Debug.Log($"Attempting to load map from: {mapFilePath}");

        if (System.IO.File.Exists(mapFilePath))
        {
            StringBuilder mapContent = new StringBuilder();
            string[] lines = System.IO.File.ReadAllLines(mapFilePath);

            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    mapContent.AppendLine(line);
                }
            }

            return mapContent.ToString();
        }
        else
        {
            Debug.LogError($"File not found at: {mapFilePath}");
            return string.Empty;
        }
    }
}
