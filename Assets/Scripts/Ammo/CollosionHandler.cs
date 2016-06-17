using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class CollosionHandler : MonoBehaviour {

    public enum TagsEnum { Untagged, Respawn, Finish, EditorOnly, MainCamera, Player, GameController, Ammo }
    public TagsEnum[] TagsToIgnoreCollide;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!TagsToIgnoreCollide.Any(item => item.ToString() == col.gameObject.tag))
            Destroy(gameObject);
    }
}