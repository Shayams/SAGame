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
    private Stopwatch watch = new Stopwatch();

    void Start()
    {
        watch.Start();
    }

    void Update()
    {
        if (watch.ElapsedMilliseconds >= 6000)
        {
            new EnemiesCreator(Player, Enemies.First())
                .MoveTowardsTarget(Player)
                .WithRandomPosition()
                .Activate();
            watch.Reset();
            watch.Start();
        }
    }
}
