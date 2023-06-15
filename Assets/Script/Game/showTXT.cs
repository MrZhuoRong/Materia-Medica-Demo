using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class showTXT : MonoBehaviour
{
    //��������������������������ı��ĸ�ʽ�Ƿ�Ϊutf8
    public string fileName; // txt �ļ���
    char splitChar= '\t'; // �ָ���
    public Text outputText;

    private List<string> cnList = new List<string>(); // ������ȡ���������ĵ�

    void Start()
    {
        ReadTxt();
        ShowCNText(1);
    }

    void ReadTxt()  //���ļ�
    {
        string fileUrl = Application.dataPath + "/" + fileName; // ��ȡ txt �ļ�·��
        StreamReader sr = new StreamReader(fileUrl, System.Text.Encoding.Default);
        string line;
        while ((line = sr.ReadLine()) != null) // ���ж�ȡ txt
        {
            string[] splitArr = line.Split(splitChar); // ��ÿһ�а��ָ������в��
            if (splitArr.Length < 2) continue; // �жϳ����Ƿ�С��2�����ǣ�������
            string numStr = splitArr[0]; // ����ַ���
            if (!int.TryParse(numStr, out int num)) continue; // ������ַ���ת��Ϊ���֣���ת��ʧ�ܣ�������
            if (num == cnList.Count + 1) // �жϵ�ǰ����Ƿ�Ϊ�����б����һ�����
            {
                cnList.Add(splitArr[1]); // ����ǣ���������ӵ��б�
            }
        }
        sr.Close(); // �ر� StreamReader ����
    }

    public void ShowCNText(int index)  //����������
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
