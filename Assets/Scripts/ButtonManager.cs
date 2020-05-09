using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private static readonly string izzyWebsite = @"https://www.theizzyfoundation.org/";
    public void LoadScene(int index)
    {
        Application.LoadLevel(index);
    }

    public void OpenURL(){
        Application.OpenURL(izzyWebsite);
    }
}
