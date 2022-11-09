using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// Responsible for just loading and saving to a file. That's it.
/// </summary>
public class LoadingSavingManager
{
    public static void SaveGame(Savegame savegame)
    {
        string path = Application.persistentDataPath + "/save.happycamp";
        using FileStream fileStream = File.Create(path);
        string jsonStringSaveGame = JsonUtility.ToJson(savegame);
        AddText(fileStream, jsonStringSaveGame);
    }

    public static Savegame LoadGame()
    {
        string path = Application.persistentDataPath + "/save.happycamp";

        using (FileStream fs = File.OpenRead(path))
        {
            if (File.Exists(path))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                string json = temp.GetString(b);
                int i = 0;
            }
            else
            {
                return null;
            }
        }
        return null;
    }

    private static void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }
}
