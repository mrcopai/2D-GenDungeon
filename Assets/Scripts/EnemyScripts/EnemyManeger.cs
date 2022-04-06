using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyManeger : MonoBehaviour
{
    [SerializeField]
    Tilemap Ground;
    [SerializeField]
    BoundsInt area = new BoundsInt(new Vector3Int(-15, -15, 0), new Vector3Int(30, 30, 1));

    public GameObject Enemy;
    public List<Vector3> availablePlaces;

    private void Start()
    {
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

        foreach (Vector3Int item in area.allPositionsWithin)
        {
            availablePlaces.Remove(item);
        }

        int podsnumber = availablePlaces.Count / 150;
        BoundsInt[] Pods = new BoundsInt[podsnumber];
        for (int i = 0; i < Pods.Length; i++)
        {
            Vector3Int pos = Vector3Int.FloorToInt(availablePlaces[Random.Range(0, availablePlaces.Count)]);
            Pods[i] = new BoundsInt(pos + new Vector3Int(-5,-5,0), new Vector3Int(10,10,1));
            List<Vector3> PODPlaces = new List<Vector3>();
            foreach (Vector3Int item in Pods[i].allPositionsWithin)
            {
                if (Ground.HasTile(item))
                {
                    PODPlaces.Add(item);
                }
            }
            int podSize = Random.Range(2,8);
            for (int j = 0; j < podSize; j++)
            {
                Instantiate(Enemy, (PODPlaces[Random.Range(0, PODPlaces.Count)] + new Vector3(0, 0, -1)), Quaternion.identity,gameObject.transform);

            }
        }
    }
}