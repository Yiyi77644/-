using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    /// <summary>
    /// 位置偏移。摄像头与角色之间的向量差。
    /// </summary>
    private Vector3 offsetPosition;
    /// <summary>
    /// 标志位，鼠标是否滑动
    /// </summary>
    private bool isRotating = false;



    /// <summary>
    /// 鼠标滚轮放缩视野
    /// </summary>
    public float distance = 0;
    /// <summary>
    /// 鼠标滚轮滑动速度
    /// </summary>
    public float scrollSpeed = 10;
    /// <summary>
    /// 
    /// </summary>
    public float rotateSpeed = 2;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(S_Tags.player).transform;
        transform.LookAt(player.position);
        offsetPosition = transform.position - player.position;
    }

    private void Update()
    {
        transform.position = offsetPosition + player.position;

        //旋转视野
        RotateView();
        //处理视野的拉近拉远
        ScrollView();
    }

    /// <summary>
    /// 处理视野的拉近拉远
    /// </summary>
    private void ScrollView()
    {
        //print(Input.GetAxis("Mouse ScrollWheel"));
        //向前滑动拉远，返回正值
        distance = offsetPosition.magnitude;
        distance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        //Mathf.Clamp()方法将一个变量限制在某两个值之间。
        distance = Mathf.Clamp(distance, 2, 18);
        offsetPosition = offsetPosition.normalized * distance;
    }

    /// <summary>
    /// 旋转视野
    /// </summary>
    private void RotateView()
    {
        //得到鼠标在水平方向上的滑动
        //Input.GetAxis("Mouse X");
        //得到鼠标在垂直方向上的滑动
        //Input.GetAxis("Mouse Y");
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }
        if (isRotating)
        {
            transform.RotateAround(player.position, player.up, rotateSpeed * Input.GetAxis("Mouse X"));

            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;
            //影响有两个：一个是position，一个是rotation
            transform.RotateAround(player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
            float x = transform.eulerAngles.x;
            //当超出范围之后，将属性归为原来的，即让旋转无效
            if (x < 10 || x > 80)
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
        }

        offsetPosition = transform.position - player.position;
    }
}
