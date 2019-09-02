using UnityEngine;

public class Combat_Unit : Destructable_Object
{
    [Range (0, int.MaxValue)]
    public int score;           //Количество очков, полученных юнитом во время игры

}
