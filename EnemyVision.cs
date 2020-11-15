using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVision : MonoBehaviour
{
    public float fovAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;
    public GameObject player;
    public NavMeshAgent nav;
    public SphereCollider col;
    //public EnemyMovement controller;

    //private LastPlayerSighting lastPlayerSighting;
    //private Animator animator;
    private Vector3 previousSighting;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Object in trigger = " + other.gameObject);

            Vector3 direction = transform.position - player.transform.position;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, -direction, out hit, 1000f))
            {
                Debug.DrawRay(transform.position, -direction, Color.green, 0.5f);
                Debug.Log("Raycast hit: " + hit.collider.gameObject);

                if (hit.collider.gameObject == player)
                {
                    playerInSight = true;
                    //personalLastSighting = player.transform.position;
                    nav.SetDestination(player.transform.position);
                    Debug.Log(nav.destination);
                }
            }
        }
    }

    void OnTriggerExit()
    {
        playerInSight = false;
        Debug.Log("Player out of Trigger");
    }
}
