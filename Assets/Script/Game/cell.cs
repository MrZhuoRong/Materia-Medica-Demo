using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cell : MonoBehaviour
{
    public Image texture;  //��Ʒ����

    public bag.ItemType bag_ItemType ; //�����������Ʒ����

    public int bag_ItemCount=0;  //������������

    public  Text bagCell_Text;  //��ʾ�������ı���

    public Sprite[] this_Texture;  //��Ʒ��ͼ

    public int cell_index;

    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        texture.gameObject.SetActive(bag_ItemCount>0);  //����Ʒ��������0��ʱ����ʾ
        texture.sprite = this_Texture[(int)bag_ItemType];  //������ͼ

        //����1ʱ��ʾ
        bagCell_Text.text = bag_ItemCount > 1 ? bag_ItemCount.ToString() : "";
    }
}
