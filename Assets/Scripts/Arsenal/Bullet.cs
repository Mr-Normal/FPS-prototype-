using UnityEngine;

public class Bullet : Shell
{
    [Range(0, 10)] public int damage;

    void Start()
    {
        Destroy(gameObject, time_to_death);
    }

    void Update()
    {
        Rotate();               //Разворачиваем снаряд по направлению его движения
        Stretch();              //Растягиваем его в соответствии с его скоростью
        //Try_Force_Destroy();    //Если снаряд слишком медленно, разрушаем его
    }

    /// <summary> Если снаряд слишком медленный, разрушаем его </summary>
    void Try_Force_Destroy()
    {
        if(rb.velocity.magnitude < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary> Растягивает пулю. Чем больше скорость, тем длиннее она становится. </summary>
    void Stretch()
    {
        float length;
        Vector3 scale;

        length = rb.velocity.magnitude * Time.deltaTime;    //Находим длину пули
        scale = transform.localScale;                       //Находим размер пули
        scale.z = length / 2;                               //Растягиваем пулю по оси z
        transform.localScale = scale;                       //Обновляем локальный размер пули
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destructable_Object dest_obj;

        dest_obj = Find_Target(collision);  //Находим скрипт разрушаемого объекта

        Damage(dest_obj, damage);           //Пытаемся принести вред разрушаемому объекту
    }

    /// <summary> Наносит вред указанному разрушаемому объекту </summary>
    void Damage(Destructable_Object target, int damage)
    {
        if (target != null)
        {
            if (Can_Damage(target))             //Если этому объекту разрешено наносить вред
            {
                if (target.TakeDamage(damage))  //Наносим вред, если объект в итоге полностью разрушен
                {
                    Add_Score();                //Добавляем очки хозяину снаряда
                }
            }
        }
    }

    /// <summary> Добавляем очки хозяину снаряда </summary>
    void Add_Score()
    {
        owner.score += score;
    }

    /// <summary> Находим скрипт уничтожаемого объекта среди родителей указанного коллайдера </summary>
    Destructable_Object Find_Target(Collision another)
    {
        GameObject go;

        go = another.gameObject;

        return Find_Target(go);
    }

    /// <summary> Находим скрипт уничтожаемого объекта среди родителей указанного коллайдера </summary>
    Destructable_Object Find_Target(GameObject go)
    {
        Destructable_Object target;

        target = go.GetComponentInParent<Destructable_Object>();

        return target;
    }

}
