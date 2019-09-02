using UnityEngine;

public abstract class Shell : Destructable_Object
{
    [Range(1, 10)] [Tooltip("Очки, добавляемые к очкам хозяина всякий раз, когда снаряд уничтожает объект")]    public int score;
    [Range(1, 10)] [Tooltip("Время, отведенное для жизни снаряда с момента его инициализации")]                 public float time_to_death = 3; 
    [HideInInspector]   public Rigidbody rb;
    public Combat_Unit owner;

    /// <summary> Разворачивает по направлению движения объекта </summary>
    protected void Rotate()
    {
        Vector3 velocity;

        velocity = rb.velocity + transform.position;
        transform.LookAt(velocity);
    }

    /// <summary> Инициализирует снаряд                                     </summary>
    /// <param name="owner"             > Юнит, которому принадлежит снаряд </param>
    /// <param name="shoot_angle"       > Направление выстрела              </param>
    /// <param name="force_magnitude"   > Магнитуда силы выстрела           </param>
    public void Initialise(Combat_Unit owner, Quaternion shoot_angle, float force_magnitude)
    {
        Vector3 direction;  //Направление выстрела
        Vector3 force;      //Сила выстрела
        Rigidbody unit_rb;  //Тело хозяина

        this.owner = owner;

        rb          = GetComponent<Rigidbody>();                //Находим (и запоминаем) Rigidbody снаряда
        unit_rb     = owner.GetComponent<Rigidbody>();          //Находим тело снаряда

        direction   = shoot_angle * Vector3.forward;            //Находим направление выстрела в виде Vector3
        force       = direction.normalized * force_magnitude;   //Находим силу выстрела (в виде вектора)

        rb.velocity += unit_rb.velocity;                        //Прибавляем к скорости снаряда скорость хозяина выстрела
        rb.AddForce(force, ForceMode.Impulse);                  //Придаем импульс снаряду

    }

    /// <summary> Возвращает true если снаряду разрешено наносить урон указанному объекту </summary>
    public bool Can_Damage(Destructable_Object target)
    {
        return target.gameObject != owner.gameObject;           //Разрешаем наносить урон всем, кроме хозяина снаряда
    }

}
