using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class MazeFactory : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] protected GameObject[] obstaclePrefabs;

    [SerializeField] private float spaceSize = 1;
    [SerializeField] private float wallThickness = 0.1f;
    [SerializeField] private bool makeObstacles = true;

    private Maze maze;

    public void MakeMaze(Maze m)
    {
        maze = m;
        
        Make();
    }
    public void MakeMaze(string m)
    {
        maze = JsonConvert.DeserializeObject<Maze>(m);
        Make();
    }

    private void Make()
    {
        GameData.spaceSize = spaceSize;
        
        MakeFloor();
        MakeBall();
        MakeEndPoint();

        MakeHorizontalWalls();
        MakeVerticalWalls();

        MakeObstacles();
    }

    private void MakeHorizontalWalls()
    {
        // 행 고정 후 x좌표 순회
        for(int y = 0 ; y < maze.sizeY + 1 ; ++y)
        {
            int seq = 0;
            for(int x = 0 ; x < maze.sizeX ; ++x)
            {
                bool isWall = maze.horizontalWalls[y,x];

                if(isWall && x < maze.sizeX - 1)
                {
                    seq++;
                }
                else
                {
                    if (seq > 0 || x == maze.sizeX - 1)
                    {
                        GameObject wall = Instantiate(wallPrefab, this.transform);

                        // Set Scale
                        if (x == maze.sizeX - 1 && isWall)
                        {
                            x++;
                            seq++;
                        }

                        float wallLength = spaceSize * seq + wallThickness;
                        wall.transform.localScale = new Vector2(wallLength, wallThickness);

                        // Set Position
                        float posX = spaceSize * x - (spaceSize * seq * 0.5f);
                        float posY = y * spaceSize;
                        wall.transform.localPosition = new Vector2(posX, posY);
                    }

                    seq = 0;
                }
            }
        }
    }
    private void MakeVerticalWalls()
    {
        // 열 고정 후 y좌표 순회 (World에서는 z좌표임)
        for(int x = 0 ; x < maze.sizeX + 1 ; ++x)
        {
            int seq = 0;
            for(int y = 0 ; y < maze.sizeY ; ++y)
            {
                bool isWall = maze.verticalWalls[y,x];

                if (isWall && y < maze.sizeY - 1)
                {
                    seq++;
                }
                else
                {
                    if (seq > 0 || y == maze.sizeY - 1)
                    {
                        GameObject wall = Instantiate(wallPrefab, this.transform);

                        // Set Scale
                        if (y == maze.sizeY - 1 && isWall)
                        {
                            y++;
                            seq++;
                        }

                        float wallLength = spaceSize * seq + wallThickness;
                        wall.transform.localScale = new Vector2(wallThickness, wallLength);

                        // Set Position
                        float posX = x * spaceSize;
                        float posY = spaceSize * y - (spaceSize * seq * 0.5f);
                        wall.transform.localPosition = new Vector2(posX, posY);
                    }

                    seq = 0;
                }
            }
        }
    }

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject endPointPrefab;


    private void MakeFloor()
    {
        GameObject floor = Instantiate(floorPrefab, this.transform);

        float sizeX = spaceSize * maze.sizeX + wallThickness;
        float sizeY = spaceSize * maze.sizeY + wallThickness;
        floor.transform.localScale = new Vector2(sizeX, sizeY);
        float posX = spaceSize * maze.sizeX * 0.5f;
        float posY = spaceSize * maze.sizeY * 0.5f;
        floor.transform.localPosition = new Vector3(posX, posY, 1);
    }

    private void MakeBall()
    {
        Vector3 ballPos;

        if(GameData.wasPlaying.value)
        {
            ballPos = PlayerPrefsExt.GetObject<Vector3>(KeyData.LAST_POSITION, Vector3.zero);
        }
        else
        {
            ballPos = new Vector3(maze.startX + (spaceSize * 0.5f), maze.startY + (spaceSize * 0.5f), -2);
        }

        GameSceneObjects.Instance.ball.transform.position = ballPos;
    }
    private void MakeEndPoint()
    {
        GameSceneObjects.Instance.endPoint.transform.position
            = new Vector3((maze.endX + 0.5f) * spaceSize, (maze.endY + 0.5f) * spaceSize, -1);
    }

    public void MakeObstacles()
    {
        if(!makeObstacles) return;

        int lenY = maze.obstacles.GetLength(0);
        int lenX = maze.obstacles.GetLength(1);
        for (int y = 0; y < lenY; ++y) for (int x = 0; x < lenX; ++x)
        {
            int type = maze.obstacles[y, x];
            if(type < 0) continue;

            type %= obstaclePrefabs.Length;
            GameObject obstacle = Instantiate(obstaclePrefabs[type], this.transform);
            float posZ = obstacle.transform.position.z;
            obstacle.transform.position = new Vector3(x * spaceSize, y * spaceSize, posZ);


            // 오른쪽 : 0 x==lenX
            // 아래 : 90 y==lenY
            // 왼쪽 : 180 x==0
            // 위 : 270 y==0
            List<int> possibleRotations = new List<int> {0, 90, 180, 270};

            Vector3 r = obstacle.transform.rotation.eulerAngles;
            float rotZ = 0;

            if(y == 0) possibleRotations.RemoveAt(3);
            if(x == 0) possibleRotations.RemoveAt(2);
            if(y == lenY) possibleRotations.RemoveAt(1);
            if(x == lenX) possibleRotations.RemoveAt(0);

            int listSize = possibleRotations.Count;

            if(listSize == 0) Debug.LogError("Something Went Wrong on Maze!");

            rotZ = possibleRotations[Random.Range(0, listSize)];
            obstacle.transform.rotation = Quaternion.Euler(r.x, r.y, rotZ);
        }
    }
}