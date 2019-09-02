using UnityEngine;

public class FPS_Look : FPS
{
    public Vector2 offset_vs;   //Угол поворота в углах Эйлера
    public GameObject head;     //Голова, внутри которой находится камера

    void Start()
    {
        Find_Camera();
        offset_vs = Get_Euler(camera.transform.rotation);
    }

    void Update()
    {
        Rotate();
    }

    /// <summary> Возвращает стартовый угол поворота камеры в углах Эйлера </summary>
    /// <param name="rotation"> Поворот камеры в виде кватерниона </param>
    static Vector2 Get_Euler(Quaternion rotation)
    {
        Vector2 offset;
        Vector3 euler;

        euler = rotation.eulerAngles;

        offset.x = euler.y;
        offset.y = euler.x;

        return offset;
    }

    /// <summary> Поворачивает голову в ту сторону, куда двинулась мышка </summary>
    void Rotate()
    {
        Quaternion angle, a_la, a_lo;

        offset_vs.y -= Input.GetAxis("Mouse Y");    //Меняем поворот по широте
        offset_vs.x += Input.GetAxis("Mouse X");    //Меняем поворот по долготе

        a_la = Quaternion.Euler(offset_vs.y, 0, 0); //Находим кватернион поворота широты
        a_lo = Quaternion.Euler(0, offset_vs.x, 0); //Находим кватернион поворота долготы

        angle = a_lo * a_la;                        //Находим результирующий угол поворота

        head.transform.rotation = angle;            //Поворачиваем голову на нужный угол
    }

}
