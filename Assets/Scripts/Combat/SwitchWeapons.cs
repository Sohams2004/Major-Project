using System;
using UnityEngine;
using UnityEngine.UI;

public class SwitchWeapons : MonoBehaviour
{
    [SerializeField] MeleeAttack meleeAttack;
    [SerializeField] Shoot shoot;
    
    [SerializeField] private Image ammoUI;


    private void Start()
    {
        meleeAttack = GetComponent<MeleeAttack>();
        shoot = GetComponent<Shoot>();
        ammoUI = GameObject.Find("Gun").GetComponent<Image>();
        
        shoot.enabled = false;
        ammoUI.color = new Color(0,0,0,0.3f);
    }

    void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            meleeAttack.enabled = true;
            shoot.enabled = false;
            ammoUI.color = new Color(0,0,0,0.3f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            meleeAttack.enabled = false;
            shoot.enabled = true;
            ammoUI.color = new Color(0,0,0,1f);
        }
    }

    private void Update()
    {
        SwitchWeapon();
    }
}
