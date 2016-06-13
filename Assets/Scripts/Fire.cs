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
            transform.rotation = new Quaternion(_ammoRigidBody.velocity.x, _ammoRigidBody.velocity.y, transform.rotation.z, transform.rotation.w);
        }
	}
}
