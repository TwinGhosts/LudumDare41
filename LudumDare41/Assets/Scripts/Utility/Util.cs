using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    private static GameController _gameController;

    public static GameController GameController
    {
        get
        {
            if (_gameController)
            {
                return _gameController;
            }
            else
            {
                return _gameController = GameObject.FindObjectOfType<GameController>();
            }
        }
        set
        {
            _gameController = value;
        }
    }
}
