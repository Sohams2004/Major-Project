using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchWeapons : MonoBehaviour
{
    [SerializeField] MeleeAttack meleeAttack;
    [SerializeField] Shoot shoot;
    
    [SerializeField] private Image ammoUI;
    [SerializeField] private Image meleeUI;
    
    [SerializeField] GameObject meleeWeaponPrefab;

    private void Start()
    {
        meleeAttack = GetComponent<MeleeAttack>();
        shoot = GetComponent<Shoot>();
        
        meleeWeaponPrefab = GameObject.Find("MeleeWeaponPrefab");

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            ammoUI = GameObject.Find("Gun").GetComponent<Image>();
            meleeUI = GameObject.Find("Melee").GetComponent<Image>();
            
            ammoUI.color = new Color(0,0,0,0.3f);
            meleeUI.color = new Color(0,0,0,1f);
        }
        
        shoot.enabled = false;
    }

    void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            meleeAttack.enabled = true;
            meleeWeaponPrefab.SetActive(true);
            shoot.enabled = false;
            ammoUI.color = new Color(0,0,0,0.3f);
            meleeUI.color = new Color(0,0,0,1f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            meleeAttack.enabled = false;
            meleeWeaponPrefab.SetActive(false);
            shoot.enabled = true;
            ammoUI.color = new Color(0,0,0,1f);
            meleeUI.color = new Color(0,0,0,0.3f);
        }
    }

    private void Update()
    {
        SwitchWeapon();
    }
}
