using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configs : MonoBehaviour
{
    // On/Off toggle for sound effect
    public static bool SFX
    {
        get;
        set;

    }= true;

    // Music volume
    public static float MusicVol
    {
        set;
        get;
    } = 1;

    
}
