using UnityEngine;
using UnityEngine.UI;

/// <summary> Отображает в указанном UI тексте количество очков указанного юнита </summary>
public class Show_Value : MonoBehaviour
{
    public Combat_Unit unit;
    public Text text;

    void Start()
    {
        text = GetComponentInChildren<Text>();  //Если ссылки на текст нет, то пытаемся найти его самостоятельно
    }

    void Update()
    {
        text.text = unit.score.ToString();      //Обновляем значение 
    }

}
