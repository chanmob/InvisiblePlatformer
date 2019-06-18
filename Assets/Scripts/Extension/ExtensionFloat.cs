using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionFloat
{
    private static readonly float tileSize = 0.4f;

    public static float GetTileSize(this int _size)
    {
        float returnValue = 0;
        returnValue = _size * tileSize;

        return returnValue;
    }
}
