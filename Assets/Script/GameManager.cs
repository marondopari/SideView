using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //UIを使うのに必要

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;    //画像を持つgameObject
    public Sprite gameOverSpr;      //GAME OVER画像
    public Sprite gameClearSpr;     //GAME CLEAR画像
    public GameObject panel;        //パネル
    public GameObject restartButton;    //RESTARTボタン
    public GameObject nextButton;       //ネクストボタン

    Image titleImage;       //画像を表示しているImageコンポーネント
    
   // Start is called before the first frame update
    void Start()
    {
        //画像を非表示にする
        Invoke("InactiveImage", 1.0f);
        //ボタン(パネル)を非表示にする
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームがクリアしたら
        if(PlayerController.gameState=="gameclear")
        {
            mainImage.SetActive(true);  //画像を表示する
            panel.SetActive(true);      //ボタン(パネル)を表示する

            //RESTARTボタンを無効化する
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;  //画像を設定する
            PlayerController.gameState = "gameend";
        }
        //ゲームオーバーだったら
        else if(PlayerController.gameState=="gameover")
        {
            mainImage.SetActive(true);      //画像を表示する
            panel.SetActive(true);          //ボタン(パネル）を表示する
            //NEXTボタンを無効化する
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;   //画像を設定する
            PlayerController.gameState = "gameend";
        }
        //ゲーム中
        else if(PlayerController.gameState=="Playing")
        {

        }
    }

    //画像を非表示にする
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
