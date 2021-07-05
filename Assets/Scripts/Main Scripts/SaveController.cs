using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public void SaveGame(SaveData gameSave)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save_game.dat");
        bf.Serialize(file, gameSave);
        file.Close();
    }

    public SaveData LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save_game.dat"))
        {
            //Debug.Log("Found save data.");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save_game.dat", FileMode.Open);
            SaveData gameSave = (SaveData)bf.Deserialize(file);
            file.Close();
            return gameSave;
        }
        else
        {
            Debug.Log(string.Format("File doesn't exist at path: {0}{1}", Application.persistentDataPath, "/save_game.dat"));
            return null;
        }

    }
    public void DeleteGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save_game.dat"))
        {
            //Debug.Log("Found save data.");
            File.Delete(Application.persistentDataPath + "/save_game.dat");
            return;
        }
        else
        {
            Debug.Log(string.Format("File doesn't exist at path: {0}{1}", Application.persistentDataPath, "/save_game.dat"));
            return;
        }
    }
}
