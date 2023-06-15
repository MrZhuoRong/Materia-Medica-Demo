using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class bag : MonoBehaviour
{
    public enum ItemType
    {
        无 = 0, banlangen, lingzhi, renshen, gouwen
    }
    [System.Serializable]
    public struct ItemNature  //物品的特性
    {
        public ItemType itemType;
        public int MAX_Number;  //物品最大数量
    }



    //  public Camera thisCamera; //当前相机

    public Transform BagFater; //背包对象父物体
    public cell Bag;  //背包对象
    public int bagNumber = 54;  //背包格子数

    public List<bool> ListIsBagFull = new List<bool>(); //背包对象是否已满

    //public List<ItemType> itemsType_ed = new List<ItemType>();  //背包已经放置的物品类型
    //public List<int> items_ed=new List<int>(); //背包放置物品的数量

    public List<cell> Bags = new List<cell>();  //所以背包对象
    public ItemNature[] Natures;
    //  public static ItemNature[] theItemNatures;

    private Player player;

    private FaceBookController faceBookController;  //引用图鉴

    public void ChangeBagNumber(int banNumber)  //改变格子数量
    {
        for (int i = 0; i < bagNumber; i++)
        {
            ListIsBagFull.Add(false);
            Bag.cell_index = i;
            Bags.Add(Instantiate(Bag, BagFater));
            Bags[Bags.Count - 1].gameObject.SetActive(true);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = GameObject.Find("Player");
        player = gameObject.GetComponent<Player>();

        GameObject Object = GameObject.Find("FaceBookSystem");
        faceBookController = Object.GetComponent<FaceBookController>();
        ChangeBagNumber(bagNumber);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public int checkTheItemNature(bag.ItemType itemType)  //查询物品属性
    {
        int theItemNumber = 1;
        for (int j = 0; j < Natures.Length; j++)
        {
            if (Natures[j].itemType == itemType)
            {
                theItemNumber = Natures[j].MAX_Number;  //格子的物品数量大于等于设定一格子的上限数量，返回ture,即设该格子满了

            }
        }
        theItemNumber = Mathf.Max(1, theItemNumber);
        return theItemNumber;
    }

    public int checktemMAX(bag.ItemType itemType)  //查询物品最大上限值
    {
        int max_number = 0;
        for (int j = 0; j < Natures.Length; j++)
        {
            if (Natures[j].itemType == itemType)
            {
                max_number = Natures[j].MAX_Number;
            }
        }
        return max_number;
    }


    public void BagButton(GameObject button3)  //在格子的纹理上设置一个button，这样当格子没有物品时点不了
    {
        button3.SetActive(true);
    }

    public void cancelButton(GameObject button3)
    {
        button3.SetActive(false);
    }

    public void useItem(cell gameobject)  //使用
    {
        if (gameobject.bag_ItemCount > 0)
        {
            switch (gameobject.bag_ItemType)
            {
                case ItemType.banlangen:
                    {
                        if (player.currentHealth + 5 >= player.maxHealth)
                        {
                            player.currentHealth = player.maxHealth;
                        }
                        else
                        {
                            player.currentHealth += 5;
                        }
                    }
                    break;
                case ItemType.lingzhi:
                    {
                        player.maxHealth += 5;
                    }
                    break;
                case ItemType.renshen:
                    {
                        player.maxHealth += 10;
                        player.currentHealth = player.maxHealth;
                    }
                    break;
                case ItemType.gouwen:
                    {
                        player.currentHealth = 0;
                    }
                    break;
                default: break;
            }
            gameobject.bag_ItemCount -= 1;
            Debug.Log(gameobject.bag_ItemType);

            if (gameobject.bag_ItemCount < checktemMAX(gameobject.bag_ItemType))  //当物品数量小于物品单格存储上限时
            {
                //把满的标记设为false
                ListIsBagFull[gameobject.cell_index] = false;
            }


            if (gameobject.bag_ItemCount == 0)
            {
                gameobject.bag_ItemType = ItemType.无;
                //gameobject.texture = null;
            }

        }

    }

    public void discardItem(cell gameobject)  //抛弃
    {
        if (gameobject.bag_ItemCount > 0)
        {
            //把满的标记设为false
            ListIsBagFull[gameobject.cell_index] = false;

            gameobject.bag_ItemCount = 0;
            gameobject.bag_ItemType = ItemType.无;
            //gameobject.texture = null;
        }
    }

    public bool pickItem(Item_picked item_Picked) //拾取物品
    {
        if (ListIsBagFull.Contains(false)) //如果背包有空，返回一个true,
        {
            //遍历背包找空格子放进去
            for (int i = 0; i < Bags.Count; i++)
            {
                if (!ListIsBagFull[i])
                {
                    if (Bags[i].bag_ItemType == bag.ItemType.无) //找最靠前的空格子
                    {
                        Bags[i].bag_ItemType = item_Picked.itemType;
                        Bags[i].bag_ItemCount += item_Picked.amount;

                        //*****************************进行图鉴解锁
                        if (faceBookController.faceBooks[(int)item_Picked.itemType].BookLock != true)
                        {
                            faceBookController.faceBooks[(int)item_Picked.itemType].BookLock = true;
                        }
                        //************************************
                        ListIsBagFull[i] = Bags[i].bag_ItemCount >= checkTheItemNature(item_Picked.itemType);  //格子的物品数量大于等于设定一格子的上限数量，返回ture,即设该格子满了

                        //bag.ListIsBagFull[i] = true;
                        break;
                    }
                    else if (Bags[i].bag_ItemType == item_Picked.itemType)
                    {
                        Bags[i].bag_ItemCount += item_Picked.amount;

                        ListIsBagFull[i] = Bags[i].bag_ItemCount >= checkTheItemNature(item_Picked.itemType);
                        break;
                    }
                }

            }

            return true;
        }
        else
        {
            //背包已满
            return false;
        }

    }



    private void SwapCell(cell cell1, cell cell2)// 交换物品类型和物品数量
    {

        ItemType ItemType = cell1.bag_ItemType;
        int ItemCount = cell1.bag_ItemCount;

        cell1.bag_ItemType = cell2.bag_ItemType;
        cell1.bag_ItemCount = cell2.bag_ItemCount;

        cell2.bag_ItemType = ItemType;
        cell2.bag_ItemCount = ItemCount;
    }

    public void SortBag()  //整理背包
    {


        ItemStacking();//对物品进行叠加


        //排序
        for (int i = 0; i < Bags.Count - 1; i++)
        {
            for (int j = 0; j < Bags.Count - 1 - i; j++)
            {
                if (Bags[j].bag_ItemType.CompareTo(Bags[j + 1].bag_ItemType) > 0)
                {
                    // 交换两个 cell 对象的属性
                    SwapCell(Bags[j], Bags[j + 1]);
                    bool baglock = ListIsBagFull[j];
                    ListIsBagFull[j] = ListIsBagFull[j + 1];
                    ListIsBagFull[j + 1] = baglock;
                }
            }
        }

        // 按照背包位置进行重新排列
        int curIndex = 0;  //背包格子的顺位索引
        for (int i = 0; i < Bags.Count; i++)
        {
            if (Bags[i].bag_ItemType != ItemType.无)
            {

                SwapCell(Bags[curIndex], Bags[i]);
                bool baglock = ListIsBagFull[curIndex];
                ListIsBagFull[curIndex] = ListIsBagFull[i];
                ListIsBagFull[i] = baglock;
                curIndex++;

            }
        }


        //nCountSwapSort(); //位置整理
    }

    protected void ItemStacking()  //对物品进行叠加
    {
        ItemType itemType = ItemType.banlangen;

        while ((int)itemType != 5)
        {
            for (int i = 0; i < Bags.Count; i++)  //第一层循环，找第一个该类型物品
            {

                if (Bags[i].bag_ItemType == itemType && ListIsBagFull[i] != true)
                {
                    for (int j = i + 1; j < Bags.Count; j++)//第二层循环，找第二个该类型物品
                    {
                        if (Bags[j].bag_ItemType == itemType)
                        {
                            int nCount = checktemMAX(Bags[i].bag_ItemType) - Bags[i].bag_ItemCount; //第一个还差多少个到达上限

                            if (Bags[j].bag_ItemCount <= nCount && ListIsBagFull[i] != true) //如果第二个物品的数量能完全加在第一个物体上
                            {
                                Bags[i].bag_ItemCount += Bags[j].bag_ItemCount;
                                Bags[j].bag_ItemType = ItemType.无;
                                Bags[j].bag_ItemCount = 0;
                                ListIsBagFull[j] = false;

                                if (Bags[i].bag_ItemCount == checktemMAX(Bags[i].bag_ItemType))//如果第一个满了，则上锁
                                {
                                    ListIsBagFull[i] = true;
                                    break;
                                }
                            }
                            else  //第二个物品数量把第一个物体填满后还有剩
                            {
                                Bags[i].bag_ItemCount += nCount;
                                Bags[j].bag_ItemCount -= nCount;
                                ListIsBagFull[j] = false;
                                ListIsBagFull[i] = true;
                            }
                        }
                    }
                }
            }
            itemType++;
        }
    }

    //protected void nCountSwapSort()  //对物品按照数量进行位置调整
    //{
    //    for (int i = 0; i < Bags.Count-1; i++)
    //    {
    //        if (Bags[i].bag_ItemType == Bags[i + 1].bag_ItemType&&Bags[i].bag_ItemType!=ItemType.无)
    //        {
    //            if (Bags[i].bag_ItemCount < Bags[i + 1].bag_ItemCount)
    //            {
    //                SwapCell(Bags[i], Bags[i + 1]);
    //                ListIsBagFull[i] = true;
    //                ListIsBagFull[i+1] = false;
    //            }
    //        }
    //    }
    //}
}
