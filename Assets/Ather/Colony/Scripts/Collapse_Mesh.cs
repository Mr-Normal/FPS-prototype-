using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse_Mesh
{
    /// <summary> Все объекты, обладающие материалом с одинаковым названием, объединяются в один игровой объект с MeshCollider </summary>
    /// <param name="gameObjects"> Игровые объекты, которые надо объединить </param>
    public static GameObject Collapse(List<GameObject> gameObjects)
    {
        Material[] unic_materials;
        List<GameObject> gameObjects_visible;
        List<List<MeshFilter>> meshFilters;             //Меши, разделенный по типам ([уникальный тип, позиция в списке])
        GameObject collapse_container;

        collapse_container = new GameObject("Collapse Container");

        //gameObjects = Get_GameObjects(structures);
        gameObjects_visible = Get_All_Renderer_Childs(gameObjects);
        unic_materials = Get_Unic_Materials(gameObjects_visible);
        meshFilters = Separate_MeshFilters(unic_materials, gameObjects_visible);
        Create_Collapsing_GameObjects(meshFilters, collapse_container.transform);
        
        return collapse_container;
    }

    public static void Destroy(List<GameObject> gameObjects)
    {
        foreach (GameObject item in gameObjects)
        {
            GameObject.Destroy(item);
        }
    }

    static GameObject[] Create_Collapsing_GameObjects(List<List<MeshFilter>> meshFilters, Transform parent)
    {
        List<GameObject> gameObjects;
        Material material;
        GameObject gameObject;

        gameObjects = new List<GameObject>();

        foreach (List<MeshFilter> item in meshFilters)
        {
            material = item[0].gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            gameObject = Create_Collapse_GameObject(item, material, parent);
            gameObjects.Add(gameObject);
        }

        return gameObjects.ToArray();
    }

    static GameObject Create_Collapse_GameObject(List<MeshFilter> meshFilters, Material material, Transform parent)
    {
        Mesh mesh;
        GameObject go;
        MeshFilter filter;
        MeshRenderer renderer;
        MeshCollider collider;

        mesh = Collapse(meshFilters.ToArray());

        go = new GameObject();

        go.name = material.name;

        filter = go.AddComponent<MeshFilter>();
        filter.mesh = mesh;

        renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;

        collider = go.AddComponent<MeshCollider>();
        collider.sharedMesh = mesh;

        go.transform.SetParent(parent);

        return go;
    }

    /// <summary> Разделяет мешфильтры на структурный массив по типу [индекс типа материала, индекс позиции мешфильтра] </summary>
    /// <param name="unic_materials"></param>
    /// <param name="gameObjects"></param>
    static List<List<MeshFilter>> Separate_MeshFilters(Material[] unic_materials, List<GameObject> gameObjects)
    {
        List<List<MeshFilter>> meshFilters;
        Material mat_u, mat_go;

        meshFilters = new List<List<MeshFilter>>(unic_materials.Length);

        //Объявляем первое измерение (количество записей равно количеству уникальных типов материала)
        for (int i_smf = 0; i_smf < unic_materials.Length; i_smf++)
        {
            meshFilters.Add(new List<MeshFilter>());
        }

        //Распределяем игровые объекты по соответствующему типу
        foreach (GameObject item in gameObjects)
        {
            mat_go = item.GetComponent<MeshRenderer>().sharedMaterial;
            for (int i_mat = 0; i_mat < unic_materials.Length; i_mat++)
            {
                mat_u = unic_materials[i_mat];
                if (mat_u.name == mat_go.name)
                {
                    meshFilters[i_mat].Add(item.GetComponent<MeshFilter>());
                }
            }
        }

        return meshFilters;
    }




    static List<GameObject> Get_All_Renderer_Childs(List<GameObject> gameObjects)
    {
        List<MeshRenderer> meshRenderers;
        List<GameObject> all_rendere_childs;

        all_rendere_childs = new List<GameObject>();

        meshRenderers = Get_All_MeshRenderer(gameObjects);

        foreach (MeshRenderer item in meshRenderers)
        {
            all_rendere_childs.Add(item.gameObject);
        }

        return all_rendere_childs;
    }

    static List<MeshRenderer> Get_All_MeshRenderer(List<GameObject> gameObjects)
    {
        List<MeshRenderer> meshRenderers;

        meshRenderers = new List<MeshRenderer>();

        foreach (GameObject item in gameObjects)
        {
            meshRenderers.AddRange(Get_All_MeshRenderer_In_Childs(item));
        }

        return meshRenderers;
    }

    static MeshRenderer[] Get_All_MeshRenderer_In_Childs(GameObject gameObject)
    {
        return gameObject.transform.GetComponentsInChildren<MeshRenderer>();
    }

    static private Material[] Get_Unic_Materials(List<GameObject> gameObjects)
    {
        List<Material> materials;
        List<string> names;
        Material material;

        materials = new List<Material>();
        names = new List<string>();

        foreach (GameObject item in gameObjects)
        {
            material = item.GetComponent<MeshRenderer>().sharedMaterial;

            if (!names.Contains(material.name))
            {
                names.Add(material.name);
                materials.Add(material);
            }
        }

        return materials.ToArray();
    }


    static Mesh Collapse(GameObject[] gameObjects)
    {
        MeshFilter[] meshFilters;

        meshFilters = new MeshFilter[gameObjects.Length];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            meshFilters[i] = gameObjects[i].GetComponent<MeshFilter>();
        }

        return Collapse(meshFilters);
    }

    static Mesh Collapse(MeshFilter[] meshFilters)
    {
        Mesh mesh;
        CombineInstance[] combine;

        combine = Get_Combine(meshFilters);

        mesh = new Mesh();

        mesh.CombineMeshes(combine);

        return mesh;
    }

    static CombineInstance[] Get_Combine(MeshFilter[] meshFilters)
    {
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }
        return combine;
    }
}
