using UnityEngine;
using System;
using Assets.Scripts;

public class FireButtonController : ContollerBaseMonoBehavior
{
    public double MinTimeBetweenContinuesFire = 2;
    public bool UseFireButton;
    public bool UseForceToFire;
    public int FireSpeed = 800;

    private IWeaponHandler _weaponHandler;
    private DateTime _lastFire = DateTime.MinValue;
    private Action _fire;

    
    override protected void Start()
    {
        base.Start();
        _weaponHandler = Player.GetComponent<IWeaponHandler>();

        _fire = () =>
        {
            if (DateTime.Now.Subtract(_lastFire).TotalMilliseconds > MinTimeBetweenContinuesFire)
            {
                _lastFire = DateTime.Now;
                if (UseForceToFire) FireWithForce();
                else FireWithoutForce();
            }
        };
        
    }

    public void OnFireButtonClick()
    {
        if (UseFireButton)
            _fire();
    }

    void Update()
    {
        if (!UseFireButton && Input.GetMouseButtonDown(0)) _fire();
    }

    private void FireWithoutForce()
    {
        var ammo = CreateAmmoObject();
        UpdateFireSpeed(ammo.GetComponent<Fire>(), FireSpeed);
    }

    private void FireWithForce()
    {
        Debug.Log(Input.mousePosition);
        var ammo = CreateAmmoObject();
        UpdateFireSpeed(ammo.GetComponent<Fire>(), 0);
        var ammoRigidBody = ammo.GetComponent<Rigidbody2D>();

        if (ammoRigidBody != null)
        {
            ammoRigidBody.AddForce(new Vector2(Mathf.Abs(Player.transform.position.x - Input.mousePosition.x), Mathf.Abs(Player.transform.position.y - Input.mousePosition.y)) * FireSpeed)  ;
        }
    }

    private void UpdateFireSpeed(Fire component, int speed)
    {
        if (component != null) component.Speed = speed;
    }

    private GameObject CreateAmmoObject()
    {
        var exitPosition = new Vector3(Player.transform.position.x + _playerCollider.bounds.size.x + 20, Player.transform.position.y, Player.transform.position.z);

        var ammo = Instantiate(_weaponHandler.GetAmmo(), exitPosition, new Quaternion(Player.transform.rotation.x, Player.transform.rotation.y, 0, 0)) as GameObject;
        ammo.transform.SetParent(Player.transform.parent);

        return ammo;
    }
}
