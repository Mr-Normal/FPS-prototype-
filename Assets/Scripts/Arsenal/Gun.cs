using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Camera player_camera;
    [SerializeField]                                                            GameObject bullets_prefab;
    [Tooltip("Расположение дула (из него вылетает снаряд)")]                    public Transform barrel;
    [Range(0, 10)][SerializeField][Tooltip("Секунд до следующего выстрела")]    float disable_fire_seconds;
    [Range(0, 10)][Tooltip("Сила, с которой выталкивается снаряд из дула")]     public float add_force_mgn = 1;
    [Range(0, 10)][Tooltip("Пауза между выстрелами (в секундах)")]              public float seconds_to_cool = 0.5f;

    /// <summary> Выстреливает снаряд с указанной силой </summary>
    public void Fire()
    {
        Combat_Unit unit;
        Shell shell;

        unit = Find_Owner_Unit();                               //Находим хозяина выстрела
        shell = Spawn_Shell(bullets_prefab);                    //Создаем снаряд
        shell.Initialise(unit, barrel.rotation, add_force_mgn); //Инициализируем переменные
        disable_fire_seconds = seconds_to_cool;                 //Указываем - сколько времени осталось до следующего выстрела
    }

    /// <summary> Создает снаряд. Его позиция и направление будут совпадать с позицией и направлением дула. </summary>
    /// <param name="prefab"> Префаб снаряда, которым должно стрелять оружие </param>
    Shell Spawn_Shell(GameObject prefab)
    {
        GameObject go;
        Combat_Unit unit;
        Transform grand_parent;

        unit = Find_Owner_Unit();
        grand_parent = unit.transform.parent.transform;                                         //Находим контейнер всех снарядов
        go = GameObject.Instantiate(prefab, barrel.position, barrel.rotation, grand_parent);
        go.name += " " + go.GetInstanceID();

        return go.GetComponent<Shell>();
    }

    /// <summary> Возвращает true если из пушки можно сделать выстрел </summary>
    public bool Can_Fire()
    {
        if (gameObject.activeSelf && disable_fire_seconds == 0)
        {
            return true;
        }
        return false;
    }

    /// <summary> Уменьшает время до следующего выстрела. Функция должна быть расположена в Update чтобы корректно работать. </summary>
    protected void Cooling_Down()
    {
        if (disable_fire_seconds > 0)
        {
            disable_fire_seconds -= Time.deltaTime;
        }
        else
        {
            disable_fire_seconds = 0;
        }
    }

    /// <summary> Находит и возвращает хозяина пушки </summary>
    Combat_Unit Find_Owner_Unit()
    {
        Combat_Unit unit;

        unit = GetComponentInParent<Combat_Unit>();

        return unit;
    }
}
