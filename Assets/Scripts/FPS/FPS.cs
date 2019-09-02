using UnityEngine;

public class FPS : MonoBehaviour
{
    public new Camera camera;   //Камера игрока

    /// <summary> Если camera == null, пытается найти камеру </summary>
    protected void Find_Camera()
    {
        if (camera == null)                             //Если камеры нет
        {
            camera = GetComponentInChildren<Camera>();  //Находим камеру в детях
            if (camera == null)                         //Если камеры всё ещё нет
            {
                camera = Camera.main;                   //Находим основную камеру
                if (camera == null)                     //Если основной камеры тоже нет
                {
                    Debug.LogError("Назначьте камеру"); //Выводим сообщение об ошибке
                }
            }
        }
    }

}
