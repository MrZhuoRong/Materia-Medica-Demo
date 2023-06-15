using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MedicineGenerate : MonoBehaviour
{
    private Tilemap tilemap;
    public List<GameObject> resources=new List<GameObject>();

    private List<Vector3> grassTileWorldPos=new List<Vector3>();
    private int grassTileCount;
    private int resourcesCount;

    public int needCount = 50;

    public Transform MedicineParents;
    private void Start()
    {
        //�����з���λ�÷���һά����
        tilemap=GetComponent<Tilemap>();

        Vector3Int tmOrg= tilemap.origin;
        Vector3Int tmSz= tilemap.size;

        //��ʼ��grassTileWorldPos
        for(int x=tmOrg.x;x<tmSz.x;x++)
        {
            for(int y=tmOrg.y;y<tmSz.y; y++)
            {
                if (tilemap.GetTile(new Vector3Int(x, y, 0))!=null)
                {
                    Vector3 cellToWorldPos=tilemap.GetCellCenterWorld(new Vector3Int(x, y,0));//��cellλ��ת��Ϊ����λ��
                    grassTileWorldPos.Add(cellToWorldPos);
                }
            }
        }

        grassTileCount = grassTileWorldPos.Count;
        resourcesCount=resources.Count;

        for(int i=0;i< needCount; i++)
        {
            //ÿ��һ��ʱ������һ��
            int aRandomTile = Random.Range(0, grassTileCount);
            Vector3 spawnPos = grassTileWorldPos[aRandomTile];
            //�������һ����Դ
            int aRandomRes = Random.Range(0, resourcesCount);
            GameObject spawnRes = resources[aRandomRes];
            //����
            Instantiate(spawnRes, spawnPos, Quaternion.identity, MedicineParents);//Ԥ���壬λ�ã���ת
            
        }
        
    }

}
