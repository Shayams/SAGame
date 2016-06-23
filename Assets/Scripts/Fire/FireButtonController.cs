using UnityEngine;
using System;
using Assets.Scripts;

public class FireButtonController : ContollerBaseMonoBehavior
{
    public double MinTimeBetweenContinuesFire = 2;
    public bool UseFireButton;
    public bool UseForceToFire;
    public int FireSpeed = 800;

    private DateTime _lastFire = DateTime.MinValue;
    private Action _fire;

    
    override protected void Start()
    {
        base.Start();

        FireBase fireBase = UseForceToFire ? gameObject.GetOrAddComponent<FireWithForce>() : (FireBase)gameObject.GetOrAddComponent<FireWithoutForce>();
        fireBase.Player = Player;
        fireBase.Init();

        _fire = () =>
        {
            if (DateTime.Now.Subtract(_lastFire).TotalMilliseconds > MinTimeBetweenContinuesFire)
            {
                _lastFire = DateTime.Now;
                fireBase.Fire(FireSpeed);
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
        if (!UseFireButton && Input.GetMouseButtonDown(0) && Input.mousePosition.y > 65) _fire();
    }
}
