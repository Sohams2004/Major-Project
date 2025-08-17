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

    [SerializeField] private AudioSource meleeEquipAudio;
    [SerializeField] private AudioSource gunEquipAudio;

    private void Start()
    {
        meleeAttack = GetComponent<MeleeAttack>();
        shoot = GetComponent<Shoot>();
        
        meleeWeaponPrefab = GameObject.Find("MeleeWeaponPrefab");

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            ammoUI = GameObject.Find("Gun").GetComponent<Image>();
            meleeUI = GameObject.Find("Melee").GetComponent<Image>();
            
            ammoUI.color = new Color(0,0,0,0.3f);
            meleeUI.color = new Color(0,0,0,1f);
        }
        
        shoot.enabled = false;
        
        meleeEquipAudio = GameObject.Find("Melee equip audio").GetComponent<AudioSource>();
        gunEquipAudio = GameObject.Find("gun equip audio").GetComponent<AudioSource>();
    }

    void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("sword equiped");
            meleeAttack.enabled = true;
            meleeWeaponPrefab.SetActive(true);
            shoot.enabled = false;
            ammoUI.color = new Color(0,0,0,0.3f);
            meleeUI.color = new Color(0,0,0,1f);
            meleeEquipAudio.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            meleeAttack.enabled = false;
            meleeWeaponPrefab.SetActive(false);
            shoot.enabled = true;
            ammoUI.color = new Color(0,0,0,1f);
            meleeUI.color = new Color(0,0,0,0.3f);
            gunEquipAudio.Play();
        }
    }

    private void Update()
    {
        SwitchWeapon();
    }
}
