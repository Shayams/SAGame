using UnityEngine;
using System.Collections;

public static class ExtentionMethods {
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        var component = gameObject.GetComponent<T>();

        if (component == null) component = gameObject.AddComponent<T>();

        return component;
    }
}

public static class Common
{
    public enum TagsEnum { Untagged, Respawn, Finish, EditorOnly, MainCamera, Player, GameController, Ammo, Enemy }
}
