using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_picked : MonoBehaviour
{
    public bag.ItemType itemType;  //物品类型
    public int amount = 1;  //物品数量
    public GameObject effect;  //拾取特效
    public float effectTime=1f;  //特效持续的时间

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //private void OnDestroy()  //在销毁前会调用该函数
    //{
    //    Destroy(Instantiate(effect,transform.position,transform.rotation), effectTime);
    //}
}
