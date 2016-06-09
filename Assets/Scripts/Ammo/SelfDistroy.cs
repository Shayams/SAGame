using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class SelfDistroy : MonoBehaviour {

    public double TimeToLiveMs = 3000;

    private Stopwatch _ttl = new Stopwatch();
	void Start () {
        _ttl.Start();
	}
	
	// Update is called once per frame
	void Update () {
	    if (_ttl.ElapsedMilliseconds >= TimeToLiveMs)
        {
            Destroy(gameObject);
        }
	}
}
