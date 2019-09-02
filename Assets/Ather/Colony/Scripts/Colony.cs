using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Colony : MonoBehaviour
{
    public Economic_Resource[] resource;
    public GameObject prefab_district;
    public Quarter[] quarters;
    public int count_Arcs = 10;

    [Serializable]
    public class Economic_Resource
    {
        public string name;
        public string about;
        public int count_building;
        public GameObject prefab;
    }

    void Start()
    {
        quarters[0].Initialise(this, count_Arcs, 0);
        quarters[1].Initialise(this, count_Arcs, 1);
        quarters[2].Initialise(this, count_Arcs, 2);
        quarters[3].Initialise(this, count_Arcs, 3);

    }

    void Update()
    {

    }




}
