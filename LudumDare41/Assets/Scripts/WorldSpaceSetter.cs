using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceSetter : MonoBehaviour
{
    public void Awake()
    {
        Util.WorldSpaceCanvas = transform;
    }
}
