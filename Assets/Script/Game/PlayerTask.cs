using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTask : MonoBehaviour
{
    public static PlayerTask instance;//���óɵ���ģʽ���������

    public List<Task> taskList = new List<Task>();

    public int itemAmount = 0;//Ŀǰ�ռ���
    //public Dictionary<string ,Task>taskDict = new Dictionary<string ,Task>();//�ֵ�
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
        
    }
}
