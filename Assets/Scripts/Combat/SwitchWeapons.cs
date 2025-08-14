using System;
using UnityEngine;

public class SwitchWeapons : MonoBehaviour
{
    [SerializeField] MeleeAttack meleeAttack;
    [SerializeField] Shoot shoot;

    private void Start()
    {
        meleeAttack = GetComponent<MeleeAttack>();
        shoot = GetComponent<Shoot>();
        
        shoot.enabled = false;
    }

    void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            meleeAttack.enabled = true;
            shoot.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            meleeAttack.enabled = false;
            shoot.enabled = true;
        }
    }

    private void Update()
    {
        SwitchWeapon();
    }
}
