using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MARKER ����ű�����ί�����񣬹����ڷ��������npc����
public class Taskable : MonoBehaviour
{
    public Task task;

    //MARKER ����NPC�Ի���ɺ����
    public void DelegateTask()
    {
        Player.canMove = true;
        if (task.taskStatus == Task.TaskStatus.Waitting)
        {
            //���񽫱���������������
            PlayerTask.instance.taskList.Add(task);
            task.taskStatus = Task.TaskStatus.Accepted;//������״̬
        }
        
    }
}
