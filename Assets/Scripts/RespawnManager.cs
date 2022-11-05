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

        CarScript[] cars = FindObjectsOfType(typeof(CarScript)) as CarScript[];
        foreach (CarScript car in cars)
        {
            car.respawn();
            Destroy(car.gameObject);
        }

    }
}
