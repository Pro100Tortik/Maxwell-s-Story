using UnityEngine;

public class GameSettings
{
    [Range(0.1f, 10.0f)] public float sensitivity = 1.5f;
    [Range(-80.0f, 10.0f)] public float musicVolume = 1.0f;
    [Range(-80.0f, 10.0f)] public float SFXVolume = 1.0f;
}
