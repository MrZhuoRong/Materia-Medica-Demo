using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceBookController : MonoBehaviour
{
    
    public int txtIndex=1;  //���ͼƬ��Ӧ���ı�����

    [System.Serializable]
    public struct faceBook
    {
        public Sprite image;
        public bool BookLock;
    }

    public faceBook[] faceBooks;

    public Image thisImage;

    private showTXT ShowTXT;//����

    public Text PageText;  //ҳ��
    public Text unlockNumberText;  //ͼ����������/��������
   
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = GameObject.Find("FaceBookSystem");
        ShowTXT = gameObject.GetComponent<showTXT>();

        thisImage.sprite = faceBooks[txtIndex].image;
        ShowTXT.ShowCNText(txtIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PreviousPage()  //��һҳ����
    {
        if (txtIndex == 1)
            return;

        txtIndex--;
        changeFaceBook();
    }
    public void NextPage()  //��һҳ����
    {
        if (txtIndex < faceBooks.Length-1)
        {
            txtIndex++;
        }
        changeFaceBook();
    }

    public void changeFaceBook()
    {
        if (faceBooks[txtIndex].BookLock != true)
        {
            thisImage.sprite = faceBooks[0].image;  //��Ϊδ������ͼƬ
            ShowTXT.SetTextEmpty();  //���ÿ��ı�
        }
        else
        {
            thisImage.sprite = faceBooks[txtIndex].image;
            ShowTXT.ShowCNText(txtIndex);
        }

        PageText.text=txtIndex.ToString();
    }

    public int GetNulockNumber() //�鿴����Ʒ�Ƿ��Ѿ�����
    {
        int unlockNumber=0;

        for(int i=1;i<faceBooks.Length;i++)
        {
            if(faceBooks[i].BookLock == true)
            {
                unlockNumber++;
            }
        }

        return unlockNumber;
    }

    public void openButton(GameObject gameObject)  //��ͼ��ҳ�棬��������������
    {
        gameObject.SetActive(true);
        txtIndex = 1;
        changeFaceBook();

        unlockNumberText.text="���ȣ�"+GetNulockNumber().ToString()+"/"+(faceBooks.Length-1).ToString();
    }
}
