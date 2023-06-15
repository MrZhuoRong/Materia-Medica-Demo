using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;//���óɵ���ģʽ���������

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

    public void UpdateTaskList()//����UI�������б�
    {
        for(int i = 0; i < PlayerTask.instance.taskList.Count; i++)
        {
            //�����ֺ�״̬��ֵ
            taskUIArrary[i].transform.GetChild(0).GetComponent<Text>().text 
                = PlayerTask.instance.taskList[i].taskName;
            
            if (PlayerTask.instance.taskList[i].taskStatus==Task.TaskStatus.Accepted ) {
                taskUIArrary[i].transform.GetChild(1).GetComponent<Text>().text
                = "<color=red>" + "δ���" + "</color>";
            }else if (PlayerTask.instance.taskList[i].taskStatus == Task.TaskStatus.Completed)
            {
                taskUIArrary[i].transform.GetChild(1).GetComponent<Text>().text
                = "<color=green>" + "�����" + "</color>";
            }
        }
        
    }
}
