using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FireWithForce : FireBase
{
    public override void Fire(int fireSpeed)
    {
        var ammo = CreateAmmoObject();
        UpdateFireSpeed(ammo.GetComponent<Fire>(), 0);
        var ammoRigidBody = ammo.GetComponent<Rigidbody2D>();

        if (ammoRigidBody != null)
        {
            ammoRigidBody.AddForce(new Vector2(Mathf.Abs(Player.transform.position.x - Input.mousePosition.x), Mathf.Abs(Player.transform.position.y - Input.mousePosition.y)) * fireSpeed);
        }
    }
}
