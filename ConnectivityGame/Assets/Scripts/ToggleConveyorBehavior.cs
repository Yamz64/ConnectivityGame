using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToggleConveyorBehavior : ConveyorBehavior
{
    public Vector2 clear_bounds;

    public AnimatedTile right;
    public AnimatedTile left;

    private Tilemap tiles;
    
    public List<Vector3Int> tile_data;

    private void Start()
    {
        tiles = GetComponent<Tilemap>();
        tile_data = new List<Vector3Int>();
        RefreshTiles();
    }

    private void Update()
    {
        if(toggled != direction)
        {
            direction = toggled;
            RefreshTiles();
        }
    }

    void RefreshTiles()
    {
        tile_data.Clear();

        //loop through tiles within the clear bounds
        for (int x = (int)clear_bounds.x; x < (int)Mathf.Abs(clear_bounds.x); x++)
        {
            for (int y = (int)clear_bounds.y; y < (int)Mathf.Abs(clear_bounds.y); y++)
            {
                Vector3Int temp = new Vector3Int(x, y, 0);
                if (tiles.GetTile<AnimatedTile>(temp) != null)
                {
                    tile_data.Add(temp);
                }
            }
        }

        //replace tiles at location
        for (int i = 0; i < tile_data.Count; i++)
        {
            //right
            if (!direction)
            {
                tiles.SetTile(tile_data[i], right);
            }
            //left
            else
            {
                tiles.SetTile(tile_data[i], left);
            }
        }
    }
}
