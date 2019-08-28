﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperManager
{
    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                Debug.Log("Component Found.");
                return tr.GetComponent<T>();
            }
        }
        return null;
    }
}