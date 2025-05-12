using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenericNotImplementError <T>
{
    public static T TryGet(T value,string name)
    {
        if (value != null)
        {
            return value;
        }
        Debug.Log(typeof(T) + "not implement on" + name);
        return default;

    }
}
