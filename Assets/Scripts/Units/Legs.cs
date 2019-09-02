using UnityEngine;

public class Legs : MonoBehaviour
{
    public bool on_ground;  //Находятся ли ноги на поверхности
    public Collider legs;   //Сферический коллайдер ног

    void Update()
    {
        on_ground = Is_On_Ground();
    }
    
    /// <summary> Возвращает true если ноги находятся на поверхности </summary>
    bool Is_On_Ground()
    {
        float r;
        bool ret;
        Vector3 position;
        Collider[] colliders;

        r = legs.transform.lossyScale.y / 2 + 0.001f;   //Находим радиус немного больший чем радиус сферы коллайдера
        position = legs.transform.position;             //Находим позицию ног
        colliders = Physics.OverlapSphere(position, r); //Количество коллайдеров, задетых нашей сферой, описанной вокруг сферы ног

        ret = colliders.Length > 1;                     //Если задеты не только сами ноги, значит ноги находятся на поверхности

        return ret;
    }
}
