using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySaver : MonoBehaviour
{
    public void SetDifficulty(int index)
    {
        GameController.Difficulty = (Difficulty)index;
    }
}

public enum Difficulty
{
    EASY,
    NORMAL,
    HARD,
}
