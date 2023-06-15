using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cell : MonoBehaviour
{
    public Image texture;  //物品纹理

    public bag.ItemType bag_ItemType ; //这个背包的物品类型

    public int bag_ItemCount=0;  //背包放置数量

    public  Text bagCell_Text;  //显示数量的文本框

    public Sprite[] this_Texture;  //物品贴图

    public int cell_index;

    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        texture.gameObject.SetActive(bag_ItemCount>0);  //当物品数量大于0的时候显示
        texture.sprite = this_Texture[(int)bag_ItemType];  //赋予贴图

        //大于1时显示
        bagCell_Text.text = bag_ItemCount > 1 ? bag_ItemCount.ToString() : "";
    }
}
