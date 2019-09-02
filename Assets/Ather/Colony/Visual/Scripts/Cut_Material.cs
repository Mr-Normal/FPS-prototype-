using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut_Material : MonoBehaviour
{
    public Material material;
    public GameObject cut_center;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector4 vector4;
        Vector4 rotation4;
        Vector3 rotation_euler;

        //vector4 = material.GetVector("Cut_Center");
        vector4 = cut_center.transform.position;
        material.SetVector("Vector3_3644B3E8", vector4);
        rotation_euler = cut_center.transform.rotation.eulerAngles;
        rotation4 = rotation_euler;
        material.SetVector("Vector3_87E8BAEA", rotation4);
    }
}
