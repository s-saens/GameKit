using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInstantiator : MonoBehaviour
{
    public MazeFactory mazeFactory;


    private MazeGenerator generator = new MazeGenerator();

    private void Awake()
    {
        Maze mazeToMake;
        mazeToMake = LoadMaze();

        mazeFactory.MakeMaze(mazeToMake);
        
        GameData.Last.maze.value = mazeToMake;

        GameData.wasPlaying.value = true;
    }

    private Maze LoadMaze()
    {
        if(GameData.wasPlaying.value)
        {
            if(!PlayerPrefs.HasKey(KeyData.LAST_MAZE)) Debug.LogError("NO MAZE SAVED");

            return PlayerPrefsExt.GetObject<Maze>(KeyData.LAST_MAZE, null);
        }
        else return generator.MakeMazeDFS(GameData.Last.floor.value, GameData.mazeSize.x, GameData.mazeSize.y, 0, 0);
    }
}
