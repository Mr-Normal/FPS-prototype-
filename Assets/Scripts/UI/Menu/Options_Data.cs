using System;
using UnityEngine;

/// <summary> Информация о настройках (можно сериализовывать) </summary>
[Serializable]
public class Options_Data
{
    [Tooltip("Скорость перемещения (метров/секунда)")] public float speed;
    public Options_Data()
    {
        speed = 5;
    }
}
