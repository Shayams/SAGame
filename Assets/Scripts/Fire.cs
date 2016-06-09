using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

    public int Speed;

	void Update () {
        transform.position = new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y, transform.position.z);
	}
}
