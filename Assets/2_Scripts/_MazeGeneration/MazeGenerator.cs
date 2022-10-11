using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator
{
    private readonly int[] dx = new int[8] { 0, 0, -1, 1, -1, -1, 1, 1 };
    private readonly int[] dy = new int[8] { -1, 1, 0, 0, -1, 1, -1, 1};

    private Maze maze;
    private int[,] distances; // [Y, X]
    private int maxDistance = 0;

    public Maze MakeMazeDFS(int stage, int sizeX, int sizeY, int startX, int startY)
    {
        maze = new Maze(stage, sizeX, sizeY, startX, startY);

        InitDistances(sizeX, sizeY);

        int sx = Random.Range(0, sizeX);
        int sy = Random.Range(0, sizeY);

        DFS(sx, sy, 1);

        // minimum distance
        if(maxDistance < sizeX * sizeY * 0.5)
        {
            return MakeMazeDFS(stage, sizeX, sizeY, startX, startY);
        }

        FindEndPoint();

        MakeObstacles();

        return maze;
    }
    private void InitDistances(int sizeX, int sizeY)
    {
        distances = new int[sizeY, sizeX];
        maxDistance = 0;
        for (int y = 0; y < sizeY; ++y) for (int x = 0; x < sizeX; ++x) distances[y, x] = 0;
    }
    private void DFS(int x, int y, int dist)
    {
        distances[y, x] = dist;

        List<int> directionToGo = new List<int>() {0, 1, 2, 3};
        for(int i=4 ; i>0 ; --i)
        {
            int index = Random.Range(0, i);
            int dir = directionToGo[index];
            directionToGo.RemoveAt(index);

            int nx = x + dx[dir];
            int ny = y + dy[dir];

            if (nx < 0 || nx >= maze.sizeX || ny < 0 || ny >= maze.sizeY) continue;
            if (distances[ny, nx] > 0) continue;

            // 벽 파괴
            if (dir == 0) {maze.horizontalWalls[y, x] = false;} // 위
            else if (dir == 1) {maze.horizontalWalls[y + 1, x] = false;} // 아래
            else if (dir == 2) {maze.verticalWalls[y, x] = false;} // 왼쪽
            else if (dir == 3) {maze.verticalWalls[y, x + 1] = false;} // 오른쪽

            distances[ny, nx] = dist + 1;
            maxDistance = Mathf.Max(maxDistance, distances[ny, nx]);
            DFS(nx, ny, dist+1);
        }
    }
    private void FindEndPoint() // BFS
    {
        bool[,] visited = new bool[maze.sizeY, maze.sizeX];
        for (int y = 0; y < maze.sizeY; ++y) for (int x = 0; x < maze.sizeX; ++x) visited[y, x] = false;

        Queue<Vector2Int> q = new Queue<Vector2Int>();
        q.Enqueue(new Vector2Int(maze.startX, maze.startY));

        Vector2Int front = q.Peek();

        while(q.Count > 0)
        {
            front = q.Dequeue();
            visited[front.y, front.x] = true;

            for(int i=0 ; i<4 ; ++i)
            {
                if(i == 0 && maze.horizontalWalls[front.y, front.x]
                || i == 1 && maze.horizontalWalls[front.y + 1, front.x]
                || i == 2 && maze.verticalWalls[front.y, front.x]
                || i == 3 && maze.verticalWalls[front.y, front.x + 1]) continue;

                int nx = front.x + dx[i];
                int ny = front.y + dy[i];

                if (nx < 0 || nx >= maze.sizeX || ny < 0 || ny >= maze.sizeY || visited[ny, nx]) continue;

                q.Enqueue(new Vector2Int(nx, ny));
            }
        }

        maze.endX = front.x;
        maze.endY = front.y;
    }


    private void MakeObstacles()
    {
        // Select Position Randomly
        int X = maze.sizeX + 1;
        int Y = maze.sizeY + 1;
        int N = X * Y;
        int floor = GameData.Last.floor.value;
        int availableObstacleSize = (floor - 1) / 5 + 1;

        int[,] obstacles = new int[Y, X]; // -2면 빈칸, -1이면 다른 장애물의 사정거리 내에 있음, 0부터는 장애물의 type.
        for (int i = 0; i < N; ++i) obstacles[i / X, i % X] = -2;
        obstacles[0, 0]++;
        obstacles[1, 0]++;
        obstacles[0, 1]++;
        obstacles[1, 1]++;

        int maxObstaclesCnt = N/2 - 1;
        int oc = 0;

        List<int> remainPositions = new List<int>();
        for(int i=0; i<N ; ++i) remainPositions.Add(i);

        int[] obstacleTypes = new int[floor];
        for(int j=0 ; j<floor%5 ; ++j) for(int i=0 ; i<floor ; ++i) obstacleTypes[i] = i / 5;

        string s = "";
        for(int i=0 ; i<floor ; ++i)
        {
            s += obstacleTypes[i];
        }

        for (int i = N; i > 0 && oc < maxObstaclesCnt ; --i)
        {
            int posIndex = Random.Range(0, i);
            int x = remainPositions[posIndex] % X;
            int y = remainPositions[posIndex] / X;
            remainPositions.RemoveAt(posIndex);

            if(obstacles[y,x] > -2) continue;
            int r = Random.Range(0, floor);

            int obstacleTypeToMake = oc <= availableObstacleSize * ((floor-1) % 5 + 1) ? oc % availableObstacleSize : obstacleTypes[r];
            obstacles[y,x] = obstacleTypeToMake;
            oc++;

            for(int j=0 ; j<8 ; ++j)
            {
                int nx = x + dx[j];
                int ny = y + dy[j];
                if(nx < 0 || ny < 0 || nx >= X || ny >= Y || obstacles[ny,nx] > -2) continue;
                obstacles[ny,nx] = -1;
                int removingIndex = remainPositions.BinarySearch(ny * X + nx); // List가 정렬된 상태로 초기화되어있음.
                remainPositions.RemoveAt(removingIndex);
                i--;
            }
        }

        maze.obstacles = obstacles;
    }
}