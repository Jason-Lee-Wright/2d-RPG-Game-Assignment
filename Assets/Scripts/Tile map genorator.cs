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

    // Start is called before the first frame update
    void Start()
    {
        string premade = LoadPremadeLevel();
        ConvertMapToTilemap(premade);
    }

    // Converts the generated map string into a Unity Tilemap
    void ConvertMapToTilemap(string mapData)
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

    public bool IsTilePassable(Vector3Int position)
    {
        TileBase tile = tilemap.GetTile(position);

        // Check if the tile at the given position is impassable
        return tile != wallTile && tile != doorTile && tile != chestTile && tile != null;
    }

    string LoadPremadeLevel()
    {
        string Map1 = Application.streamingAssetsPath + "/MapHolder/" + "/Maps/" + "Map1" + ".txt";

        if (System.IO.File.Exists(Map1))
        {
            StringBuilder map1 = new StringBuilder();
            string[] lines = System.IO.File.ReadAllLines(Map1);

            foreach (string line in lines)
            {
                if(!string.IsNullOrEmpty(line))
                {
                    map1.Append(line);
                }
            }

            return map1.ToString();
        }
        else
        {
            Debug.LogError("File not found at: " + Map1);
            return string.Empty;
        }
    }
}
