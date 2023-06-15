using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;//设置成单例模式，方便调用

    public GameObject[] taskUIArrary;

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

    public void UpdateTaskList()//更新UI上任务列表
    {
        for(int i = 0; i < PlayerTask.instance.taskList.Count; i++)
        {
            //将名字和状态赋值
            taskUIArrary[i].transform.GetChild(0).GetComponent<Text>().text 
                = PlayerTask.instance.taskList[i].taskName;
            
            if (PlayerTask.instance.taskList[i].taskStatus==Task.TaskStatus.Accepted ) {
                taskUIArrary[i].transform.GetChild(1).GetComponent<Text>().text
                = "<color=red>" + "未完成" + "</color>";
            }else if (PlayerTask.instance.taskList[i].taskStatus == Task.TaskStatus.Completed)
            {
                taskUIArrary[i].transform.GetChild(1).GetComponent<Text>().text
                = "<color=green>" + "已完成" + "</color>";
            }
        }
        
    }
}
