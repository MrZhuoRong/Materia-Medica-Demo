using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class bag : MonoBehaviour
{
    public enum ItemType
    {
        �� = 0, banlangen, lingzhi, renshen, gouwen
    }
    [System.Serializable]
    public struct ItemNature  //��Ʒ������
    {
        public ItemType itemType;
        public int MAX_Number;  //��Ʒ�������
    }



    //  public Camera thisCamera; //��ǰ���

    public Transform BagFater; //������������
    public cell Bag;  //��������
    public int bagNumber = 54;  //����������

    public List<bool> ListIsBagFull = new List<bool>(); //���������Ƿ�����

    //public List<ItemType> itemsType_ed = new List<ItemType>();  //�����Ѿ����õ���Ʒ����
    //public List<int> items_ed=new List<int>(); //����������Ʒ������

    public List<cell> Bags = new List<cell>();  //���Ա�������
    public ItemNature[] Natures;
    //  public static ItemNature[] theItemNatures;

    private Player player;

    private FaceBookController faceBookController;  //����ͼ��

    public void ChangeBagNumber(int banNumber)  //�ı��������
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


    public int checkTheItemNature(bag.ItemType itemType)  //��ѯ��Ʒ����
    {
        int theItemNumber = 1;
        for (int j = 0; j < Natures.Length; j++)
        {
            if (Natures[j].itemType == itemType)
            {
                theItemNumber = Natures[j].MAX_Number;  //���ӵ���Ʒ�������ڵ����趨һ���ӵ���������������ture,����ø�������

            }
        }
        theItemNumber = Mathf.Max(1, theItemNumber);
        return theItemNumber;
    }

    public int checktemMAX(bag.ItemType itemType)  //��ѯ��Ʒ�������ֵ
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


    public void BagButton(GameObject button3)  //�ڸ��ӵ�����������һ��button������������û����Ʒʱ�㲻��
    {
        button3.SetActive(true);
    }

    public void cancelButton(GameObject button3)
    {
        button3.SetActive(false);
    }

    public void useItem(cell gameobject)  //ʹ��
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

            if (gameobject.bag_ItemCount < checktemMAX(gameobject.bag_ItemType))  //����Ʒ����С����Ʒ����洢����ʱ
            {
                //�����ı����Ϊfalse
                ListIsBagFull[gameobject.cell_index] = false;
            }


            if (gameobject.bag_ItemCount == 0)
            {
                gameobject.bag_ItemType = ItemType.��;
                //gameobject.texture = null;
            }

        }

    }

    public void discardItem(cell gameobject)  //����
    {
        if (gameobject.bag_ItemCount > 0)
        {
            //�����ı����Ϊfalse
            ListIsBagFull[gameobject.cell_index] = false;

            gameobject.bag_ItemCount = 0;
            gameobject.bag_ItemType = ItemType.��;
            //gameobject.texture = null;
        }
    }

    public bool pickItem(Item_picked item_Picked) //ʰȡ��Ʒ
    {
        if (ListIsBagFull.Contains(false)) //��������пգ�����һ��true,
        {
            //���������ҿո��ӷŽ�ȥ
            for (int i = 0; i < Bags.Count; i++)
            {
                if (!ListIsBagFull[i])
                {
                    if (Bags[i].bag_ItemType == bag.ItemType.��) //���ǰ�Ŀո���
                    {
                        Bags[i].bag_ItemType = item_Picked.itemType;
                        Bags[i].bag_ItemCount += item_Picked.amount;

                        //*****************************����ͼ������
                        if (faceBookController.faceBooks[(int)item_Picked.itemType].BookLock != true)
                        {
                            faceBookController.faceBooks[(int)item_Picked.itemType].BookLock = true;
                        }
                        //************************************
                        ListIsBagFull[i] = Bags[i].bag_ItemCount >= checkTheItemNature(item_Picked.itemType);  //���ӵ���Ʒ�������ڵ����趨һ���ӵ���������������ture,����ø�������

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
            //��������
            return false;
        }

    }



    private void SwapCell(cell cell1, cell cell2)// ������Ʒ���ͺ���Ʒ����
    {

        ItemType ItemType = cell1.bag_ItemType;
        int ItemCount = cell1.bag_ItemCount;

        cell1.bag_ItemType = cell2.bag_ItemType;
        cell1.bag_ItemCount = cell2.bag_ItemCount;

        cell2.bag_ItemType = ItemType;
        cell2.bag_ItemCount = ItemCount;
    }

    public void SortBag()  //������
    {


        ItemStacking();//����Ʒ���е���


        //����
        for (int i = 0; i < Bags.Count - 1; i++)
        {
            for (int j = 0; j < Bags.Count - 1 - i; j++)
            {
                if (Bags[j].bag_ItemType.CompareTo(Bags[j + 1].bag_ItemType) > 0)
                {
                    // �������� cell ���������
                    SwapCell(Bags[j], Bags[j + 1]);
                    bool baglock = ListIsBagFull[j];
                    ListIsBagFull[j] = ListIsBagFull[j + 1];
                    ListIsBagFull[j + 1] = baglock;
                }
            }
        }

        // ���ձ���λ�ý�����������
        int curIndex = 0;  //�������ӵ�˳λ����
        for (int i = 0; i < Bags.Count; i++)
        {
            if (Bags[i].bag_ItemType != ItemType.��)
            {

                SwapCell(Bags[curIndex], Bags[i]);
                bool baglock = ListIsBagFull[curIndex];
                ListIsBagFull[curIndex] = ListIsBagFull[i];
                ListIsBagFull[i] = baglock;
                curIndex++;

            }
        }


        //nCountSwapSort(); //λ������
    }

    protected void ItemStacking()  //����Ʒ���е���
    {
        ItemType itemType = ItemType.banlangen;

        while ((int)itemType != 5)
        {
            for (int i = 0; i < Bags.Count; i++)  //��һ��ѭ�����ҵ�һ����������Ʒ
            {

                if (Bags[i].bag_ItemType == itemType && ListIsBagFull[i] != true)
                {
                    for (int j = i + 1; j < Bags.Count; j++)//�ڶ���ѭ�����ҵڶ�����������Ʒ
                    {
                        if (Bags[j].bag_ItemType == itemType)
                        {
                            int nCount = checktemMAX(Bags[i].bag_ItemType) - Bags[i].bag_ItemCount; //��һ��������ٸ���������

                            if (Bags[j].bag_ItemCount <= nCount && ListIsBagFull[i] != true) //����ڶ�����Ʒ����������ȫ���ڵ�һ��������
                            {
                                Bags[i].bag_ItemCount += Bags[j].bag_ItemCount;
                                Bags[j].bag_ItemType = ItemType.��;
                                Bags[j].bag_ItemCount = 0;
                                ListIsBagFull[j] = false;

                                if (Bags[i].bag_ItemCount == checktemMAX(Bags[i].bag_ItemType))//�����һ�����ˣ�������
                                {
                                    ListIsBagFull[i] = true;
                                    break;
                                }
                            }
                            else  //�ڶ�����Ʒ�����ѵ�һ��������������ʣ
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

    //protected void nCountSwapSort()  //����Ʒ������������λ�õ���
    //{
    //    for (int i = 0; i < Bags.Count-1; i++)
    //    {
    //        if (Bags[i].bag_ItemType == Bags[i + 1].bag_ItemType&&Bags[i].bag_ItemType!=ItemType.��)
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
