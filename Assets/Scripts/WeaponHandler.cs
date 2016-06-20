using UnityEngine;
using System.Collections;
using System;

public interface IWeaponHandler
{
    GameObject GetAmmo();
}

public class WeaponHandler : MonoBehaviour, IWeaponHandler
{
    public GameObject CurrentAmmo;

    public GameObject GetAmmo()
    {
        return CurrentAmmo.gameObject;
    }
}
