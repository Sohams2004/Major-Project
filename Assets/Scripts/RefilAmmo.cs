using System;
using TMPro;
using UnityEngine;

public class RefilAmmo : MonoBehaviour
{
    private int player;

    private Shoot shoot;
    
    [SerializeField] AudioSource refillAudio;

    private void Start()
    {
        player = LayerMask.NameToLayer("Player");
        shoot = FindObjectOfType<Shoot>();
        refillAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == player)
        {
            Debug.Log("Refilling ammo");
            refillAudio.Play();
            FillAmmo();
        }
    }

    void FillAmmo()
    {
        shoot.currentAmmo = shoot.magazineSize;
        shoot.currentAmmoText.text = shoot.currentAmmo.ToString();
    }
}
