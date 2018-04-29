using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mean_menu : MonoBehaviour {
    public void playgame()
    {
        SceneManager.LoadScene(1);
    }
    public void gamequit()
    {
        Application.Quit();
    }

}
