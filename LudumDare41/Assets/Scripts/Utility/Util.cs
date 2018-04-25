using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Util
{
    public static UnitStats PlayerStats
    {
        get; set;
    }

    public static Transform WorldSpaceCanvas
    {
        get; set;
    }

    public static bool MenuSceneActive
    {
        get
        {
            return SceneManager.GetActiveScene().name == "Main Menu" || SceneManager.GetActiveScene().name == "Difficulty Select" || SceneManager.GetActiveScene().name == "Class Select" || SceneManager.GetActiveScene().name == "Credits";
        }
    }

    public static Player Player
    {
        get; set;
    }

    public static GameController GameController
    {
        get; set;
    }

    public static TransitionSwipe TransitionSwipe
    {
        get; set;
    }

    public static TransitionFade TransitionFade
    {
        get; set;
    }

    public static UIManager UIManager
    {
        get; set;
    }

    public static StageGenerator StageGenerator
    {
        get; set;
    }

    public static MusicManager MusicManager
    {
        get; set;
    }

    public static Vector2 VectorFromAngle(float dir)
    {
        return new Vector2(Mathf.Cos(Mathf.Deg2Rad * dir), Mathf.Sin(Mathf.Deg2Rad * dir));
    }

    public static float AngleFromVector(Vector2 dir)
    {
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
