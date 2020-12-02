using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counting : MonoBehaviour
{
    public static int rockCounter = 1;
    public static int max = 6;

    public static void updateCounter()
    {
        rockCounter += 1;
    }
}
