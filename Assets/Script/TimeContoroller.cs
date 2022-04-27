using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeContoroller : MonoBehaviour
{
    public bool isCountdown = true; //true=���Ԃ��J�E���g�_�E���v������
    public float gameTime = 0;      //�Q�[���̍ő厞��
    public bool isTimeOver = false; //�^�C�}�[��~
    public float displayTime = 0;   //�\������

    float times = 0;    //���ݎ���

    // Start is called before the first frame update
    void Start()
    {
        //�J�E���g�_�E��
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

            //�J�E���g�_�E��
            if(isCountdown)
            {
                displayTime = gameTime - times;

                if(displayTime<=0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;
                }
            }
            //�J�E���g�A�b�v
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
