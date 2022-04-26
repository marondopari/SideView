using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //UI���g���̂ɕK�v

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;    //�摜������gameObject
    public Sprite gameOverSpr;      //GAME OVER�摜
    public Sprite gameClearSpr;     //GAME CLEAR�摜
    public GameObject panel;        //�p�l��
    public GameObject restartButton;    //RESTART�{�^��
    public GameObject nextButton;       //�l�N�X�g�{�^��

    Image titleImage;       //�摜��\�����Ă���Image�R���|�[�l���g
    
   // Start is called before the first frame update
    void Start()
    {
        //�摜���\���ɂ���
        Invoke("InactiveImage", 1.0f);
        //�{�^��(�p�l��)���\���ɂ���
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[�����N���A������
        if(PlayerController.gameState=="gameclear")
        {
            mainImage.SetActive(true);  //�摜��\������
            panel.SetActive(true);      //�{�^��(�p�l��)��\������

            //RESTART�{�^���𖳌�������
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;  //�摜��ݒ肷��
            PlayerController.gameState = "gameend";
        }
        //�Q�[���I�[�o�[��������
        else if(PlayerController.gameState=="gameover")
        {
            mainImage.SetActive(true);      //�摜��\������
            panel.SetActive(true);          //�{�^��(�p�l���j��\������
            //NEXT�{�^���𖳌�������
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;   //�摜��ݒ肷��
            PlayerController.gameState = "gameend";
        }
        //�Q�[����
        else if(PlayerController.gameState=="Playing")
        {

        }
    }

    //�摜���\���ɂ���
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
