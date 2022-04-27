using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeContoroller : MonoBehaviour
{
    public bool isCountdown = true; //true=時間をカウントダウン計測する
    public float gameTime = 0;      //ゲームの最大時間
    public bool isTimeOver = false; //タイマー停止
    public float displayTime = 0;   //表示時間

    float times = 0;    //現在時間

    // Start is called before the first frame update
    void Start()
    {
        //カウントダウン
        if (isCountdown)
        {
            displayTime = gameTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimeOver==false)
        {
            times += Time.deltaTime;

            //カウントダウン
            if(isCountdown)
            {
                displayTime = gameTime - times;

                if(displayTime<=0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;
                }
            }
            //カウントアップ
            else
            {
                displayTime = times;

                if (displayTime >= gameTime)
                {
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }


            Debug.Log("TIMES:" + displayTime);
        }
    }
}
