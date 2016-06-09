using UnityEngine;
using System.Collections;

public class CollosionHandler : MonoBehaviour {

    private BoxCollider2D _boxCollider;

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }


	void Start () {
        _boxCollider = GetComponent<BoxCollider2D>();
    }
}
