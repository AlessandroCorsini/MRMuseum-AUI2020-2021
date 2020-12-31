using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedInfos
{
    private static string lastWaypointMove = "None";
    private static string nextWaypointMove = "Waypoint start";
    private static Vector3 movement;
    private static Transform playerTransform;
    private static int playerLayerIndex;


    public static string getNextWaypointName()
    {
        return nextWaypointMove;
    }

    public static void setNextWaypointName(string nameNextWaypoint)
    {
        nextWaypointMove = nameNextWaypoint;
    }

    public static string getLastWaypointName ()
    {
        return lastWaypointMove;
    }

    public static void setLastWaypointName(string nameLastWaypoint)
    {
        lastWaypointMove = nameLastWaypoint;
    }

    public static Vector3 getMovement()
    {
        return movement;
    }

    public static void setMovement(Vector3 movementVec)
    {
        movement = movementVec;
    }

    public static Transform getPlTransform()
    {
        return playerTransform;
    }

    public static void setPlTransform(Transform transform)
    {
        playerTransform = transform;
    }

    public static int getPlayerLayer()
    {
        return playerLayerIndex;
    }

    public static void setPlayerLayer(int layerIndex)
    {
        playerLayerIndex = layerIndex;
    }
}
