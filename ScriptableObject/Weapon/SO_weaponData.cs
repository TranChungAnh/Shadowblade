using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="newWeaponData",menuName ="Data/Weapon Data/Weapon")]
public class SO_weaponData : ScriptableObject
{
    public int amountOfAttack { get; protected set; }
    public float[] movementSpeed { get; protected set; }
}
