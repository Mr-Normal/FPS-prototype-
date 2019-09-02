using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class Options_Save : Base_SL
{
    /// <summary> Сохраняет в файл настройки </summary>
    public void Save()
    {
        string path;

        path = Get_Path("options.txt");
        Save(path, data_SL.options);
    }
}
