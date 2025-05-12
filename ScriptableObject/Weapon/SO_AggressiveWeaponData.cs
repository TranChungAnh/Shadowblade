using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="newAggressiveWeapon",menuName = "Data/Weapon Data/Aggressive Weapon")]
public class SO_AggressiveWeaponData : SO_weaponData
{
    [SerializeField] public WeaponAttackDetails[] weaponAttackDetails;

    public WeaponAttackDetails[] AttackDetails { get => weaponAttackDetails;private  set => weaponAttackDetails = value; }
    private void OnEnable()
    {
        amountOfAttack = weaponAttackDetails.Length;
        movementSpeed = new float[amountOfAttack];
        for(int i = 0; i < amountOfAttack; i++)
        {
            movementSpeed[i] = weaponAttackDetails[i].movementSpeed;
        }
    }
}
