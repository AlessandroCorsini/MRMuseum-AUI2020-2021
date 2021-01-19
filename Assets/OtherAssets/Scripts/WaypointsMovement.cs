using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsMovement : MonoBehaviour
{
    public GameObject wayPointNorth;
    public GameObject wayPointSouth;
    public GameObject wayPointWest;
    public GameObject wayPointEast;
    // public string[] starredDirections;

    public bool starNorth;
    public bool starSouth;
    public bool starWest;
    public bool starEast;

    private bool unlocked = false;
    private int mrInputDirection;
    private Vector3 nullVector;
    private bool[] starredDirections = new bool[4];

    public string nextSceneName;

    private void Start()
    {
        nullVector = new Vector3(-1000f, -1000f, -1000f);
        starredDirections[0] = starNorth;
        starredDirections[1] = starSouth;
        starredDirections[2] = starWest;
        starredDirections[3] = starEast;
    }

    private void Update()
    {
        // unlocked is set true by the trigger
        // isMoving is set false by Update when the player is arrived near to the next waypoint
        if (unlocked && !InputMR.isMoving())
        {
            if (InputMR.isEventHappened())
            {
                mrInputDirection = InputMR.getDirection();

                if (mrInputDirection == -1)
                {
                    Debug.LogError("There was an error in retrieving the direction");
                    return;
                }
            }
            else
            {
                // in this frame there is no input from the user
                return;
            }

            Vector3 movement = wayPointsRedirect(mrInputDirection);

            if (Vector3.Distance(movement, nullVector) > 0.5f)
            {
                InputMR.setMoving();
                SharedInfos.setMovement(movement);
            } else
            {
                // tried to move in a direction in which was not possible to move
            }

        }

    }

    // handles the right activation of triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != SharedInfos.getPlayerLayer())
        {
            Debug.Log(this.name + ":: not colliding with " + SharedInfos.getPlayerLayer() + " but with " + other.gameObject.layer);
            return;
        }

        Debug.Log(this.name + ":: ontrigger");

        //checking if this is the last waypoint, in that case returning
        if (SharedInfos.getLastWaypointName().Equals(this.name) && !SharedInfos.getLastWaypointName().Equals("None"))
        {
            Debug.Log(this.name + ":: Triegger failed because it was the last trigger");
            return;
        }

        // checking if it is not the next designed waypoint, in that case returning
        Debug.Log(this.name + ":: next WP is " + SharedInfos.getNextWaypointName());
        if (!SharedInfos.getNextWaypointName().Equals(this.name))
        {
            Debug.Log(this.name + ":: Trigger failed because it is not the next trigger");
            return;
        }

        // now the trigger can activate itself

        Debug.Log(this.name + ":: Trigger activated");

        // if the player triggered this collider and the last waypoint was different from this, he can use this waypoint to move
        
        unlocked = true;
        ArrowManager.decodeDisabledDirections(getNotNullDirections());
        ArrowManager.decodeStarredDirections(starredDirections);
        ArrowManager.saveNextSceneName(nextSceneName);
        InputMR.setNewWaypoint();
        InputMR.clearEventHappened();

        // now this waypoint is listening to the arrows in order to set the movement performed by NavigationSetter 
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(this.name + ":: Exit from trigger");
        unlocked = false;
    }

    // taked in input a direction and outputs the possible movement
    private Vector3 wayPointsRedirect(int dir)
    {

        if (dir == 0)
        {
            // trying to move south
            if (wayPointSouth != null)
                return moveToWaypoint(wayPointSouth);
            else
                return nullVector;

        }
        else if (dir == 1)
        {
            // trying to move east
            if (wayPointEast != null)
                return moveToWaypoint(wayPointEast);
            else
                return nullVector;

        }
        else if (dir == 2)
        {
            // trying to move up
            if (wayPointNorth != null)
                return moveToWaypoint(wayPointNorth);
            else
                return nullVector;

        }
        else if (dir == 3)
        {
            // trying to move left
            if (wayPointWest != null)
                return moveToWaypoint(wayPointWest);
            else
                return nullVector; 

        }
        else
        {
            Debug.LogError(this.name + ":: There was an error in retrieving the waypoints direction internally");
            return nullVector;
        }
    }

    private Vector3 moveToWaypoint(GameObject waypoint)
    {
        if (waypoint)
        {
            // float heightDiff = Mathf.Abs(waypoint.transform.position.y - SharedInfos.getPlTransform().position.y);
            // Vector3 positionMove = new Vector3(waypoint.transform.position.x, waypoint.transform.position.y, waypoint.transform.position.z);
            Vector3 positionMove = new Vector3(waypoint.transform.position.x, SharedInfos.getPlTransform().position.y, waypoint.transform.position.z);
            SharedInfos.setNextWaypointName(waypoint.name);
            SharedInfos.setLastWaypointName(this.name);

            return positionMove;
        }

        // here the user insert an input that should not be abled

        Debug.Log(this.name + ":: User can not move in that direction " + waypoint);
        return nullVector;
    }

    private string[] getNotNullDirections()
    {
        int i = 0;
        List<string> directionsList = new List<string>();
        if(wayPointNorth != null)
        {
            directionsList.Add("wayPointNorth");
        }
        if (wayPointWest != null)
        {
            directionsList.Add("wayPointWest");
        }
        if (wayPointEast != null)
        {
            directionsList.Add("wayPointEast");
        }
        if (wayPointSouth != null)
        {
            directionsList.Add("wayPointSouth");
            i++;
        }

        return directionsList.ToArray();
    }
   
}
