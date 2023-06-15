using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class patient : MonoBehaviour
{
    public GameObject dialog;

    public GameObject WaittingDialogueText;  //�Ի��ı���
    public GameObject AcceptedDialogueText;  //�Ի��ı���
    public GameObject CompletedDialogueText;  //�Ի��ı���
    // Start is called before the first frame update

    public Text WaittingText;
    public Text AcceptedText;
    public Text CompletedText;

    public GameObject WaittingBox;
    public GameObject AcceptedBox;
    public GameObject CompletedBox;

    //////////////////////////////////////////////
    //��������ϵͳ
    public Taskable taskable;//���ȷ��ί������
    ///////////////////////////////////////////////////

    public PlayerTask player;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        player=playerObject.GetComponent<PlayerTask>();

        dialog.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerStay2D(Collider2D collision) //��ײ��Ӧ
    //{
    //    PatientController.instance.currentTaskable = taskable;
        
    //    if (collision.gameObject.tag =="Player")
    //    {
    //        Debug.Log("2222222");
    //        if (Input.GetKeyUp(KeyCode.F))
    //        {
    //            Debug.Log("����");
    //            switch (taskable.task.taskStatus)
    //            {
    //                case Task.TaskStatus.Waitting:
    //                    int num = Random.Range(0, 4);
    //                    taskable.task.taskName = taskable.task.taskNameArray[num];
    //                    switch (num)
    //                    {
    //                        case 0:
    //                            taskable.task.requireAmount = 5;
    //                            break;
    //                        case 1:
    //                            taskable.task.requireAmount = 5;
    //                            break;
    //                        case 2:
    //                            taskable.task.requireAmount = 2;
    //                            break;
    //                        case 3:
    //                            taskable.task.requireAmount = 2;
    //                            break;
    //                    }
    //                    WaittingBox.SetActive(true);
    //                    dialog.SetActive(false);
    //                    WaittingDialogueText.SetActive(true);
    //                    collision.GetComponent<PlayerTask>().itemAmount = 0;
    //                    WaittingText.text = "ҽ�ݣ��������ˣ����м�����ҽ��ҩ�Ĳ�������Ҫ"
    //                        + taskable.task.taskName + "���鷳�������һ�°ɣ�лл��";
    //                    break; 
    //                case Task.TaskStatus.Accepted:
    //                    AcceptedBox.SetActive(true);
    //                    dialog.SetActive(false);
    //                    AcceptedDialogueText.SetActive(true);
    //                    AcceptedText.text = "ҽ�ݣ��������ˣ���û�����𣿺ð��鷳�����������ˣ���";
    //                    break; 
    //                case Task.TaskStatus.Completed:
    //                    CompletedBox.SetActive(true);
    //                    dialog.SetActive(false);
    //                    CompletedDialogueText.SetActive(true);
    //                    CompletedText.text = "ҽ�ݣ��������ˣ�̫лл���ˣ��Ҵ������Ǹ�л���ĸ�����";
    //                    for (int i = 0; i < PlayerTask.instance.taskList.Count; i++)
    //                    {
    //                        if (PlayerTask.instance.taskList[i].taskName == taskable.task.taskName)
    //                        {
    //                            PlayerTask.instance.taskList.Remove(PlayerTask.instance.taskList[i]);
    //                            TaskManager.instance.UpdateTaskList();
    //                        }
    //                    }
    //                    taskable.task.taskStatus = Task.TaskStatus.Waitting;
    //                    break;
    //            }
    //        }
    //    }
                           
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        PatientController.instance.currentTaskable = null;
        dialog.SetActive(true);
    }

    public void TaskAccess()
    {
        Player.canMove = false;
        PatientController.instance.currentTaskable = taskable;
        Debug.Log("����");
        switch (taskable.task.taskStatus)
        {
            case Task.TaskStatus.Waitting:
                int num = Random.Range(0, 4);
                taskable.task.taskName = taskable.task.taskNameArray[num];
                switch (num)
                {
                    case 0:
                        taskable.task.requireAmount = 5;

                        break;
                    case 1:
                        taskable.task.requireAmount = 5;
                        break;
                    case 2:
                        taskable.task.requireAmount = 2;
                        break;
                    case 3:
                        taskable.task.requireAmount = 2;
                        break;
                }
                WaittingBox.SetActive(true);
                dialog.SetActive(false);
                WaittingDialogueText.SetActive(true);
                player.itemAmount = 0;
                WaittingText.text = "ҽ�ݣ��������ˣ����м�����ҽ��ҩ�Ĳ�������Ҫ"
                   + taskable.task.taskName + "���鷳�������һ�°ɣ�лл��";
                break;
            case Task.TaskStatus.Accepted:
                AcceptedBox.SetActive(true);
                dialog.SetActive(false);
                AcceptedDialogueText.SetActive(true);
                AcceptedText.text = "ҽ�ݣ��������ˣ���û�����𣿺ð��鷳�����������ˣ���";
                break;
            case Task.TaskStatus.Completed:
                CompletedBox.SetActive(true);
                dialog.SetActive(false);
                CompletedDialogueText.SetActive(true);
                CompletedText.text = "ҽ�ݣ��������ˣ�̫лл���ˣ��Ҵ������Ǹ�л���ĸ�����";
                for (int i = 0; i < PlayerTask.instance.taskList.Count; i++)
                {
                    if (PlayerTask.instance.taskList[i].taskName == taskable.task.taskName)
                    {
                        PlayerTask.instance.taskList.Remove(PlayerTask.instance.taskList[i]);
                        taskable.task.taskName = taskable.task.taskNameArray[0];
                        TaskManager.instance.UpdateTaskList();
                    }
                }
                taskable.task.taskStatus = Task.TaskStatus.Waitting;
                break;
        }
        //if (taskable.task.taskStatus == Task.TaskStatus.Waitting)
        //{
        //    int num = Random.Range(0, 4);
        //    taskable.task.taskName = taskable.task.taskNameArray[num];
        //    switch (num)
        //    {
        //        case 0:
        //            taskable.task.requireAmount = 5;

        //            break;
        //        case 1:
        //            taskable.task.requireAmount = 5;
        //            break;
        //        case 2:
        //            taskable.task.requireAmount = 2;
        //            break;
        //        case 3:
        //            taskable.task.requireAmount = 2;
        //            break;
        //    }
        //    WaittingBox.SetActive(true);
        //    dialog.SetActive(false);
        //    WaittingDialogueText.SetActive(true);
        //    player.itemAmount = 0;
        //    WaittingText.text = "ҽ�ݣ��������ˣ����м�����ҽ��ҩ�Ĳ�������Ҫ"
        //       + taskable.task.taskName + "���鷳�������һ�°ɣ�лл��";
        //}
        //else if (taskable.task.taskStatus == Task.TaskStatus.Accepted)
        //{
        //    AcceptedBox.SetActive(true);
        //    dialog.SetActive(false);
        //    AcceptedDialogueText.SetActive(true);
        //    AcceptedText.text = "ҽ�ݣ��������ˣ���û�����𣿺ð��鷳�����������ˣ���";
        //}
        //else if (taskable.task.taskStatus == Task.TaskStatus.Completed)
        //{
        //    CompletedBox.SetActive(true);
        //    dialog.SetActive(false);
        //    CompletedDialogueText.SetActive(true);
        //    CompletedText.text = "ҽ�ݣ��������ˣ�̫лл���ˣ��Ҵ������Ǹ�л���ĸ�����";
        //    //for (int i = 0; i < PlayerTask.instance.taskList.Count; i++)
        //    //{
        //    //    if (PlayerTask.instance.taskList[i].taskName == taskable.task.taskName)
        //    //    {
        //    //        taskable.task.taskName = "???";
        //    //        PlayerTask.instance.taskList.Remove(PlayerTask.instance.taskList[i]);

        //    //    }
        //    //}
        //}
    }
}
