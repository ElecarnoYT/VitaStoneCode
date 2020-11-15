using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Gun : MonoBehaviour
{
    // References
    public ThirdPersonController tpController;
    public Camera cam;
    public GameObject impactEffect;
    public VisualEffect muzzleFlash;

    // Parameters
    public float damage;
    public float range;
    public float fireRate;
    public float knockback;
    
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    private float nextTimeToFire = 0f;

    void Start ()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable ()
    {
        isReloading = false;
    }

    // Raycasting
    void Update ()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            if (!tpController.isSprint)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    IEnumerator reload ()
    {
        isReloading = true;
        Debug.Log("Reloading");

        yield return new WaitForSeconds(reloadTime);

        if (!tpController.isSprint)
        {
            currentAmmo = maxAmmo;
            isReloading = false;
            Debug.Log("Reloaded");
        }
        else
        {
            StartCoroutine(reload());
        }       
    }

    void Shoot ()
    {
        currentAmmo--;
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            
            if (target != null)
            {
                target.takeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * knockback);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, new Quaternion(0, 0, 0, 0));
            Destroy(impactGO, 2f);
        }
    }
}