using UnityEngine;

/// <summary> Скрипт, расставляющий на карте экземпляры префабов (расположение - в форме квадрата) </summary>
public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;        //Префабы, которые будут расставляться внутри квадрата по алгоритму случайного выбора
    [Range(0.1f, 30)]
    public float distance_between = 5;  //Расстояние между каждым экземпляром по x или y в юнитах
    [Range(0, 30)]
    public int radius = 5;              //Количество объектов, расположенных на половине стороны квадрата

    void Start()
    {
        Spawn_Area(radius);
    }

    /// <summary> Запускает алгоритм расставления на карте экземпляров префаба </summary>
    /// <param name="r"> Количество объектов, расположенных на половине стороны квадрата </param>
    void Spawn_Area(int r)
    {
        Vector3 position;

        for (int x = -r; x < r; x++)
        {
            for (int z = -r; z < r; z++)
            {
                position = new Vector3(x, 0, z) * distance_between;
                Spawn_Random(position);
            }
        }
    }

    /// <summary> Создает экземпляр случайного префаба (из списка префабов) и перемещает его на указанную позицию </summary>
    /// <param name="position"> Позиция, на которой должен быть расположен экземпляр префаба </param>
    void Spawn_Random(Vector3 position)
    {
        int index;

        index = Random.Range(0, prefabs.Length);

        Instantiate(prefabs[index], position, Quaternion.identity, transform);
    }
    



}
