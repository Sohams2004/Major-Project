using System;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject meleeWeaponPrefab;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] private float attackTimer;
    [SerializeField] bool isAttacking = false;

    private void Start()
    {
        meleeWeaponPrefab.SetActive(false);
    }

    private void Update()
    {
        MeleeTimer();
        if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            attackTimer = attackCooldown;
            Attack();
        }
    }

    void Attack()
    {
        if (isAttacking)
        {
            meleeWeaponPrefab.SetActive(true);
        }
    }
    
    void MeleeTimer()
    {
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                isAttacking = false;
                meleeWeaponPrefab.SetActive(false);
                attackTimer = 0f; 
            }
        }
    }
}
