using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FireWithoutForce : FireBase
{
    public override void Fire(int fireSpeed)
    {
        var ammo = CreateAmmoObject();
        UpdateFireSpeed(ammo.GetComponent<Fire>(), fireSpeed);
    }
}
