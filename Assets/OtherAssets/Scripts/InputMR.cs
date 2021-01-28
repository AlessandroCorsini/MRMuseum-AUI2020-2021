using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputMR
{
    static private bool eventHappened = false;
    static private bool newWaypoint = false;
    static private string direction = "None";
    static private bool moving;

    static private float speed;
    static private float standingTime;
    static private float minDistance;

    static private int arrowCounter = 0;
  
    static public bool isEventHappened()
    {
        return eventHappened;
    }

    static public bool isNewWaypointHappened()
    {
        return newWaypoint;
    }

    static public void setEventHappened(string arrowDirection)
    {
        direction = arrowDirection;
        eventHappened = true;
    }

    static public void setNewWaypoint()
    {
        newWaypoint = true;
    }

    static public int getDirection()
    {
        if (direction.Equals("None"))
        {
            Debug.LogError(":: direction is None");
            return -1;
        } else if(direction.Equals("South"))
        {
            Debug.Log("Requested a South position");
            return 0;
        } else if(direction.Equals("East"))
        {
            Debug.Log("Requested a East direction");
            return 1;
        } else if (direction.Equals("North"))
        {
            Debug.Log("Requested a North direction");
            return 2;
        } else if (direction.Equals("West"))
        {
            Debug.Log("Reuqested a West direction");
            return 3;
        }

        Debug.LogError("Direction not coherent");

        return -1;
    }

    static public void clearEventHappened()
    {
        direction = "None";
        eventHappened = false;
    }

    static public void clearNewWaypoint()
    {
        // called by each arrow
        if(arrowCounter <= 3)
        {
            arrowCounter++;
        } else
        {
            arrowCounter = 0;
            newWaypoint = false;
        }
    }

    static public float getStandingTime()
    {
        return standingTime;
    }

    static public void setStandingTime(float time)
    {
        standingTime = time;
    }

    static public float getSpeed()
    {
        return speed;
    }

    static public void setSpeed(float speedIntensity)
    {
        speed = speedIntensity;
    }

    static public bool isMoving()
    {
        return moving;
    }

    static public void setMoving()
    {
        moving = true;
    }

    static public void clearMoving()
    {
        moving = false;
    }

    public static float getMinDistance()
    {
        return minDistance;
    }

    static public void setMinDistance(float minDist)
    {
        minDistance = minDist;
    }

}
