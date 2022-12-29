using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerReferences 
{
    [SerializeField]
    private GameObject[] weaponObjects;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private Transform gunBarrel;


    public GameObject[] WeaponObjects { get => weaponObjects; set => weaponObjects = value;}

    public GameObject ProjectilePrefab { get => projectilePrefab; private set => projectilePrefab = value; }

    public Transform GunBarrel { get => gunBarrel; private set => gunBarrel = value; }
}
