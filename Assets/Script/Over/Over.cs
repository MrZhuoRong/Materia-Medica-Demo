using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Over: MonoBehaviour
{
    // Start is called before the first frame update

    public void BackToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void HandleExit(){
        Application.Quit();//ÍË³öÓÎÏ·
    }
}
