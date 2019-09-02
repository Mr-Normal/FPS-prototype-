using UnityEngine;

/// <summary> Объект, который может иметь несколько вариаций. </summary>
public class Variable_Object : Destructable_Object
{
    public int seed = -1;                   //Зерно для Random.InitState(int)
    public GameObject[] states_prefabs;     //Варианты объекта

    private void Start()
    {
        Initialise(seed);
    }

    /// <summary> Выбирает случайный префаб из списка и объявляет экземпляр </summary>
    /// <param name="seed"> Зерно для Random.InitState(int) </param>
    void Initialise(int seed)
    {
        this.seed = seed;
        int index;

        if (seed > -1)                                      //Если зерно объявлено, вносим его в функцию рандома
        {
            Random.InitState(seed);
        }

        index = Random.Range(0, states_prefabs.Length - 1); //Находим индекс префаба

        Change_State(index);                                //Меняем состояние вариативного объекта
    }

    /// <summary> Удаляет всех детей и помещает в качестве ребенка экземпляр указанного префаба </summary>
    /// <param name="index"> Индекс префаба в списке states_prefabs </param>
    void Change_State(int index)
    {
        GameObject[] childs;

        childs = Find_Childs();
        Delete_All(childs);

        Instantiate(states_prefabs[index], transform);
    }

    /// <summary> Удаляет указанные игровые объекты </summary>
    /// <param name="gos"> Игровые объекты, которые надо удалить </param>
    void Delete_All(GameObject[] gos)
    {
        GameObject go;

        gos = Find_Childs();

        for (int i = 0; i < gos.Length; i++)
        {
            go = gos[i];
            Destroy(go);
        }
    }

    /// <summary> Возвращает всех детей </summary>
    GameObject[] Find_Childs()
    {
        GameObject[] childs;
        int i;
        
        i       = 0;
        childs  = new GameObject[transform.childCount];

        foreach (Transform child in transform)
        {
            childs[i] = child.gameObject;
            i++;
        }

        return childs;
    }

}
