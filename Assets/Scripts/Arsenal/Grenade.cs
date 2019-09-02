using System.Collections.Generic;
using UnityEngine;

public class Grenade : Shell
{
    Destructable_Object[] targets;  //Цели, задетые взрывом
    [Tooltip("Радиус взрыва")]      [Range(0.1f, 100)]  public float radius;                    
    [Tooltip("Максимальный урон")]  [Range(1, 100)]     public float max_damage;                
    [Tooltip("Сила разброса")]      [Range(0, 1000)]    public float explode_force = 100;
    [Tooltip("Префаб взрыва")]                          public GameObject prefab_explosion; 

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Destroy(gameObject, time_to_death);
        Rotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.activeSelf)          //Запрещаем повторное срабатывание
        {
            Explode();                      //При столкновении - взорвать снаряд
            gameObject.SetActive(false);    //Дезактивируем снаряд чтобы не было повторных срабатываний
        }
    }

    /// <summary> Находит цели в радиусе и взрывает их, после чего уничтожает взорванный снаряд </summary>
    void Explode()
    {
        targets = Find_Targets(transform.position, radius);     //Находим цели внутри сферы
        Explode(targets);                                       //Подвергаем цели взрыву
        Destroy(gameObject);                                    //Уничтожаем снаряд
    }

    /// <summary> Находит и возвращает уничтожаемые объекты внутри сферы    </summary>
    /// <param name="position"  > Позиция сферы поиска                      </param>
    /// <param name="radius"    > Радиус сферы поиска                       </param>
    static Destructable_Object[] Find_Targets(Vector3 position, float radius)
    {
        Collider[] colliders;
        List<Destructable_Object> ret;
        Destructable_Object target;

        ret         = new List<Destructable_Object>();
        colliders   = Physics.OverlapSphere(position, radius);  //Находим все коллайдеры внутри сферы

        for (int i = 0; i < colliders.Length; i++)              //Проверяем все коллайдеры
        {
            target = Find_DO(colliders[i].gameObject);          //Находим у коллайдера уничтожаемый объект

            if (target != null)                                 //Если это уничтожаемый объект
            {
                ret.Add(target);                                //Добавляем объект в список
            }
        }

        return ret.ToArray();
    }

    /// <summary> Находит скрипт разрушаемого объекта у (родителя) потенциального обладателя </summary>
    /// <param name="target_potential"> Потенциальный обладатель разрушаемого объекта </param>
    static Destructable_Object Find_DO(GameObject target_potential)
    {
        Destructable_Object destructable;

        destructable = target_potential.GetComponentInParent<Destructable_Object>();

        return destructable;
    }

    /// <summary> Находит скрипты разрушаемых объектов у (родителей) потенциальных обладателей </summary>
    void Explode(Destructable_Object[] targets)
    {
        Destructable_Object target;

        if (targets.Length == 0) { Debug.Log("Разрушаемых объектов не обнаружено. "); }

        for (int i = 0; i < targets.Length; i++)
        {
            target = targets[i];
            Explode(target);
            Damage_To(target);
        }
    }

    /// <summary> Отбрасывает указанную цель, лишает книнематичного состояния и добавляет влияние гравитации </summary>
    void Explode(Destructable_Object target)
    {
        Rigidbody rb;

        rb = Find_RB(target);

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddExplosionForce(explode_force, transform.position, radius);
    }

    /// <summary> Наносит урон указанной цели </summary>
    void Damage_To(Destructable_Object target)
    {
        Vector3 distance;
        int damage;
        bool is_destroy;

        if (Can_Damage(target))
        {
            distance    = target.transform.position - transform.position;
            damage      = Get_Damage(distance.magnitude);
            is_destroy  = target.TakeDamage(damage);

            if (is_destroy)
            {
                owner.score += score;
            }
        }

    }

    /// <summary> Находит урон, соответствующий отдаленности цели от центра взрыва  </summary>
    /// <param name="distance"> Вектор-расстояние центра цели до центра взрыва      </param>
    int Get_Damage(float distance)
    {
        float percent;
        int damage;

        percent = distance / radius;
        percent = Mathf.Min(1, percent);
        damage  = Mathf.RoundToInt(percent * max_damage);

        return damage;
    }

    /// <summary> Находит Rigidbody </summary>
    static Rigidbody Find_RB(Destructable_Object destructable)
    {
        return destructable.GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        GameObject go;
        //Создает игровой объект, содержащий анимацию взрыва
        go = Instantiate(prefab_explosion, transform.position, Quaternion.identity);
        //Удаляет объект после 1 секунды анимации
        Destroy(go, 1);
    }
}
