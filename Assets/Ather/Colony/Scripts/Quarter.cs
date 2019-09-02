using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarter : MonoBehaviour
{
    public List<Arc> arcs;
    public int index;
    public GameObject prefab_arc;
    public Colony colony;
    public GameObject collapse_container;
    public float wh = 100;
    public float w_road = 20;
    public float center_offcet = 120 * 7;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Initialise(Colony colony, int count, int index)
    {
        this.colony = colony;
        this.index = index;
        name = "Квартал " + index;
        arcs = new List<Arc>();
        Create(count);
        //Collapse();
    }

    /// <summary> Создает несколько дуг внутри квартала </summary>
    /// <param name="arc_count"> Количество дуг </param>
    void Create(int arc_count)
    {
        float r;
        float max_rad;
        int max_count;
        Arc arc;

        max_rad = Mathf.PI / 2;

        for (int i = 0; i < arc_count; i++)
        {
            r = (wh + w_road) * i + center_offcet;
            max_count = Max_Count(r, wh, w_road, max_rad);

            arc = Add("Дуга " + i);
            arc.Initalise(colony, max_count, r, max_rad, max_rad * index);
        }

    }

    void Collapse()
    {
        List<GameObject> gameObjects;

        gameObjects = Get_GameObjects();

        collapse_container = Collapse_Mesh.Collapse(gameObjects);

        collapse_container.transform.SetParent(this.transform);

        Collapse_Mesh.Destroy(gameObjects);
    }

    List<GameObject> Get_GameObjects()
    {
        List<GameObject> gameObjects;

        gameObjects = new List<GameObject>();

        foreach (Arc item in arcs)
        {
            gameObjects.Add(item.gameObject);
        }
        return gameObjects;
    }

    /// <summary> Добавляет дугу в квартал и возвращает ссылку на нее. </summary>
    /// <param name="name"> Новое имя для дуги </param>
    public Arc Add(string name)
    {
        Arc arc;
        GameObject arc_go;

        arc_go = Instantiate(prefab_arc);

        arc = arc_go.GetComponent<Arc>();

        arc.name = name;

        arc.transform.SetParent(this.transform);

        arcs.Add(arc);

        return arc;
    }

    /// <summary> Возвращает максимальное количество кубов, которые поместятся в указанную дугу </summary>
    /// <param name="r" > Средний радиус дуги </param>
    /// <param name="wh"> Ширина и высота в метрах </param>
    /// <param name="w_road"> Ширина дороги (минимальный отступ между фундаментами строений) </param>
    /// <param name="max_rad"> Максимальная длина дуги в радианах </param>
    int Max_Count(float r, float wh, float w_road, float max_rad)
    {
        int max;
        float l_m;
        float h05;
        float r_min;
        float res_wh;

        res_wh = wh + w_road;

        h05 = res_wh / 2;
        r_min = r - h05;

        l_m = max_rad * r_min;

        max = Mathf.FloorToInt(l_m / res_wh);

        return max;
    }
}
