using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyManeger : MonoBehaviour
{
    [SerializeField]
    GameObject chest;
    [SerializeField]
    Tilemap Ground;
    [SerializeField]
    BoundsInt area = new BoundsInt(new Vector3Int(-15, -15, 0), new Vector3Int(30, 30, 1));

    public GameObject[] SmallEnemy;
    public GameObject[] MediumEnemy;
    public GameObject[] LargeEnemy;
    public List<Vector3> availablePlaces;
    private int NumberOfChests;

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
            CreatePod(podSize, PODPlaces);
        }
        NumberOfChests = Random.Range(-1, 3);
        for (int i = 0; i < NumberOfChests; i++)
        {
            Instantiate(chest, availablePlaces[Random.Range(0, availablePlaces.Count)] + new Vector3(0, 0, -1), Quaternion.identity, gameObject.transform);
        }
    }
    private void CreatePod(int PodSize, List<Vector3> PODPlaces)
    {
        for (int i = 0; i < PodSize; i++)
        {
            int EnemyType = Random.Range(0,100);
            if (EnemyType <50)
            {
                //create Small Enemy
                Instantiate(SmallEnemy[Random.Range(0,SmallEnemy.Length)], PODPlaces[Random.Range(0, PODPlaces.Count)] + new Vector3(0, 0, -1), Quaternion.identity, gameObject.transform);
            }
            else if (EnemyType < 85)
            {
                //create Medium Enemy
                Instantiate(MediumEnemy[Random.Range(0, MediumEnemy.Length)], PODPlaces[Random.Range(0, PODPlaces.Count)] + new Vector3(0, 0, -1), Quaternion.identity, gameObject.transform);
            }
            else
            {
                //create Large Enemy
                Instantiate(LargeEnemy[Random.Range(0, LargeEnemy.Length)], PODPlaces[Random.Range(0, PODPlaces.Count)] + new Vector3(0, 0, -1), Quaternion.identity, gameObject.transform);
            }
        }
    }
}