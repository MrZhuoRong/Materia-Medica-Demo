using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ýű�������Ҫ�жϸ����Ķ�����
public class TaskTarge : MonoBehaviour
{
    public string taskName;
    public enum TaskType { Gathering };
    public TaskType taskType;

    

    public void TaskComplete()//�ռ���ҩƷ�����
    {
        for(int i = 0; i < PlayerTask.instance.taskList.Count; i++)
        {
            if (taskName == PlayerTask.instance.taskList[i].taskName
                && PlayerTask.instance.taskList[i].taskStatus==Task.TaskStatus.Accepted)//�ж����
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
