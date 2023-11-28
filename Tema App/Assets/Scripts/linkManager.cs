using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linkManager : MonoBehaviour
{
    public void OpenLinks(string link)
    {
        Application.OpenURL(link);
    }
    
}
