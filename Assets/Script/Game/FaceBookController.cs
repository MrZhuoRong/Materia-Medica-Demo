using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceBookController : MonoBehaviour
{
    
    public int txtIndex=1;  //存放图片对应的文本索引

    [System.Serializable]
    public struct faceBook
    {
        public Sprite image;
        public bool BookLock;
    }

    public faceBook[] faceBooks;

    public Image thisImage;

    private showTXT ShowTXT;//引用

    public Text PageText;  //页码
    public Text unlockNumberText;  //图鉴解锁数量/上限数量
   
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

    public void PreviousPage()  //上一页按下
    {
        if (txtIndex == 1)
            return;

        txtIndex--;
        changeFaceBook();
    }
    public void NextPage()  //下一页按下
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
            thisImage.sprite = faceBooks[0].image;  //设为未解锁的图片
            ShowTXT.SetTextEmpty();  //设置空文本
        }
        else
        {
            thisImage.sprite = faceBooks[txtIndex].image;
            ShowTXT.ShowCNText(txtIndex);
        }

        PageText.text=txtIndex.ToString();
    }

    public int GetNulockNumber() //查看该物品是否已经解锁
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

    public void openButton(GameObject gameObject)  //打开图鉴页面，对索引进行重置
    {
        gameObject.SetActive(true);
        txtIndex = 1;
        changeFaceBook();

        unlockNumberText.text="进度："+GetNulockNumber().ToString()+"/"+(faceBooks.Length-1).ToString();
    }
}
