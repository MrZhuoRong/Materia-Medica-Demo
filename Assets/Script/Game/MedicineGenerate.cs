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
        //将所有方块位置放入一维数组
        tilemap=GetComponent<Tilemap>();

        Vector3Int tmOrg= tilemap.origin;
        Vector3Int tmSz= tilemap.size;

        //初始化grassTileWorldPos
        for(int x=tmOrg.x;x<tmSz.x;x++)
        {
            for(int y=tmOrg.y;y<tmSz.y; y++)
            {
                if (tilemap.GetTile(new Vector3Int(x, y, 0))!=null)
                {
                    Vector3 cellToWorldPos=tilemap.GetCellCenterWorld(new Vector3Int(x, y,0));//把cell位置转化为世界位置
                    grassTileWorldPos.Add(cellToWorldPos);
                }
            }
        }

        grassTileCount = grassTileWorldPos.Count;
        resourcesCount=resources.Count;

        for(int i=0;i< needCount; i++)
        {
            //每隔一段时间生成一次
            int aRandomTile = Random.Range(0, grassTileCount);
            Vector3 spawnPos = grassTileWorldPos[aRandomTile];
            //随机生成一个资源
            int aRandomRes = Random.Range(0, resourcesCount);
            GameObject spawnRes = resources[aRandomRes];
            //生成
            Instantiate(spawnRes, spawnPos, Quaternion.identity, MedicineParents);//预制体，位置，旋转
            
        }
        
    }

}
