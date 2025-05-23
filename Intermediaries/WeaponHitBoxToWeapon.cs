using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitBoxToWeapon : MonoBehaviour
{
    private AggressiveWeapon weapon;
    private void Awake()
    {
        weapon = GetComponentInParent<AggressiveWeapon>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        weapon.AddToDetected(collision);
        Debug.Log("WeaponHitBoxToWeapon: OnTriggerEnter2D");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        weapon.RemoveFromDetected(collision);
    }
}

