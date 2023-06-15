using UnityEngine;

[System.Serializable]//可序列化
public class Task
{
    public enum TaskType { Gathering};//收集类任务
    public enum TaskStatus { Waitting,Accepted,Completed};

    public string taskName;//任务名称
    public string[] taskNameArray = { "找到5份新采摘的板蓝根", "找到5份新采摘的灵芝", "找到2份新采摘的人参", "找到2份新采摘的钩吻" };//任务名称
    public TaskType taskType;//任务类型
    public TaskStatus taskStatus;//任务状态

    public int jiangli;//奖励

    [Header("Gathering Type Quest")]
    public int requireAmount;

}
