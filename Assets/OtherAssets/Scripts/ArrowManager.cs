using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager
{

    private static List<string> starredDirections;
    private static List<string> disabledDirections;

    public static string nextSceneName;
    public static string miniGameName;

    private static string raycastedArrow = "None";
    private static bool hitRaycasting = false;
    private static bool unhitRaycasting = true;
    /*
    public static void decodeStarredDirections(string[] directions)
    {
        List<string> directionList = new List<string>();
        Debug.Log("STARRED DIRECTIONS: length=" + directions.Length);
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.Log(directions[i]);
        }
        for (int i=0; i < directions.Length; i++) {
            if (directions[i].Equals("N"))
            {
                directionList.Add("North");
            }
            else if (directions[i].Equals("W"))
            {
                directionList.Add("West");
            }
            else if (directions[i].Equals("S"))
            {
                directionList.Add("South");
            }
            else if (directions[i].Equals("E"))
            {
                directionList.Add("East");
            }
        }

        starredDirections = directionList;
        Debug.Log("STARRED DIRECTIONS: after calculus, length=" + starredDirections.ToString());
    }
    */

    public static void decodeStarredDirections(bool[] directions)
    {
        string[] directionsString = { "North", "West", "East", "South" };

        List<string> directionList = new List<string>(directionsString);
        // Debug.Log("STARRED DIRECTIONS: length=" + directions.Length);

        if (!directions[0])
        {
            directionList.Remove("North");
        }
        if (!directions[1])
        {
            directionList.Remove("South");
        } 
        if (!directions[2])
        {
            directionList.Remove("West");
        } 
        if (!directions[3])
        {
            directionList.Remove("East");
        }

        starredDirections = directionList;
    }


    public static void decodeDisabledDirections (string[] directions)
    {
        string[] directionsString = { "North", "West", "East", "South" };

        List<string> directionList = new List<string>(directionsString);

        for (int i=0; i < directions.Length; i++)
        {
            if (directions[i] != null) // if it is not null
            {
                string wpname = directions[i];

                if (wpname.Equals("wayPointNorth"))
                {
                    directionList.Remove("North");
                }
                else if (wpname.Equals("wayPointWest"))
                {
                    directionList.Remove("West");
                }
                else if (wpname.Equals("wayPointEast"))
                {
                    directionList.Remove("East");
                }
                else if (wpname.Equals("wayPointSouth"))
                {
                    directionList.Remove("South");
                }
            }
        }

        disabledDirections =  directionList;
    }

    public static List<string> getDisabledDirections()
    {
        return disabledDirections;
    }

    public static List<string> getStarredDirections()
    {
        return starredDirections;
    }

    public static bool isHitRaycasting()
    {
        return hitRaycasting;
    }

    public static bool isUnhitRaycasting()
    {
        return unhitRaycasting;
    }

    public static void setRaycasting(string name)
    {
        raycastedArrow = name;
        hitRaycasting = true;
        unhitRaycasting = false;
    }

    public static string getRaycastedArrowName()
    {
        return raycastedArrow;
    }

    public static void resetRaycastedArrowName()
    {
        raycastedArrow = "None";
    }

    public static void clearRaycasting()
    {
        hitRaycasting = false;
        unhitRaycasting = true;
    }

    public static void saveNextSceneName(string sceneName)
    {
        nextSceneName = sceneName;
    }

    public static string getNextSceneName()
    {
        return nextSceneName;
    }

    public static void saveMiniGameName(string name)
    {
        miniGameName = name;
    }

    public static string getMiniGameName()
    {
        return miniGameName;
    }

}
