using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;
    public Text textnum;

    public void StartMenu()
    {
        StartCoroutine(Loadlevel());
        
    }
    public void HandleExit()
    {
        Application.Quit();//�˳���Ϸ
    }

    IEnumerator Loadlevel()
    {
        loadScreen.SetActive(true);
        AsyncOperation operation=SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
        operation.allowSceneActivation = false;//�ݲ���ת

        while (!operation.isDone) 
        {
            slider.value = operation.progress;

            textnum.text = operation.progress * 100 + "%";

            if(operation.progress>=0.9f)
            {
                slider.value = 1;
                textnum.text = "�밴�¿ո��������Ϸ��";
                if(Input.GetKeyUp(KeyCode.Space))
                {
                    operation.allowSceneActivation = true;
                    Player.canMove = true;

                }
            }

            yield return null;
        }
    }
}
