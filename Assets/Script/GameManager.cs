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

    //�{�{�{���Ԑ����ǉ��{�{
    public GameObject timeBar;    //���ԕ\���C���[�W
    public GameObject timeText;   //���ԃe�L�X�g
    TimeContoroller timeCnt;      //TimeController

    Image titleImage;       //�摜��\�����Ă���Image�R���|�[�l���g

    //�{�{�{�X�R�A�ǉ��{�{�{
    public GameObject scoreText;    //�X�R�A�e�L�X�g
    public static int totalScore;   //���v�X�R�A
    public int stageScore = 0;      //�X�e�[�W�X�R�A
    
   // Start is called before the first frame update
    void Start()
    {
        //�摜���\���ɂ���
        Invoke("InactiveImage", 1.0f);
        //�{�^��(�p�l��)���\���ɂ���
        panel.SetActive(false);

        //�{�{�{���Ԑ����ǉ��{�{�{
        //TimeController���擾
        timeCnt = GetComponent<TimeContoroller>();
        if(timeCnt!=null)
        {
            if(timeCnt.gameTime==0.0f)
            {//�������ԃi�V�Ȃ�B��
                timeBar.SetActive(false);
            }
        }

        //�{�{�{�X�R�A�ǉ��{�{�{
        UpdateScore();
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

            //�{�{�{���Ԑ����ǉ��{�{�{
            if(timeCnt!=null)
            {
                timeCnt.isTimeOver = true;  //���ԃJ�E���g��~

                //�{�{�{�X�R�A�ǉ��{�{�{
                //�����ɑ�����邱�Ƃŏ�����؂�̂Ă�
                int time = (int)timeCnt.displayTime;
                totalScore += time * 10;    //�c�莞�Ԃ��X�R�A�ɉ�����
            }

            //�{�{�{�X�R�A�ǉ��{�{�{
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore();
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

            //�{�{�{���Ԑ����ǉ��{�{�{
            if(timeCnt!=null)
            {
                timeCnt.isTimeOver = true;  //���ԃJ�E���g��~
            }
        }
        //�Q�[����
        else if(PlayerController.gameState=="Playing")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerCnt = player.GetComponent<PlayerController>();   //PlayerController���擾����

            //+++���Ԑ����ǉ��{�{�{
            //�^�C�����X�V����
            if(timeCnt!=null)
            {
                if(timeCnt.gameTime>0.0f)
                {
                    //�����ɑ�����邱�Ƃŏ�����؂�̂Ă�
                    int time = (int)timeCnt.displayTime;
                    //�^�C���X�V
                    timeText.GetComponent<Text>().text = time.ToString();
                    //�^�C���I�[�o�[
                    if(time==0)
                    {
                        playerCnt.GameOver();//�Q�[���I�[�o�[�ɂ���
                    }
                }
            }

            //�{�{�{�X�R�A�ǉ��{�{�{
            if(playerCnt.score!=0)
            {
                stageScore += playerCnt.score;
                playerCnt.score = 0;
                UpdateScore();
            }
        }
    }

    //�摜���\���ɂ���
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    //�{�{�{�X�R�A�ǉ��{�{�{
    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();
    }
}
