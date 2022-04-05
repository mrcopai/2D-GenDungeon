using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    private Tile[] GroundSprites;
    [SerializeField]
    private Tile ExitTile;
    [SerializeField]
    private Tile leftWallTile;
    [SerializeField]
    private Tile righttWallTile;
    [SerializeField]
    private Tile pitTile;
    [SerializeField]
    private Tile topWallTile;
    [SerializeField]
    private Tile botWallTile;
    [SerializeField]
    private Tilemap groundMap;
    [SerializeField]
    private Tilemap pitMap;
    [SerializeField]
    private Tilemap wallMap;
    [SerializeField]
    private Tilemap ExitMap;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int deviationRate = 10;
    [SerializeField]
    private int roomRate = 15;
    [SerializeField]
    private int maxRouteLength;
    [SerializeField]
    private int maxRoutes = 20;


    private int routeCount = 0;
    private bool isEnd = false;

    private int xFinal;
    private int yFinal;
    private int LongestRoute;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //something dumb
        var vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = player.transform;

        int x = 0;
        int y = 0;
        int routeLength = 0;
        GenerateSquare(x, y, 1);
        Vector2Int previousPos = new Vector2Int(x, y);
        y += 3;
        GenerateSquare(x, y, 1);
        NewRoute(x, y, routeLength, previousPos);

        //gen exit
        isEnd = true;
        GenerateSquare(xFinal,yFinal,1);

        FillWalls();
    }

    private void FillWalls()
    {
        BoundsInt bounds = groundMap.cellBounds;
        for (int xMap = bounds.xMin - 10; xMap <= bounds.xMax + 10; xMap++)
        {
            for (int yMap = bounds.yMin - 10; yMap <= bounds.yMax + 10; yMap++)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                Vector3Int posBelow = new Vector3Int(xMap, yMap - 1, 0);
                Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);
                Vector3Int posSideL = new Vector3Int(xMap + 1, yMap, 0);
                Vector3Int posSideR = new Vector3Int(xMap - 1, yMap, 0);
                TileBase tile = groundMap.GetTile(pos);
                TileBase tileBelow = groundMap.GetTile(posBelow);
                TileBase tileAbove = groundMap.GetTile(posAbove);
                TileBase tileSideL = groundMap.GetTile(posSideL);
                TileBase tileSideR = groundMap.GetTile(posSideR);
                if (tile == null)
                {
                    pitMap.SetTile(pos, pitTile);
                    if (tileBelow != null)
                    {
                        wallMap.SetTile(pos, topWallTile);
                    }
                    else if (tileAbove != null)
                    {
                        wallMap.SetTile(pos, topWallTile);
                    }
                    else if (tileSideL != null)
                    {
                        wallMap.SetTile(pos, leftWallTile);
                    }
                    else if (tileSideR != null)
                    {
                        wallMap.SetTile(pos, righttWallTile);
                    }
                }
            }
        }
    }

    private void NewRoute(int x, int y, int routeLength, Vector2Int previousPos)
    {
        if (routeCount < maxRoutes)
        {
            routeCount++;
            while (++routeLength < maxRouteLength)
            {
                //Initialize
                bool routeUsed = false;
                int xOffset = x - previousPos.x; //0
                int yOffset = y - previousPos.y; //3
                int roomSize = 1; //Hallway size
                if (Random.Range(1, 100) <= roomRate)
                    roomSize = Random.Range(5, 12);
                previousPos = new Vector2Int(x, y);

                //Go Straight
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + xOffset, previousPos.y + yOffset, roomSize);
                        NewRoute(previousPos.x + xOffset, previousPos.y + yOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        x = previousPos.x + xOffset;
                        y = previousPos.y + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                //Go left
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x - yOffset, previousPos.y + xOffset, roomSize);
                        NewRoute(previousPos.x - yOffset, previousPos.y + xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        y = previousPos.y + xOffset;
                        x = previousPos.x - yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }
                //Go right
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + yOffset, previousPos.y - xOffset, roomSize);
                        NewRoute(previousPos.x + yOffset, previousPos.y - xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        y = previousPos.y - xOffset;
                        x = previousPos.x + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                if (!routeUsed)
                {
                    x = previousPos.x + xOffset;
                    y = previousPos.y + yOffset;
                    GenerateSquare(x, y, roomSize);
                }
                if (routeLength > LongestRoute)
                {
                    LongestRoute = routeLength;
                    xFinal = x;
                    yFinal = y;
                }
            }
        }
    }

    private void GenerateSquare(int x, int y, int radius)
    {
        for (int tileX = x - radius; tileX <= x + radius; tileX++)
        {
            for (int tileY = y - radius; tileY <= y + radius; tileY++)
            {
                Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                Tile ground = randTile();
                if (isEnd == true)
                {
                    ExitMap.SetTile(tilePos, ExitTile);
                }
                else
                {
                    groundMap.SetTile(tilePos, ground);
                }
            }
        }
    }
    private Tile randTile()
    {
        var floor2 = 15.0f;
        var floor3 = 30.0f;
        var floor4 = 32.0f;
        var floor5 = 34.0f;
        var floor6 = 36.0f;
        var floor7 = 38.0f;

        var choosen = Random.Range(0.0f, 300.0f);
        Tile groundtile = null;

        if (choosen >= 0f && choosen < floor2)
        {
            groundtile = GroundSprites[1];
        }
        else if (choosen >= floor2 && choosen < floor3)
        {
            groundtile = GroundSprites[2];
        }
        else if (choosen >= floor3 && choosen < floor4)
        {
            groundtile = GroundSprites[3];
        }
        else if (choosen >= floor4 && choosen < floor5)
        {
            groundtile = GroundSprites[4];
        }
        else if (choosen >= floor5 && choosen < floor6)
        {
            groundtile = GroundSprites[5];
        }
        else if (choosen >= floor6 && choosen <= floor7)
        {
            groundtile = GroundSprites[6];
        }
        else
        {
            groundtile = GroundSprites[0];
        }
        return groundtile;
    }
}