using UnityEngine;
using UnityEngine.UI;

public class Change_Speed : MonoBehaviour
{
    public Slider slider;       //Преставление скорости в виде интерактивного слайдера
    public FPS_Move move;       //Скрипт перемещения, в котором хранится значение скорости
    public Text text;           //Численное представление скорости
    public Data_SL data;        //Данные для сохранения

    private void Start()
    {
        Set_Start_Value();
    }

    void Update()
    {
        if(data.options == null)
        {
            Debug.LogAssertion("Данные не загружены");
            data.options = new Options_Data();
        }
        else
        {
            data.options.speed = 10 * slider.value;     //Обновляем данные для записи
            move.speed_mgn = data.options.speed;        //Меняем скорость перемещения
            text.text = data.options.speed.ToString();  //Отображаем численное значение новой скорости
        }
    }

    /// <summary> Устанавливает слайдер в положение, соответствующее значению скорости </summary>
    void Set_Start_Value()
    {
        if (data.options == null)
        {
            data.options = new Options_Data();
        }
        else
        {
            slider.value = data.options.speed / 10;
        }
    }
}
