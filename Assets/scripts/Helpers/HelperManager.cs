using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class HelperManager
{
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
