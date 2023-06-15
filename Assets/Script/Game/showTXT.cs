using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class showTXT : MonoBehaviour
{
    //如果读出来的中文是乱码请检查文本的格式是否为utf8
    public string fileName; // txt 文件名
    char splitChar= '\t'; // 分隔符
    public Text outputText;

    private List<string> cnList = new List<string>(); // 用来存取出来的中文的

    void Start()
    {
        ReadTxt();
        ShowCNText(1);
    }

    void ReadTxt()  //读文件
    {
        string fileUrl = Application.dataPath + "/" + fileName; // 获取 txt 文件路径
        StreamReader sr = new StreamReader(fileUrl, System.Text.Encoding.Default);
        string line;
        while ((line = sr.ReadLine()) != null) // 逐行读取 txt
        {
            string[] splitArr = line.Split(splitChar); // 将每一行按分隔符进行拆分
            if (splitArr.Length < 2) continue; // 判断长度是否小于2，若是，则跳过
            string numStr = splitArr[0]; // 序号字符串
            if (!int.TryParse(numStr, out int num)) continue; // 将序号字符串转换为数字，若转换失败，则跳过
            if (num == cnList.Count + 1) // 判断当前序号是否为中文列表的下一个序号
            {
                cnList.Add(splitArr[1]); // 如果是，则将中文添加到列表
            }
        }
        sr.Close(); // 关闭 StreamReader 对象
    }

    public void ShowCNText(int index)  //根据序号输出
    {
        if (index < 1 || index > cnList.Count) return; 
        string cnText = cnList[index - 1]; 
        Debug.Log(cnText);
        outputText.text = cnText.ToString(); 
    }

    public void SetTextEmpty()
    {
        outputText.text = "???";
    }
}
