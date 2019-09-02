using UnityEngine;

/// <summary> Объект, имеющий hp, функцию получения урона и который уничтожается (при достижении значения hp меньше 1). </summary>
public class Destructable_Object : MonoBehaviour
{
    [Range(0, 100)]
    public int hp = 10;     //Количество оставшегося здоровья

    /// <summary> Уменьшает hp у этого объекта, возвращает true если hp меньше 1 а также вызывает функцию смерти. </summary>
    /// <param name="damage"> Количество hp, которое надо отнять у разрушаемого объекта </param>
    public bool TakeDamage(int damage)
    {
        if(hp > 0)
        {
            hp -= damage;
            if (hp <= 0)
            {
                Die();
                return true;
            }
        }
        return false;
    }

    /// <summary> Уничтожает игровой объект </summary>
    void Die()
    {
        Destroy(gameObject);
    }

}

