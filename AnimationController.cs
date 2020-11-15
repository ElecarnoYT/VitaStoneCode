using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public ThirdPersonController tpController;
    public GraphicsFollow graphicsFollow;
    public WeaponManager weaponManager;
    public Animator animator;

    void Update()
    {
        // Check for Gun
        if (weaponManager.selectedWeapon > 0)
        {
            animator.SetInteger("Gun", 1);
        }
        else
        {
            animator.SetInteger("Gun", 0);
        }

        // Idle
        if (!tpController.isMove)
        {
            animator.SetInteger("Idle", 1);
            animator.SetInteger("Run", 0);
            animator.SetInteger("Walk", 0);
            animator.SetInteger("Crouch", 0);
        }

        // Walking
        if (tpController.isMove)
        {
            animator.SetInteger("Walk", 1);
            animator.SetInteger("Idle", 0);
            animator.SetInteger("Run", 0);
            animator.SetInteger("Crouch", 0);
        }

        // Sprint
        if (tpController.isSprint && tpController.isMove)
        {
            animator.SetInteger("Run", 1);
            animator.SetInteger("Walk", 0);
            animator.SetInteger("Idle", 0);
            animator.SetInteger("Crouch", 0);
        }

        // Crouching
        if (tpController.isCrouch)
        {
            animator.SetInteger("Run", 0);
            animator.SetInteger("Walk", 0);
            animator.SetInteger("Idle", 0);
            animator.SetInteger("Crouch", 1);

            if (!tpController.isMove)
            {
                animator.SetInteger("Run", 0);
                animator.SetInteger("Walk", 0);
                animator.SetInteger("Idle", 2);
                animator.SetInteger("Crouch", 0);
            }
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetInteger("Jump", 1);
        }
        if (Input.GetButtonUp("Jump"))
        {
            animator.SetInteger("Jump", 0);
        }

        // Slide

        if (tpController.isSprint)
        {
            if (Input.GetKeyDown("left ctrl"))
            {
                animator.SetInteger("Slide", 1);
                Invoke("ResetSlide", 0.1f);
            }
        }
    }

    void ResetSlide ()
    {
        animator.SetInteger("Slide", 0);
    }

}