using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MARKER 这个脚本负责委派任务，挂载在发布任务的npc身上
public class Taskable : MonoBehaviour
{
    public Task task;

    //MARKER 在与NPC对话完成后调用
    public void DelegateTask()
    {
        Player.canMove = true;
        if (task.taskStatus == Task.TaskStatus.Waitting)
        {
            //任务将被发布在人物身上
            PlayerTask.instance.taskList.Add(task);
            task.taskStatus = Task.TaskStatus.Accepted;//被接受状态
        }
        
    }
}
