using UnityEngine;

[System.Serializable]//�����л�
public class Task
{
    public enum TaskType { Gathering};//�ռ�������
    public enum TaskStatus { Waitting,Accepted,Completed};

    public string taskName;//��������
    public string[] taskNameArray = { "�ҵ�5���²�ժ�İ�����", "�ҵ�5���²�ժ����֥", "�ҵ�2���²�ժ���˲�", "�ҵ�2���²�ժ�Ĺ���" };//��������
    public TaskType taskType;//��������
    public TaskStatus taskStatus;//����״̬

    public int jiangli;//����

    [Header("Gathering Type Quest")]
    public int requireAmount;

}
