using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade_Launcher : Gun
{
    void Start()
    {
        player_camera = Camera.main;
        if (player_camera == null)
        {
            Debug.LogError("Укажите ссылку на камеру игрока для " + name);
        }
    }

    void Update()
    {
        Cooling_Down();
    }



}
