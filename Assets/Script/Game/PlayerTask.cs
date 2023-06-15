using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTask : MonoBehaviour
{
    public static PlayerTask instance;//设置成单例模式，方便调用

    public List<Task> taskList = new List<Task>();

    public int itemAmount = 0;//目前收集的
    //public Dictionary<string ,Task>taskDict = new Dictionary<string ,Task>();//字典
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
