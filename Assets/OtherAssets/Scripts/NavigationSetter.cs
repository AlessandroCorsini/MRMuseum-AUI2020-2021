using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationSetter : MonoBehaviour
{
    public float movementSpeed;
    public float standingTime;
    public float minDistance;
    public GameObject firstWaypoint;
    public GameObject playerObject;
    public int indexPlayerLayer;


    private float step;
    private Vector3 movement;

    private void Start()
    {
        InputMR.setStandingTime(standingTime);
        InputMR.setSpeed(movementSpeed);
        InputMR.setMinDistance(minDistance);
        step = InputMR.getSpeed() * Time.deltaTime;
        SharedInfos.setPlTransform(playerObject.transform);
        SharedInfos.setLastWaypointName("None");
        SharedInfos.setNextWaypointName(firstWaypoint.name);
        SharedInfos.setMovement(new Vector3(firstWaypoint.transform.position.x, playerObject.transform.position.y, firstWaypoint.transform.position.z));
        SharedInfos.setPlayerLayer(indexPlayerLayer);
    }

    private void Update()
    {
        if (Vector3.Distance(SharedInfos.getPlTransform().position, SharedInfos.getMovement()) > minDistance)
        {
            playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, SharedInfos.getMovement(), step);
            InputMR.setMoving();
        }
        else
        {
            InputMR.clearMoving();
        }
    }
}
