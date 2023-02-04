using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//巡游模式摄像机控制
public class Fashe : MonoBehaviour
{

    public static Fashe Instance = null;

    private Vector3 dirVector3;
    private Vector3 rotaVector3;
    private float paramater = 0.1f;
    //旋转参数
    private float xspeed = -0.05f;
    private float yspeed = 0.1f;

    private float dis;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rotaVector3 = transform.localEulerAngles;
/*        dis = UIFuc.Instance.Dis;*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //旋转
        if (Input.GetMouseButton(3))
        {
            rotaVector3.y += Input.GetAxis("Horizontal") * yspeed;
            rotaVector3.x += Input.GetAxis("Vertical") * xspeed;
            transform.rotation = Quaternion.Euler(rotaVector3);
        }


            //移动
            dirVector3 = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift)) dirVector3.z = 3;
                else dirVector3.z = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.LeftShift)) dirVector3.z = -3;
                else dirVector3.z = -1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.LeftShift)) dirVector3.x = -3;
                else dirVector3.x = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftShift)) dirVector3.x = 3;
                else dirVector3.x = 1;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                if (Input.GetKey(KeyCode.LeftShift)) dirVector3.y = -3;
                else dirVector3.y = -1;
            }
            if (Input.GetKey(KeyCode.E))
            {
                if (Input.GetKey(KeyCode.LeftShift)) dirVector3.y = 3;
                else dirVector3.y = 1;
            }


        if (Input.GetKey(KeyCode.J))
        {
            this.transform.Rotate(0, 10 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.L))
        {
            this.transform.Rotate(0, -10 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.I))
        {
            this.transform.Rotate(10 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.K))
        {
            this.transform.Rotate(-10 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.U))
        {
            this.transform.Rotate(0, 0, 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.O))
        {
            this.transform.Rotate(0, 0, -10 * Time.deltaTime);
        }



        transform.Translate(dirVector3 * paramater, Space.Self);
        //限制摄像机范围
/*        transform.position = Vector3.ClampMagnitude(transform.position, dis);*/
    }
}