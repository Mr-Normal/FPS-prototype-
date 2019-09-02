using UnityEngine;

public class FPS_Actions : FPS
{
    public Gun[] guns;  //Список оружия, которым владеет игрок

    void Start()
    {
        guns = Find_Guns(gameObject);
    }

    void Update()
    {
        Fire();
    }

    /// <summary> Прослушивает ввод и если нажата кнопка атаки, проихводит выстрел </summary>
    void Fire()
    {
        if (Input.GetButton("Fire1"))               //Если нажата кнопка "Огонь"
        {
            Fire_All();
        }
    }

    /// <summary> Просматривает всё оружие и стреляет из всего, из чего можно выстрелить </summary>
    void Fire_All()
    {
        Gun gun;

        for (int i = 0; i < guns.Length; i++)   //Просматривает всё оружие
        {
            gun = guns[i];
            if (gun.Can_Fire())                 //И если оно может выстрелить
            {
                gun.Fire();                     //Активируем выстрел
            }
        }
    }

    /// <summary> Находит и возвращает всё активное и неактивное оружие среди детей указанного хозяина </summary>
    /// <param name="guns_owner"> Хозяин, у которого в детях находится оружие </param>
    static Gun[] Find_Guns(GameObject guns_owner)
    {
        Gun[] guns;
        guns = guns_owner.GetComponentsInChildren<Gun>(true);
        return guns;
    }


}
