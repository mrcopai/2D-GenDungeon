using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyManeger : MonoBehaviour
{
    [SerializeField]
    Tilemap Ground;
    public BoundsInt area;

    [Header("Enemy1")]
    public GameObject Enemy1;
    public List<Vector3> availablePlaces;

    private void Start()
    {
        //Position von jedem Floortile ermitteln:
        availablePlaces = new List<Vector3>();

        for (int n = Ground.cellBounds.xMin; n < Ground.cellBounds.xMax; n++)
        {
            for (int p = Ground.cellBounds.yMin; p < Ground.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)Ground.transform.position.y);
                Vector3 place = Ground.CellToWorld(localPlace);
                if (Ground.HasTile(localPlace))
                {
                    availablePlaces.Add(place);
                }
            }
        }
        Debug.Log(availablePlaces.Count);
        TileBase[] tileArray = Ground.GetTilesBlock(area);
        Debug.Log(tileArray.Length);

        foreach (Tile item in tileArray)
        {
            Vector3 cellPosition = MatrixExtensions.ExtractPosition(item.transform);
            cellPosition = Ground.WorldToCell(cellPosition);
            availablePlaces.Remove(cellPosition);
        }
        Debug.Log(availablePlaces.Count);



        for (int i = 0; i < 70; i++)
        {
            Instantiate(Enemy1, (availablePlaces[Random.Range(0, availablePlaces.Count)] + new Vector3(0, 0, -1)), Quaternion.identity);
        }
    }
}
