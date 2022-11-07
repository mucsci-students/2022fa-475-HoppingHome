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

        CopScript[] cops = FindObjectsOfType(typeof(CopScript)) as CopScript[];
        foreach (CopScript cop in cops)
        {
            cop.respawn();
            Destroy(cop.gameObject);
        }

        AsteroidScript[] asteroids = FindObjectsOfType(typeof(AsteroidScript)) as AsteroidScript[];
        foreach (AsteroidScript ass in asteroids)
        {
            ass.respawn();
            Destroy(ass.gameObject);
        }

        BigAsteroidScript[] bigAsteroids = FindObjectsOfType(typeof(BigAsteroidScript)) as BigAsteroidScript[];
        foreach (BigAsteroidScript ass in bigAsteroids)
        {
            ass.respawn();
            Destroy(ass.gameObject);
        }
    }
}
