using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;          //Rigidbody2D�^�̕ϐ�
    float axisH = 0.0f;         //����
    public float speed = 3.0f;  //�ړ����x

    public float jump = 9.0f;       //�W�����v��
    public LayerMask groundLayer;   //���n�ł��郌�C���[
    bool goJump = false;            //�W�����v�J�n�t���O
    bool onGround = false;          //�n�ʂɗ����Ă���t���O

    //�A�j���[�V�����Ή�
    Animator animator;  //�A�j���[�^�[
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";

    public static string gameState = "Playing"; //�Q�[���̏��
    public int score = 0;   //�X�R�A

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D������Ă���
        rbody = this.GetComponent<Rigidbody2D>();

        //animator������Ă���
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        gameState = "Playing";      //�Q�[�����ɂ���
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState!="Playing")
        {
            return;
        }

        //���������̓��͂��`�F�b�N����
        axisH = Input.GetAxisRaw("Horizontal");

        //�����̒���
        if(axisH>0.0f)
        {
            //�E�ړ�
            Debug.Log("�E�ړ�");
            transform.localScale = new Vector2(1,1);
        }
        else if(axisH<0.0f)
        {
            //���ړ�
            Debug.Log("���ړ�");
            transform.localScale = new Vector2(-1, 1);  //���E���]������
        }

        //�L�����N�^�[���W�����v������
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

        //�n�㔻��
        onGround = Physics2D.Linecast(transform.position,
            transform.position - (transform.up * 0.1f), groundLayer);

        //�n�ʂ̏ォ���x���O�łȂ�������
        if (onGround || axisH != 0)
        {
            //���x���X�V����
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        //�n�ʂ̏�ŃW�����v�L�[�������ꂽ��
        if(onGround&&goJump)
        {
            //�W�����v������
            Debug.Log("�W�����v");
            Vector2 jumpPw = new Vector2(0, jump);  //�W�����v������x�N�g��������
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //�u�ԓI�ȗ͂�������
            goJump = false;     //�W�����v�t���O�����낷
        }

        if(onGround)
        {
            //�n�ʂ̏�
            if(axisH==0)
            {
                nowAnime = stopAnime;   //��~��
            }
            else
            {
                nowAnime = moveAnime;   //�ړ�
            }
        }
        //��
        else
        {
            nowAnime = jumpAnime;
        }
        if(nowAnime!=oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);    //�A�j���[�V�����Đ�
        }
    }

    //�W�����v
    public void Jump()
    {
        goJump = true;  //�W�����v�t���O�����Ă�
        Debug.Log("�W�����v�{�^������");
    }

    //�ڐG�J�n
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Goal")
        {
            Goal(); //�S�[��
        }
        else if(collision.gameObject.tag=="Dead")
        {
            GameOver(); //�Q�[���I�[�o�[
        }
        else if(collision.gameObject.tag=="ScoreItem")
        {
            //�X�R�A�A�C�e��
            //ItemData�𓾂�
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            //�X�R�A�𓾂�
            score = item.value;

            //�A�C�e�����폜����
            Destroy(collision.gameObject);
        }
    }

    //�S�[��
    public void Goal()
    {
        animator.Play(goalAnime);

        gameState = "gameclear";
        GameStop();  //�Q�[����~
    }

    //�Q�[���I�[�o�[
    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "gameover";
        GameStop(); //�Q�[����~

        //�Q�[���I�[�o�[���o
        //�v���C���[�����������
        GetComponent<CapsuleCollider2D>().enabled = false;
        //�v���C���[����ɏ������ˏグ�鉉�o
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    //�Q�[����~
    void GameStop()
    {
        //���W�b�h�{�f�B2D������Ă���
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        //���x���O�ɂ��ċ�����~
        rbody.velocity = new Vector2(0, 0);
    }
}
