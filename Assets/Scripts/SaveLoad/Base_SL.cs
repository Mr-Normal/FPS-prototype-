using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

/// <summary> Базовый класс сохранения и загрузки (Base Save Load) </summary>
public class Base_SL : MonoBehaviour
{
    [Tooltip("Контейнер, в котором содержится вся сохраняемая/загружаемая информация")]
    public Data_SL data_SL;

    /// <summary> Создает пустой файл и сохраняет класс него, перезаписывая существующие с подобными именами </summary>
    /// <param name="path"> Полное имя файла                            </param>
    /// <param name="data"> Сериализуемый класс, который нужно записать </param>
    protected static void Save(string path, object data)
    {
        XmlSerializer serializer;
        FileStream file;

        serializer = new XmlSerializer(data.GetType()); //Создаем формат сериализации
        file = File.Create(path);                       //Создаем файл для записи данных
        serializer.Serialize(file, data);               //Записываем сериализованные данные
        file.Close();                                   //Закрываем файл

        Debug.Log("Записано в файл: " + path);
    }

    /// <summary> Возвращает класс из указанного файла. Возвращает null если не может прочитать. </summary>
    /// <param name="path"> Полное имя файла                    </param>
    /// <param name="type"> Тип загружаемых данных (имя класса) </param>
    public static object Load(string path, Type type)
    {
        XmlSerializer serializer;
        StreamReader file;
        object data;

        serializer = new XmlSerializer(type);           //Создаем формат сериализации

        try
        {
            file = new StreamReader(path);              //Открываем файл
            data = serializer.Deserialize(file);        //Десериализуем данные в переменную
            file.Close();                               //Закрываем файл
            Debug.Log("Прочитано из файла: " + path);
            return data;
        }
        catch (Exception)
        {
            Debug.LogWarning("Не удалось прочитать из файла: " + path);
            return null;
        }

    }

    /// <summary> 
    /// Возвращает полное имя файла в папке постоянного расположения. 
    /// Пример: "C:/Users/Alex/AppData/LocalLow/DefaultCompany/FPS (prototype)\options.txt" 
    /// </summary>
    /// <param name="file_name"> Неполное имя файла. Пример: "options.txt" </param>
    protected static string Get_Path(string file_name)
    {
        return Path.Combine(Application.persistentDataPath, file_name);
    }
}
