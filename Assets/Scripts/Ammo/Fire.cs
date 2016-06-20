using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

    public int Speed { get; set; }

    private Rigidbody2D _ammoRigidBody;

    void Start()
    {
        _ammoRigidBody = GetComponent<Rigidbody2D>();
    }

	void Update () {
        if (_ammoRigidBody != null)
        {
            transform.position = new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y);
        }
	}
}
