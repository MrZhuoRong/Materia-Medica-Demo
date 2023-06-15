using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class JsonData
{
    public PlayerInfo playerInfo;

}
[System.Serializable]
public class PlayerInfo
{
    public int playerHP;
    public int playerMaxHP;
    public Vector3 playerPosition;

    public BagItem[] itemDatas;

    public List<bool> ListIsBagFull = new List<bool>();

    public bool[] FaceBookLocks;
}
[System.Serializable]
public class BagItem
{
    public int itemType;
    public int itemCount;
}


public class SaveAndLoad : MonoBehaviour
{
    public JsonData jsonData;  //存数据的变量

    //引用的对象
    public Player player;
    public bag bags;
    public FaceBookController faceBookController;
    // Start is called before the first frame update

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();

        GameObject bagObject = GameObject.Find("bagSystem");
        bags = bagObject.GetComponent<bag>();

        GameObject facebook = GameObject.Find("FaceBookSystem");
        faceBookController = facebook.GetComponent<FaceBookController>();

        InitJsonData();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            for (int i = 0; i < bags.Bags.Count; i++)
            {
                Debug.Log("类型：" + bags.Bags[i].bag_ItemType + "  数量：" + bags.Bags[i].bag_ItemCount);
            }
            for (int i = 0; i < jsonData.playerInfo.itemDatas.Length; i++)
            {
                Debug.Log("存储类型：" + jsonData.playerInfo.itemDatas[i].itemType + "  数量：" + jsonData.playerInfo.itemDatas[i].itemCount);
            }
        }
    }



    public void SaveData() //数据存储
    {
        //存角色数据
        jsonData.playerInfo.playerMaxHP = player.maxHealth;
        jsonData.playerInfo.playerHP = player.currentHealth;
        jsonData.playerInfo.playerPosition = player.transform.position;

        //存背包物品数据
        if (jsonData.playerInfo.itemDatas == null) // 如果 itemDatas 为空，则需要先创建数组对象
        {
            jsonData.playerInfo.itemDatas = new BagItem[bags.Bags.Count];

            for (int i = 0; i < bags.Bags.Count; i++)
            {
                jsonData.playerInfo.itemDatas[i] = new BagItem();
            }
        }
        else
        {
            for (int i = 0; i < bags.Bags.Count; i++)
            {
                if (i < jsonData.playerInfo.itemDatas.Length)
                {
                    jsonData.playerInfo.itemDatas[i].itemType = (int)bags.Bags[i].bag_ItemType;
                    jsonData.playerInfo.itemDatas[i].itemCount = bags.Bags[i].bag_ItemCount;
                }
                else
                {
                    Debug.Log("越界");
                }
            }
        }

        //存背包格子锁
        jsonData.playerInfo.ListIsBagFull.Clear();
        jsonData.playerInfo.ListIsBagFull.AddRange(bags.ListIsBagFull);


        //存图鉴锁
        if (jsonData.playerInfo.FaceBookLocks == null)
        {
            jsonData.playerInfo.FaceBookLocks = new bool[faceBookController.faceBooks.Length];
            for (int i = 0; i < faceBookController.faceBooks.Length; i++)
            {
                jsonData.playerInfo.FaceBookLocks[i] = false;
            }
        }
        else
        {
            for (int i = 1; i < faceBookController.faceBooks.Length; i++)
            {
                jsonData.playerInfo.FaceBookLocks[i] = faceBookController.faceBooks[i].BookLock;
            }
        }
    }

    public void LoadData()  //数据加载
    {
        player.maxHealth = jsonData.playerInfo.playerMaxHP;
        player.currentHealth = jsonData.playerInfo.playerHP;
        player.transform.position = jsonData.playerInfo.playerPosition;

        for (int i = 0; i < bags.Bags.Count; i++)
        {
            bags.Bags[i].bag_ItemType = (bag.ItemType)jsonData.playerInfo.itemDatas[i].itemType;
            bags.Bags[i].bag_ItemCount = jsonData.playerInfo.itemDatas[i].itemCount;
        }

        bags.ListIsBagFull.Clear();
        bags.ListIsBagFull.AddRange(jsonData.playerInfo.ListIsBagFull);

        for (int i = 1; i < faceBookController.faceBooks.Length; i++)
        {
            faceBookController.faceBooks[i].BookLock = jsonData.playerInfo.FaceBookLocks[i];
        }

    }

    void InitJsonData()  //初始化存放数据的变量
    {
        jsonData = new JsonData();
        jsonData.playerInfo = new PlayerInfo();
        //jsonData.playerInfo.itemDatas = new BagItem[bags.Bags.Count+1];
        //for (int i = 0; i < bags.Bags.Count+1; i++)
        //{
        //    jsonData.playerInfo.itemDatas[i] = new BagItem();

        //}
    }



    string JsonPath()  //指定文件生成路径
    {
        return Path.Combine(Application.streamingAssetsPath, "Data.json");
    }

    bool ExistsJson()  //检查是否存在该文件
    {
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
#if UNITY_EDITOR
            AssetDatabase.Refresh();  //创建后刷新
#endif
        }

        return File.Exists(JsonPath());
    }

    public void Save()
    {
        if (!ExistsJson())
        {
            File.Create(JsonPath()).Close();  //把文件关闭，不然会出现奇奇怪怪的错误
#if UNITY_EDITOR
            AssetDatabase.Refresh();  //创建后刷新
#endif
        }
        SaveData();
        string json = JsonUtility.ToJson(jsonData, true);
        File.WriteAllText(JsonPath(), json);  //写入
        Debug.Log("成功存储");

    }

    public void Load()
    {
        if (!ExistsJson())
        {
            return;

        }
        string json = File.ReadAllText(JsonPath());
        jsonData = JsonUtility.FromJson<JsonData>(json);
        LoadData();
        Debug.Log("成功读取");
    }
}
