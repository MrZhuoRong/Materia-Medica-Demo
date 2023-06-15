using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    

    public GameObject menuList;
    public GameObject bagList;
    public GameObject TaskList;
    //public GameObject gameOver;
    
   
    // Start is called before the first frame update
    void Start()
    {
        menuList.SetActive(false);
        bagList.SetActive(false);
        //gameOver.SetActive(false);
        TaskList.SetActive(false);

       
    }
    private void Update()
    {
        HandleKeyUp();
    }
    public void HandlemenuList()
    {
        if (menuList != null)
        {
            if (menuList.activeSelf == false)
            {
                menuList.SetActive(true);
                Player.canMove=false;
             }
            else
            {
                menuList.SetActive(false);
                Player.canMove = true;
            }
        }
    }
    public void ShowBag()
    {
        if (bagList != null)
        {
            if (bagList.activeSelf == false)
            {
                bagList.SetActive(true);
                Player.canMove = false;
            }
            else
            {
                bagList.SetActive(false);
                Player.canMove = true;
            }
        }
    }

    public void ShowTask()
    {
        if(TaskList.activeSelf==false)
        {
            TaskList.SetActive(true);
            Player.canMove = false;
        }
        else
        {
            TaskList.SetActive(false);
            Player.canMove = true;
        }
    }


    //public void HandleGameOver()
    //{
    //    if (gameOver != null)
    //    {
    //        gameOver.SetActive(true);
    //    }
    //}
    public void HandleKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            HandlemenuList();
        }
        if(Input.GetKeyUp(KeyCode.B))
        {
            ShowBag();
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void HandleExit(){
        Application.Quit();//ÍË³öÓÎÏ·
    }
}
