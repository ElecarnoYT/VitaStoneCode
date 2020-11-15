using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // References
    public ThirdPersonController tpController;
    public WeaponManager weaponManager;
    public GameObject gunA;
    public GameObject gunB;

    // Meter
    public float suspicion;
    public float maxSuspicion;

    // Status
    public bool isSearch;
    public bool isPatrol = true;
    public bool viewingPlayer;

    // Trigger
    void OnTriggerEnter (Collider collisionInfo)
    {
        // Set Status
        if (collisionInfo.CompareTag("Player"))
        {
            viewingPlayer = true;
            Debug.Log("In View");

            if (weaponManager.selectedWeapon > 0)
            {
                isSearch = true;
                Debug.Log("Begin Search");
            }
        }
    }

    void OnTriggerExit(Collider collisionInfo)
    {
        viewingPlayer = false;
        Debug.Log("Out of View");
    }

    void Update ()
    {
        // Check for Status
        if (viewingPlayer)
        {
            if (weaponManager.selectedWeapon == 0)
            {
                suspicion += 1 * Time.deltaTime;
                Debug.Log(suspicion);
            }

            if (suspicion >= 100)
            {
                isSearch = true;
                Debug.Log("Begin Search");
            }

            if (weaponManager.selectedWeapon > 0)
            {
                isSearch = true;
                Debug.Log("Begin Search");
            }
        }

        // Apply Status
        if (isPatrol)
        {

        }

        if (isSearch)
        {

        }
    }
}