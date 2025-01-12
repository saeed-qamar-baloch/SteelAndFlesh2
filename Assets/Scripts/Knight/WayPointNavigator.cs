using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [Header("AI Character")]
    public CharacterNavigatorScript character;
    public WayPoint currentWaypoint;
    int direction;

    private void Awake()
    {
        character = GetComponent<CharacterNavigatorScript>();
    }

    private void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));

        // Ensure currentWaypoint is not null before accessing its method
        if (currentWaypoint != null)
        {
            character.LocateDestination(currentWaypoint.GetPosition());
        }
        else
        {
            Debug.LogError("currentWaypoint is not assigned!");
        }
    }

    private void Update()
    {
        if (character.destinationReached)
        {
            if (direction == 0)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }
            else if (direction == 1)
            {
                currentWaypoint = currentWaypoint.previousWaypoint;
            }

            character.LocateDestination(currentWaypoint.GetPosition());
        }
    }
}
