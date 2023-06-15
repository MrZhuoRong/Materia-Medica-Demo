using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rain;
   

    public float rainLife = 60f;  //下雨持续时间

    public float rainInterval = 120f;  //下雨间隔
    private float timeCount=0f;
    private bool rainLock = false;

    public Player player;
    public GameObject weather;
    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();

        //GameObject weatherObject = GameObject.Find("Video");
        //weather = weatherObject.transform.Find("weather").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timeCount+=Time.deltaTime;
        if(timeCount > rainInterval&&rainLock==false)
        {
            rainLock=true;
            openRain(rainLock);
        }
        if(timeCount > (rainLife+rainInterval))
        {
            timeCount=0;
            rainLock=false;
            openRain(rainLock);
            randRainTime();
        }

        if(player.currentHealth<=0)
        {
            openRain(false);
        }
    }

    void randRainTime()
    {
        rainLife = Random.Range(30f, 60f);
        rainInterval = Random.Range(120f, 360f);
    }

    void openRain(bool isOpen)
    {
        if (rain == null) { return; }
        if (weather == null) { return; }
        rain.gameObject.SetActive(isOpen);
        weather.gameObject.SetActive(isOpen);
    }



}
