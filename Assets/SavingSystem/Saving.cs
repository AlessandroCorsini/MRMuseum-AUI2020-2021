using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Saving
{

    public static string path = Application.persistentDataPath + "/MuseumPosition.fun";

    public static void SavePlayer(PlayerType player, string waypointName)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, waypointName);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found" + path);

            PlayerData data = new PlayerData();
            return data;
        }
    }

    public static void Reset()
    {
        Debug.Log("Reset chiamato");

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Truncate);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

}
