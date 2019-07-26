using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class TransformExtension
{
    public static void SetPosition(this Transform _transform, Vector3 _pos)
    {
        _transform.position = _pos;
    }

    public static T[] GetcomponentsRealChildren<T>(this Transform _transform, bool _checkOff = false) where T : Component
    {
        var component = _transform.GetComponentsInChildren<T>(_checkOff);
        T[] result = new T[component.Length - 1];

        for (int i = 1; i < component.Length; i++)
        {
            result[i - 1] = component[i];
        }

        return result;
    }

    public static GameObject FindInChildren(this Transform go, string name)
    {
        return (from x in go.GetComponentsInChildren<Transform>(true)
                where x.gameObject.name == name
                select x.gameObject).First();
    }
}
