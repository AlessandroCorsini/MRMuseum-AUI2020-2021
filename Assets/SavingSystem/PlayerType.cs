using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerType : MonoBehaviour
{
    public float[] position;
    public static bool save = false;
    public static bool load = false;
    public static Vector3 copiedPosition;
    private static string nextWaypointName;
    public GameObject firstWaypoint;

    public void Update()
    {
        if (save)
        {
            save = false;
            SavePlayer();
        }

        if (load)
        {
            load = false;
            LoadPlayer();

        }
    }

    public void SavePlayer()
    {
        Saving.SavePlayer(this, nextWaypointName);
    }

    public void LoadPlayer()
    {
        PlayerData data = Saving.LoadPlayer();

        //Loading player position
        Vector3 position;
        position.x = data.position[0];
        position.y = transform.position.y;
        position.z = data.position[2];
        transform.position = position;
        copiedPosition = position;
        Debug.Log("POSITION LOADED =  x: " + position.x + " y: " + position.y + " z: " + position.z);

        //Loading last waypoint name
        Debug.Log("SAVED WAYPOINT NAME:" + nextWaypointName);

        MovePlayerAfterLoar();
    }

    public void MovePlayerAfterLoar()
    {
        //@Fede modifiche per salvataggio
        if (transform.position.x == 0f)
        {
            MuseumIntroduction.StartGameBool();
            SharedInfos.setMovement(new Vector3(firstWaypoint.transform.position.x, transform.position.y, firstWaypoint.transform.position.z));
        }
        else
        {
            MuseumIntroduction.NotNeededIntroduction();
            SharedInfos.setNextWaypointName(nextWaypointName);
            SharedInfos.setMovement(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            Debug.Log("Loaded" + transform.position.x + " " + transform.position.y + " " + transform.position.z);
        }
    }

    private void OnApplicationQuit()
    {
        Saving.Reset();
    }

    public static void InvokeSave()
    {
        save = true;
    }

    public static void InvokeLoad()
    {
        load = true;
    }

    public static Vector3 getPosition()
    {
        return copiedPosition;
    }

    public static void setWaypointName(string name)
    {
        if (name != "Waypoint start")
            nextWaypointName = name;
    }
}
