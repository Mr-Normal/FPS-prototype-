using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    //public List<Structure> structures;
    public Colony colony;
    public List<GameObject> structures_go;
    public GameObject Collapse_Container;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary> Создает внутри дуги строения </summary>
    /// <param name="count"     > Количество строений </param>
    /// <param name="R"         > Радиус дуги </param>
    /// <param name="max_rad"   > Максимальная длина дуги в радианах </param>
    /// <param name="offset_rad"> Отступ дуги от нуля (в радианах) </param>
    public void Initalise(Colony colony, int count, double R, float max_rad, float offset_rad)
    {
        Vector3 position;
        double percent;
        double angle;
        double step;

        this.colony = colony;

        //structures = new List<Structure>();
        structures_go = new List<GameObject>();

        step = max_rad / count;

        for (int i = 0; i < count; i++)
        {
            percent = (i + 0.5f) / count;
            angle = percent * max_rad + offset_rad;
            position.y = 0;
            position.x = (float)(Math.Sin(angle) * R);
            position.z = (float)(Math.Cos(angle) * R);
            Add(position, angle, UnityEngine.Random.Range(0, 6));
        }
        Collapse();
    }

    /// <summary> Создает строение внутри дуги в указанной точке и разворачивает его на угол по оси y </summary>
    /// <param name="position"  > Позиция строения </param>
    /// <param name="angle"     > Угол разворота (в радианах) </param>
    void Add(Vector3 position, double angle, int type)
    {
        GameObject structure_go;
        Transform structure_tr;
        float angle_degrees;
        //Structure structure;

        structure_go = Instantiate(colony.resource[type].prefab);

        structure_tr = structure_go.transform;

        structure_tr.SetParent(this.transform);
        structure_tr.localPosition = position;

        angle_degrees = (float)(angle * 360 / (Math.PI * 2));

        structure_tr.localRotation = Quaternion.Euler(0, angle_degrees, 0);

        //structure = structure_go.GetComponent<Structure>();

        structures_go.Add(structure_go);
    }

    void Collapse()
    {
        Collapse_Container = Collapse_Mesh.Collapse(structures_go);
        Collapse_Container.transform.SetParent(this.transform);
        Collapse_Mesh.Destroy(structures_go);
    }


    
}
