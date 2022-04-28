using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float length = 0.0f;     //�����������m����
    public bool isDelete = false;   //������ɍ폜����t���O

    bool isFell = false;            //�����t���O
    float fadeTime = 0.5f;          //�t�F�[�h�A�E�g����

    // Start is called before the first frame update
    void Start()
    {
        //RigidBody2D�̕����������~
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //�v���C���[��T��
        if(player!=null)
        {
            //�v���C���[�Ƃ̋����v��
            float d = Vector2.Distance(transform.position, player.transform.position);

            if(length>=d)
            {
                Rigidbody2D rbody = GetComponent<Rigidbody2D>();

                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    //RigidBody2D�̕����������J�n
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
        if(isFell)
        {
            //��������
            //�����n��ύX���ăt�F�[�h�A�E�g�w����
            fadeTime -= Time.deltaTime; //�O�t���[���̍����b�}�C�i�X
            Color col = GetComponent<SpriteRenderer>().color;//�J���[�����o��
            col.a = fadeTime;   //�����n��ύX
            GetComponent<SpriteRenderer>().color = col;     //�J���[���Đݒ肷��

            if(fadeTime<=0.0f)
            {
                //0�ȉ��i�����j�ɂȂ��������
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    //�ڐG�J�n
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isDelete)
        {
            isFell = true;//�����t���O�I��
        }
    }
}
