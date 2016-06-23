using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class CollosionHandler : MonoBehaviour {
    public Common.TagsEnum[] TagsToIgnoreCollide;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!TagsToIgnoreCollide.Any(item => item.ToString() == col.gameObject.tag))
            Destroy(gameObject);
    }
}