using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;    //����������prefab�f�[�^
    public float delayTime = 3.0f;  //�x������
    public float fireSpeedX = -4f;   //���˃x�N�g��X
    public float fireSpeedY = 0.0f; //���˃x�N�g��Y
    public float length = 8.0f;

    GameObject player;      //�v���C���[
    GameObject gateObj;     //���ˌ�
    float passedTimes = 0;  //�o�ߎ���

    // Start is called before the first frame update
    void Start()
    {
        //���ˌ��I�u�W�F�N�g���擾
        Transform tr = transform.Find("gate");
        gateObj = tr.gameObject;
        //�v���C���[
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԏ��Ԕ���
        passedTimes += Time.deltaTime;
        //�����`�F�b�N
        if(CheckLength(player.transform.position))
        {
            if (passedTimes > delayTime)
            {
                //����
                passedTimes = 0;
                //���ˈʒu
                Vector3 pos = new Vector3(gateObj.transform.position.x, gateObj.transform.position.y, transform.position.z);

                //prefab����GameObject������
                GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
                //���˕���
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                Vector2 v = new Vector2(fireSpeedX, fireSpeedY);
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }

    //�����`�F�b�N
    bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if(length>=d)
        {
            ret = true;
        }
        return ret;
    }
}
