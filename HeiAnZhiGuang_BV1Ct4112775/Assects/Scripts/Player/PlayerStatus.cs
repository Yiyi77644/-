using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroType
{
    Swordman,
    Magician,
    Common,
}

public class PlayerStatus : MonoBehaviour
{
    public HeroType heroType;

    /// <summary>
    /// 等级
    /// </summary>
    public int grade = 1;
    /// <summary>
    /// 血量
    /// </summary>
    public int hp = 100;
    /// <summary>
    /// 蓝量
    /// </summary>
    public int mp = 100;
    /// <summary>
    /// 金币数
    /// </summary>
    public int coin = 20;
    /// <summary>
    /// 攻击力
    /// </summary>
    public int attack = 20;
    /// <summary>
    /// 攻击加点数
    /// </summary>
    public int attackPlus = 0;
    /// <summary>
    /// 防御力
    /// </summary>
    public int def = 20;
    /// <summary>
    /// 防御加点数
    /// </summary>
    public int defPlus = 0;
    /// <summary>
    /// 速度
    /// </summary>
    public int speed = 20;
    /// <summary>
    /// 速度加点数
    /// </summary>
    public int speedPlus = 0;

    /// <summary>
    /// 剩余点数
    /// </summary>
    public int pointRemain = 0;

    /// <summary>
    /// 获取金币
    /// </summary>
    /// <param name="count">增加的金币数量</param>
    public void GetCoin(int count)
    {
        coin += count;
    }

    public bool GetPoint(int point = 1)
    {
        if (pointRemain >= point)
        {
            pointRemain -= point;
            return true;
        }
        return false;
    }

}
