using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//该脚本放在需要判断个数的对象上
public class TaskTarge : MonoBehaviour
{
    public string taskName;
    public enum TaskType { Gathering };
    public TaskType taskType;

    

    public void TaskComplete()//收集齐药品后调用
    {
        for(int i = 0; i < PlayerTask.instance.taskList.Count; i++)
        {
            if (taskName == PlayerTask.instance.taskList[i].taskName
                && PlayerTask.instance.taskList[i].taskStatus==Task.TaskStatus.Accepted)//判断完成
            {
                if(taskType==TaskType.Gathering)
                {
                    if (PlayerTask.instance.itemAmount >= PlayerTask.instance.taskList[i].requireAmount)
                    {
                        PlayerTask.instance.taskList[i].taskStatus = Task.TaskStatus.Completed;
                        TaskManager.instance.UpdateTaskList();
                    }
                }
            }
        }
    }

}
