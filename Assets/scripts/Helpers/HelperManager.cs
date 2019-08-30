using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class HelperManager
{
    public static string atomBasePath = "Prefabs/atoms/";
    public static Dictionary<Color, string> tileResourcePaths = new Dictionary<Color, string>()
    {
        {Color.BLUE, atomBasePath + "Atom_blue" },
        {Color.GREEN, atomBasePath + "Atom_green" },
        {Color.PURPLE, atomBasePath + "Atom_purple" },
        {Color.RED, atomBasePath + "Atom_red" },
    };

    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.GetComponent<T>();
            }
        }
        return null;
    }
    public static GameObject getParentGameObject(this GameObject child)
    {
        return child.transform.parent.gameObject;
    }
}
