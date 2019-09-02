using UnityEngine;

public class FPS_Move : FPS
{
    [Tooltip("Скорость перемещения в метрах")]
    [Range(0, 10)]
    public float speed_mgn = 3;
    [Tooltip("Скорость вертикального прыжка в метрах")]
    [Range(0, 100)]
    public float jump_v_mgn = 10;
    [Tooltip("Величина торможения")]
    [Range(0, 50)]
    public float brake_force = 10;

    Rigidbody rb;   
    Legs legs;      

    void Start()
    {
        Find_Camera();
        rb = GetComponent<Rigidbody>();
        legs = Find_Legs(gameObject);
    }

    void Update()
    {
        Move_Controller();
    }

    /// <summary> Находит и возвращает ноги </summary>
    /// <param name="legs_owner"> Хозяин ног (ноги должны быть его детьми) </param>
    static Legs Find_Legs(GameObject legs_owner)
    {
        Legs legs;

        legs = legs_owner.GetComponentInChildren<Legs>();

        return legs;
    }

    /// <summary> Контролирует движение </summary>
    void Move_Controller()
    {
        if (legs.on_ground)             //Если ноги на поверхности
        {
            if (Is_Moving())
            {
                rb.drag = Drag_Move();
                Move();
            }
            else
            {
                rb.drag = Drag_Brake();
            }

            if (Is_Jump())
            {
                Jump();
            }
        }
        else
        {
            rb.drag = Drag_Fly();
        }
    }

    /// <summary> Перемещает юнит в соответствии с нажатыми клавишами </summary>
    private void Move()
    {
        Vector3 right;
        Vector3 forward;
        Vector3 velocity;

        right       = Get_Right();                          //Получаем вектор вправо
        forward     = Get_Forward();                        //Получаем вектор вперед

        velocity    = Get_Move_Velocity(right, forward);    //Находим скорости с учетом нажатых клавиш

        Set_V_Rigidbody(velocity);                          //Устанавливаем фиксированную скорость перемещения
    }

    /// <summary> Возвращает true если игрок нажимал на клавижи перемещения </summary>
    bool Is_Moving()
    {
        return Input.GetButton("Horizontal") || Input.GetButton("Vertical");
    }

    /// <summary> Находит скорость движения юнита (учитывает нажатые клавиши) </summary>
    /// <param name="right"     > Вектор, направленный вправо относительно взгляда юнита </param>
    /// <param name="forward"   > Вектор, направленный вперед относительно взгляда юнита </param>
    Vector3 Get_Move_Velocity(Vector3 right, Vector3 forward)
    {
        Vector3 velocity;

        velocity  = Input.GetAxis("Horizontal") * right;
        velocity += Input.GetAxis("Vertical") * forward;

        return velocity * speed_mgn;
    }
    
    /// <summary> Устанавливает скорость для Rigidbody </summary>
    void Set_V_Rigidbody(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    /// <summary> Добавляет скорость к Rigidbody </summary>
    void Add_V_Rigidbody(Vector3 velocity)
    {
        rb.velocity += velocity;
    }

    /// <summary> Получает вектор, направленный вперед относительно направления камеры (лежит на горизонтальной плоскости) </summary>
    Vector3 Get_Forward()
    {
        Vector3 vector;

        vector = Quaternion_To_Vector(camera.transform.rotation, Vector3.forward);

        vector.y = 0;

        return vector;
    }

    /// <summary> Получает вектор, направленный вправо относительно направления камеры </summary>
    Vector3 Get_Right()
    {
        return Quaternion_To_Vector(camera.transform.rotation, Vector3.right);
    }

    /// <summary> Возвращает повернутый вектор                                                      </summary>
    /// <param name="rotation"  > Угол поворота, выраженный в кватернионе                           </param>
    /// <param name="point"     > Вектор, который надо повернуть (центр поворота - нулевая точка)   </param>
    Vector3 Quaternion_To_Vector(Quaternion rotation, Vector3 point)
    {
        return rotation * point;
    }

    /// <summary> Подбрасывает Rigidbody вверх </summary>
    void Jump()
    {
        Vector3 velocity;

        velocity = Get_Jump_Velocity();

        Add_V_Rigidbody(velocity);
    }

    /// <summary> Возвращает true если нажата клавиша "Прыжок" </summary>
    bool Is_Jump()
    {
        return Input.GetButton("Jump");
    }

    /// <summary> Находит и возвращает начальную скорость прыжка </summary>
    Vector3 Get_Jump_Velocity()
    {
        Vector3 velocity;

        velocity = Vector3.up * jump_v_mgn;

        return velocity;
    }

    /// <summary> Возвращает сопротивление воздуха когда юнит тормозит </summary>
    float Drag_Brake()
    {
        return brake_force;
    }

    /// <summary> Возвращает сопротивление воздуха когда юнит летит </summary>
    float Drag_Fly()
    {
        return 0.1f;
    }

    /// <summary> Возвращает сопротивление воздуха когда юнит идёт/бежит </summary>
    float Drag_Move()
    {
        return 0.1f;
    }
}
