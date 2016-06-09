using UnityEngine;
using System;
using Assets.Scripts;

public class FireButtonController : ContollerBaseMonoBehavior
{
    public double MinTimeBetweenContinuesFire = 2;

    private IWeaponHandler _weaponHandler;
    private DateTime _lastFire = DateTime.MinValue;
    
    override protected void Start()
    {
        base.Start();
        _weaponHandler = Player.GetComponent<IWeaponHandler>();
    }

    public void OnFireButtonClick()
    {
        if (DateTime.Now.Subtract(_lastFire).TotalMilliseconds > MinTimeBetweenContinuesFire)
        {
            _lastFire = DateTime.Now;
            var position = new Vector3(Player.transform.position.x + _playerCollider.bounds.size.x + 20, Player.transform.position.y, Player.transform.position.z);

            var ammo = Instantiate(_weaponHandler.GetAmmo(), position, new Quaternion(Player.transform.rotation.x, Player.transform.rotation.y, 0, 0)) as GameObject;
            ammo.transform.SetParent(Player.transform.parent);
        }
    }
}
