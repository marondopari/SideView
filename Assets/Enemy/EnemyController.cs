using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;  //�ړ����x
    public string direction = "left";   //�����@right or left
    public float range = 0.0f;  //�������͈�
    Vector3 defPos;     //�����ʒu

    // Start is called before the first frame update
    void Start()
    {
        if(direction=="right")
        {
            transform.localScale = new Vector2(-1, 1);  //�����̕ύX
        }

        //�����ʒu
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(range>0.0f)
        {
            if(transform.position.x<defPos.x-(range/2))
            {
                direction = "right";
                transform.localScale = new Vector2(-1, 1);  //�����̕ύX
            }
            if(transform.position.x>defPos.x+(range/2))
            {
                direction = "left";
                transform.localScale = new Vector2(1, 1);   //�����̕ύX
            }
        }
    }

    void FixedUpdate()
    {
        //���x���X�V����
        //RigidBody2D������Ă���
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        if(direction=="right")
        {
            rbody.velocity = new Vector2(speed, rbody.velocity.y);
        }
        else
        {
            rbody.velocity = new Vector2(-speed, rbody.velocity.y);
        }
    }

    //�ڐG
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(direction=="right")
        {
            direction = "left";
        }
        else
        {
            direction = "right";
            transform.localScale = new Vector2(-1, 1);  //�����̕ύX
        }
    }
}
