using System.Collections;
using System.Collections.Generic;

using UnityEngine;

//Has a list of all players in the game
//Has name of main player and its state (current animation)
public static class globalData
{
    public static string [] gameUserList;
    public static int State;
    public static string userName;
    public static bool hostUser = false;
    public static bool GamePaused = false;
    public static int ability;
    public static int level = -1;
    public static float Score = 1000;
}
