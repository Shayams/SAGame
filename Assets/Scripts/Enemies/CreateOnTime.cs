using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Diagnostics;
using Assets;

public class CreateOnTime : MonoBehaviour {

    public List<GameObject> Enemies;
    public GameObject Player;
    public Vector2 StartPosition;
    public long MsBetweenCreation = 6000;

    private Stopwatch watch = new Stopwatch();

    void Start()
    {
        watch.Start();
    }

    void Update()
    {
        if (watch.ElapsedMilliseconds >= MsBetweenCreation)
        {
            new EnemiesCreator(Player, Enemies.First())
                .MoveTowardsTarget(Player)
                .SetPosition(StartPosition)
                .Activate();
            watch.Reset();
            watch.Start();
        }
    }
}
