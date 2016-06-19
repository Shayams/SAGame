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

    private TimeSpan startTime;
    private Stopwatch watch = new Stopwatch();
    // Use this for initialization
    void Start()
    {
        //new EnemiesCreator(Player, Enemies.First()).WithRandomPosition().Activate();
        startTime = new TimeSpan();
        watch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (watch.ElapsedMilliseconds >= 4000)
        {
            new EnemiesCreator(Player, Enemies.First()).WithRandomPosition().Activate();
            watch.Reset();
        }
    }
}
