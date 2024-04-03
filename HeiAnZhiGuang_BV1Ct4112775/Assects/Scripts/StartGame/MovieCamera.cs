using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 挂载在主摄像机上，用于移动摄像机，形成开场动画视角
public class MovieCamera : MonoBehaviour
{
    // 摄像机移动速度
    public float speed = 10;
    // 摄像机最后的位置在Z轴分量上的坐标
    private float endZ = -20;
    void Start()
    {

    }

    void Update()
    {
        if (transform.position.z < endZ)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
