using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class FireBase : ContollerBaseMonoBehavior
{
    protected IWeaponHandler _weaponHandler;

    override public void Init()
    {
        base.Init();
        if (Player == null) return;
        _weaponHandler = Player.GetComponent<IWeaponHandler>();
    }

    protected GameObject CreateAmmoObject()
    {
        var rotationExp = (int)Player.transform.rotation.y == 0 ? 1 : -1;

        var exitPosition = new Vector3(Player.transform.position.x + ((_playerCollider.bounds.size.x /2 ) * rotationExp) , Player.transform.position.y, Player.transform.position.z);

        var ammo = Instantiate(_weaponHandler.GetAmmo(), exitPosition, new Quaternion(Player.transform.rotation.x, Player.transform.rotation.y, 0, 0)) as GameObject;
        ammo.transform.SetParent(Player.transform.parent);

        return ammo;
    }

    protected void UpdateFireSpeed(Fire component, int speed)
    {
        if (component != null) component.Speed = speed;
    }

    public abstract void Fire(int FireSpeed);
}
