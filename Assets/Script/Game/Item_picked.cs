using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_picked : MonoBehaviour
{
    public bag.ItemType itemType;  //��Ʒ����
    public int amount = 1;  //��Ʒ����
    public GameObject effect;  //ʰȡ��Ч
    public float effectTime=1f;  //��Ч������ʱ��

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //private void OnDestroy()  //������ǰ����øú���
    //{
    //    Destroy(Instantiate(effect,transform.position,transform.rotation), effectTime);
    //}
}
