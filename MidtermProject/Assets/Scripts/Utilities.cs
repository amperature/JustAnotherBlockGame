using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public enum GameplayState
    {
        Play,
        Pause
    }

    public static void Shuffle<T>(this List<T> thisList) { //Spirit Drop Shuffle Tuple Method
        int n = thisList.Count;
        while(n-- > 1) {
            int k = Random.Range(0, n);
            (thisList[k], thisList[n]) = (thisList[n], thisList[k]);
        }
    }

}
