using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectUIController : MonoBehaviour
{
    public void SaveGameTest()
    {
        LoadingSavingManager.SaveGame(new Savegame(8));
    }

    public void LoadGameTest()
    {
        Savegame game = LoadingSavingManager.LoadGame();
        if(game != null)
        {
            Debug.Log("Game i:" + game.i);
        } else
        {
            Debug.Log("Null");
        }
    }
}
