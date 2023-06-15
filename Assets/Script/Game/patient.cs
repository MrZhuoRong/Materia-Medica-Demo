using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class patient : MonoBehaviour
{
    public GameObject dialog;

    public GameObject WaittingDialogueText;  //对话文本框
    public GameObject AcceptedDialogueText;  //对话文本框
    public GameObject CompletedDialogueText;  //对话文本框
    // Start is called before the first frame update

    public Text WaittingText;
    public Text AcceptedText;
    public Text CompletedText;

    public GameObject WaittingBox;
    public GameObject AcceptedBox;
    public GameObject CompletedBox;

    //////////////////////////////////////////////
    //新增任务系统
    public Taskable taskable;//点击确定委派任务
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

    //private void OnTriggerStay2D(Collider2D collision) //碰撞响应
    //{
    //    PatientController.instance.currentTaskable = taskable;
        
    //    if (collision.gameObject.tag =="Player")
    //    {
    //        Debug.Log("2222222");
    //        if (Input.GetKeyUp(KeyCode.F))
    //        {
    //            Debug.Log("交互");
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
    //                    WaittingText.text = "医馆：“好心人！流感季节咱医馆药材不够，需要"
    //                        + taskable.task.taskName + "，麻烦你帮我找一下吧！谢谢！";
    //                    break; 
    //                case Task.TaskStatus.Accepted:
    //                    AcceptedBox.SetActive(true);
    //                    dialog.SetActive(false);
    //                    AcceptedDialogueText.SetActive(true);
    //                    AcceptedText.text = "医馆：“好心人！还没集齐吗？好吧麻烦您继续加油了！”";
    //                    break; 
    //                case Task.TaskStatus.Completed:
    //                    CompletedBox.SetActive(true);
    //                    dialog.SetActive(false);
    //                    CompletedDialogueText.SetActive(true);
    //                    CompletedText.text = "医馆：“好心人！太谢谢您了！我代表病人们感谢您的付出！";
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
        Debug.Log("交互");
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
                WaittingText.text = "医馆：“好心人！流感季节咱医馆药材不够，需要"
                   + taskable.task.taskName + "，麻烦你帮我找一下吧！谢谢！";
                break;
            case Task.TaskStatus.Accepted:
                AcceptedBox.SetActive(true);
                dialog.SetActive(false);
                AcceptedDialogueText.SetActive(true);
                AcceptedText.text = "医馆：“好心人！还没集齐吗？好吧麻烦您继续加油了！”";
                break;
            case Task.TaskStatus.Completed:
                CompletedBox.SetActive(true);
                dialog.SetActive(false);
                CompletedDialogueText.SetActive(true);
                CompletedText.text = "医馆：“好心人！太谢谢您了！我代表病人们感谢您的付出！";
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
        //    WaittingText.text = "医馆：“好心人！流感季节咱医馆药材不够，需要"
        //       + taskable.task.taskName + "，麻烦你帮我找一下吧！谢谢！";
        //}
        //else if (taskable.task.taskStatus == Task.TaskStatus.Accepted)
        //{
        //    AcceptedBox.SetActive(true);
        //    dialog.SetActive(false);
        //    AcceptedDialogueText.SetActive(true);
        //    AcceptedText.text = "医馆：“好心人！还没集齐吗？好吧麻烦您继续加油了！”";
        //}
        //else if (taskable.task.taskStatus == Task.TaskStatus.Completed)
        //{
        //    CompletedBox.SetActive(true);
        //    dialog.SetActive(false);
        //    CompletedDialogueText.SetActive(true);
        //    CompletedText.text = "医馆：“好心人！太谢谢您了！我代表病人们感谢您的付出！";
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
