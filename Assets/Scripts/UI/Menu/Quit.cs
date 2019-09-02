using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit: MonoBehaviour
{
    /// <summary> Закрывает приложение </summary>
    public void Application_Quit()
    {
        Application.Quit();
    }
}
