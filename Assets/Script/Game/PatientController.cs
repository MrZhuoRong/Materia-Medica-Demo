using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientController : MonoBehaviour
{
    //public static PatientController Instance;
    //////////////////////////////////////////////
    //新增任务系统
    public static PatientController instance;//设置成单例模式，方便调用
    public Taskable currentTaskable;//点击确定委派任务
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);

    }

    public GameObject DialogueText;  //对话文本框

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setAnyActive(GameObject any)
    {
       any.SetActive(false);
    }

    public void showAnyActive(GameObject any)
    {
        any.SetActive(true);
    }

    public void acceptTask()
    {
        currentTaskable.DelegateTask();
        TaskManager.instance.UpdateTaskList();
    }

    public void completeTask()
    {
        TaskManager.instance.taskUIArrary[0].transform.GetChild(0).GetComponent<Text>().text
            =  "???" ;
        TaskManager.instance.taskUIArrary[0].transform.GetChild(1).GetComponent<Text>().text
        = "<color=black>" + "???" + "</color>";
        currentTaskable.task.taskStatus = Task.TaskStatus.Waitting;
        PlayerTask.instance.taskList.Remove(PlayerTask.instance.taskList[0]);
        Debug.Log(PlayerTask.instance.taskList.Count);
        TaskManager.instance.UpdateTaskList();
    }

}
