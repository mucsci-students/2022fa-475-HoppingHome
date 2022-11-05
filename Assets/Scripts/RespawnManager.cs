using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public void respawn()
    {
        DroneScript[] drones = FindObjectsOfType(typeof(DroneScript)) as DroneScript[];
        foreach (DroneScript drone in drones)
        {
            drone.respawn();
            Destroy(drone.gameObject);
        }
    }
}
