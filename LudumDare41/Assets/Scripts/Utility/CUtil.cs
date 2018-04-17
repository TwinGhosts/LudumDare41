using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CUtil
{
    private static ColorHolder _colorHolder;

    public static ColorHolder ColorHolder
    {
        get
        {
            if (_colorHolder)
            {
                return _colorHolder;
            }
            else
            {
                return _colorHolder = GameObject.FindObjectOfType<ColorHolder>();
            }
        }
    }
}
