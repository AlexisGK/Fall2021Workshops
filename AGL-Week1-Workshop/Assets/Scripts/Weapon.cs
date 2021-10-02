using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject shotPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Shoot();
        }   
    }

    void Shoot()
    {
        Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
    }
}
