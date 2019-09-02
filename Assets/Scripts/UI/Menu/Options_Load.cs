using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class Options_Load : Base_SL
{
    void Start()
    {
        data_SL.options = Load();
    }

    /// <summary> Загружает данные настроек из файла </summary>
    public static Options_Data Load()
    {
        Options_Data data;
        string path;

        path = Get_Path("options.txt");
        data = (Options_Data)Load(path, typeof(Options_Data));

        return data;
    }


}
