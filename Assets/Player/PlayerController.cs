using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;          //Rigidbody2D型の変数
    float axisH = 0.0f;         //入力
    public float speed = 3.0f;  //移動速度

    public float jump = 9.0f;       //ジャンプ力
    public LayerMask groundLayer;   //着地できるレイヤー
    bool goJump = false;            //ジャンプ開始フラグ
    bool onGround = false;          //地面に立っているフラグ

    //アニメーション対応
    Animator animator;  //アニメーター
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";

    public static string gameState = "Playing"; //ゲームの状態
    public int score = 0;   //スコア

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2Dを取ってくる
        rbody = this.GetComponent<Rigidbody2D>();

        //animatorを取ってくる
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        gameState = "Playing";      //ゲーム中にする
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState!="Playing")
        {
            return;
        }

        //水平方向の入力をチェックする
        axisH = Input.GetAxisRaw("Horizontal");

        //向きの調整
        if(axisH>0.0f)
        {
            //右移動
            Debug.Log("右移動");
            transform.localScale = new Vector2(1,1);
        }
        else if(axisH<0.0f)
        {
            //左移動
            Debug.Log("左移動");
            transform.localScale = new Vector2(-1, 1);  //左右反転させる
        }

        //キャラクターをジャンプさせる
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if(gameState!="Playing")
        {
            return;
        }

        //地上判定
        onGround = Physics2D.Linecast(transform.position,
            transform.position - (transform.up * 0.1f), groundLayer);

        //地面の上か速度が０でなかったら
        if (onGround || axisH != 0)
        {
            //速度を更新する
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        //地面の上でジャンプキーが押されたら
        if(onGround&&goJump)
        {
            //ジャンプさせる
            Debug.Log("ジャンプ");
            Vector2 jumpPw = new Vector2(0, jump);  //ジャンプさせるベクトルをつくる
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //瞬間的な力を加える
            goJump = false;     //ジャンプフラグをおろす
        }

        if(onGround)
        {
            //地面の上
            if(axisH==0)
            {
                nowAnime = stopAnime;   //停止中
            }
            else
            {
                nowAnime = moveAnime;   //移動
            }
        }
        //空中
        else
        {
            nowAnime = jumpAnime;
        }
        if(nowAnime!=oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);    //アニメーション再生
        }
    }

    //ジャンプ
    public void Jump()
    {
        goJump = true;  //ジャンプフラグをたてる
        Debug.Log("ジャンプボタン押し");
    }

    //接触開始
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Goal")
        {
            Goal(); //ゴール
        }
        else if(collision.gameObject.tag=="Dead")
        {
            GameOver(); //ゲームオーバー
        }
        else if(collision.gameObject.tag=="ScoreItem")
        {
            //スコアアイテム
            //ItemDataを得る
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            //スコアを得る
            score = item.value;

            //アイテムを削除する
            Destroy(collision.gameObject);
        }
    }

    //ゴール
    public void Goal()
    {
        animator.Play(goalAnime);

        gameState = "gameclear";
        GameStop();  //ゲーム停止
    }

    //ゲームオーバー
    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "gameover";
        GameStop(); //ゲーム停止

        //ゲームオーバー演出
        //プレイヤーあたりを消す
        GetComponent<CapsuleCollider2D>().enabled = false;
        //プレイヤーを上に少し跳ね上げる演出
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    //ゲーム停止
    void GameStop()
    {
        //リジッドボディ2Dを取ってくる
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        //速度を０にして強制停止
        rbody.velocity = new Vector2(0, 0);
    }
}
