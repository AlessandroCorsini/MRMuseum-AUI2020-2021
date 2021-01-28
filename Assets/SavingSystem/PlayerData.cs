using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public string nextWaypointName;
    public bool restart = false;

    public PlayerData(PlayerType player, string waypointName)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        if(waypointName!=null)
            nextWaypointName = waypointName;
        else
            nextWaypointName = "Waypoint start";
    }

    public PlayerData()
    {
        position = new float[3];
        position[0] = 0f;
        position[1] = 0f;
        position[2] = 0f;
        nextWaypointName = "Waypoint start";
    }
}
