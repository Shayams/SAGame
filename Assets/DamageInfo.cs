using UnityEngine;
using System.Collections;
using System;

public interface IDamageInfo
{
    int DamageHitPoints { get; }
}

public class DamageInfo : MonoBehaviour, IDamageInfo
{
    public int HitPoints;

    public int DamageHitPoints { get { return HitPoints; } }
}
