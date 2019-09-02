using UnityEngine;

public class FPS_Visual : MonoBehaviour
{
    bool is_mouse_hide = true;          //Сообщает - скрыт ли указатель мыши

    void Update()
    {
        Listen_Actions();               //Прослушивает и исполняет действия, связанные с некоторым визуалом для FPS

        if (is_mouse_hide)              //Если мышь должна быть скрыта
        {
            Mouse_Hide();               //Скрываем мышь
            FPS_Actions_Enable(true);   //Разрешаем исполнять действия, связанные с геймплеем
        }
        else
        {
            Mouse_Show();               //Показываем мышь
            FPS_Actions_Enable(false);  //Запрещаем исполнять действия, связанные с геймплеем
        }

    }

    /// <summary> Скрывает указатель мыши </summary>
    void Mouse_Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary> Показывает указатель мыши </summary>
    void Mouse_Show()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary> Контролирует визуальную часть в соответствии с вводом </summary>
    void Listen_Actions()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            is_mouse_hide = !is_mouse_hide;
        }
    }

    /// <summary> Разрешает/запрещает выполнять действия игрока </summary>
    /// <param name="is_enable"> Если true - действия игрока будт работать, false - не будут </param>
    void FPS_Actions_Enable(bool is_enable)
    {
        FPS_Actions actions;

        actions = GetComponent<FPS_Actions>();
        actions.enabled = is_enable;
    }

    /// <summary> Показывает/скрывает курсор мыши </summary>
    /// <param name="is_mouse_show"> Если true - курсор виден, false - скрыт </param>
    public void Show_Cursor(bool is_mouse_show)
    {
        this.is_mouse_hide = !is_mouse_show;
    }
}
