using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int selectedWeapon;
    public GameObject modelA;
    public GameObject modelB;

    // Weapon Switching
    void Start ()
    {
        selectWeapon();
    }

    void Update ()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;           
            else
                selectedWeapon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount -1;
            else
                selectedWeapon--;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            selectWeapon();
        }

        // Set Model
        if (selectedWeapon == 0)
        {
            modelA.SetActive(false);
            modelB.SetActive(false);
        }
        if (selectedWeapon == 1)
        {
            modelA.SetActive(true);
            modelB.SetActive(false);
        }
        if (selectedWeapon == 2)
        {
            modelA.SetActive(false);
            modelB.SetActive(true);
        }
    }

    void selectWeapon ()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
