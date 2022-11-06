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

        SpeedBoostScript[] sbUps = FindObjectsOfType(typeof(SpeedBoostScript)) as SpeedBoostScript[];
        foreach (SpeedBoostScript sb in sbUps)
        {
            sb.respawn();
        }

        FireRateBuffScript[] frUps = FindObjectsOfType(typeof(FireRateBuffScript)) as FireRateBuffScript[];
        foreach (FireRateBuffScript fr in frUps)
        {
            fr.respawn();
        }

        HealthPickUpScript[] healthPU = FindObjectsOfType(typeof(HealthPickUpScript)) as HealthPickUpScript[];
        foreach (HealthPickUpScript heart in healthPU)
        {
            heart.respawn();
        }
    }
}
