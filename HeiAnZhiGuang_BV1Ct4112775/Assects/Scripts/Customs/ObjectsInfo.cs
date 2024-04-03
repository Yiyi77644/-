using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品类型
/// </summary>
public enum ObjectType
{
    /// <summary>
    /// 药品
    /// </summary>
    Drug,
    /// <summary>
    /// 装备
    /// </summary>
    Equip,
    /// <summary>
    /// 材料，material
    /// </summary>
    Mat,
}

/// <summary>
/// 装备穿戴类型
/// </summary>
public enum DressType
{
    /// <summary>
    /// 头盔
    /// </summary>
    Headgear,
    /// <summary>
    /// 铠甲
    /// </summary>
    Armor,
    RightHand,
    LeftHand,
    Shoes,
    /// <summary>
    /// 配饰
    /// </summary>
    Accessory,

}

/// <summary>
/// 装备适用类型
/// </summary>
public enum ApplicationType
{
    /// <summary>
    /// 剑士
    /// </summary>
    Swordman,
    /// <summary>
    /// 魔法师
    /// </summary>
    Magician,
    /// <summary>
    /// 通用
    /// </summary>
    Common,
}

/// <summary>
/// 一个物品的信息（单个物品的信息）
/// </summary>
public class ObjectInfo
{
    /*在这里备注以下ObjectInfo.txt中各列的内容：
     * id，名称，icon名，类型，加血值，加蓝值，出售价，购买价*/

    /// <summary>
    /// 物品id
    /// </summary>
    public int id;
    /// <summary>
    /// 物品名称
    /// </summary>
    public string name;
    /// <summary>
    /// 资源名称（可在PC的文件管理器中查看的名称）
    /// </summary>
    public string iconName;
    /// <summary>
    /// 物品类型
    /// </summary>
    public ObjectType type;
    /// <summary>
    /// 血量加成
    /// </summary>
    public int hp;
    /// <summary>
    /// 蓝量加成，magic point
    /// </summary>
    public int mp;
    /// <summary>
    /// 出售价格
    /// </summary>
    public int priceSell;
    /// <summary>
    /// 购买价格
    /// </summary>
    public int priceBuy;

    /// <summary>
    /// 伤害值
    /// </summary>
    public int attack;
    /// <summary>
    /// 防御值
    /// </summary>
    public int defend;
    /// <summary>
    /// 移动速度
    /// </summary>
    public int speed;
    /// <summary>
    /// 装备类型
    /// </summary>
    public DressType dressType;
    /// <summary>
    /// 穿戴类型
    /// </summary>
    public ApplicationType applicationType;


}








public class ObjectsInfo : MonoBehaviour
{
    public static ObjectsInfo _instance;

    public TextAsset objectsInfoListText;
    /// <summary>
    /// 物品字典，用于存储单个物品的信息
    /// </summary>
    private Dictionary<int, ObjectInfo> objectInfoDict = new Dictionary<int, ObjectInfo>();

    private void Awake()
    {
        _instance = this;
        ReadInfo();
    }

    /// <summary>
    /// 读取“物品信息”文本文件的内容
    /// </summary>
    private void ReadInfo()
    {
        string text = objectsInfoListText.text;
        //按行取,将文本文件中的全部内容，按行分割开，然后用一个字符串数组存储每一行的内容
        string[] strArray = text.Split('\n');

        foreach (string str in strArray)
        {
            //将每一行（即strArray[i]）再次进行分割，得到单个物品的每一个属性
            string[] proArray = str.Split(',');
            //用于存取所有的物品信息
            ObjectInfo info = new ObjectInfo();

            int id = int.Parse(proArray[0]);
            string name = proArray[1];
            string iconName = proArray[2];
            //物品类型
            string strType = proArray[3];

            ObjectType type = ObjectType.Drug;
            switch (strType)
            {
                case "Drug":
                    type = ObjectType.Drug;
                    break;
                case "Equip":
                    type = ObjectType.Equip;
                    break;
                case "Mat":
                    type = ObjectType.Mat;
                    break;
            }

            info.id = id;
            info.name = name;
            info.iconName = iconName;
            info.type = type;

            if (type == ObjectType.Drug)
            {
                int hp = int.Parse(proArray[4]);
                int mp = int.Parse(proArray[5]);
                int priceSell = int.Parse(proArray[6]);
                int priceBuy = int.Parse(proArray[7]);

                info.hp = hp;
                info.mp = mp;
                info.priceSell = priceSell;
                info.priceBuy = priceBuy;
            }
            else if (type == ObjectType.Equip)
            {
                info.attack = int.Parse(proArray[4]);
                info.defend = int.Parse(proArray[5]);
                info.speed = int.Parse(proArray[6]);

                info.priceSell = int.Parse(proArray[9]);
                info.priceBuy = int.Parse(proArray[10]);
                //穿戴类型
                string strDressType = proArray[7];
                switch (strDressType)
                {
                    case S_ItemName.Headgear:
                        info.dressType = DressType.Headgear;
                        break;
                    case S_ItemName.Armor:
                        info.dressType = DressType.Armor;
                        break;
                    case S_ItemName.LeftHand:
                        info.dressType = DressType.LeftHand;
                        break;
                    case S_ItemName.RightHand:
                        info.dressType = DressType.RightHand;
                        break;
                    case S_ItemName.Shoe:
                        info.dressType = DressType.Shoes;
                        break;
                    case S_ItemName.Accessory:
                        info.dressType = DressType.Accessory;
                        break;
                }
                //适用类型
                string strAppType = proArray[8];
                switch (strAppType)
                {
                    case S_ItemName.Swordman:
                        info.applicationType = ApplicationType.Swordman;
                        break;
                    case S_ItemName.Magician:
                        info.applicationType = ApplicationType.Magician;
                        break;
                    case S_ItemName.Common:
                        info.applicationType = ApplicationType.Common;
                        break;
                }

            }

            objectInfoDict.Add(id, info);

        }
    }

    /// <summary>
    /// 根据id得到单个物品的信息
    /// </summary>
    /// <param name="id">物品id</param>
    /// <returns>返回该物品的信息</returns>
    public ObjectInfo GetObjectInfoById(int id)
    {
        ObjectInfo info = null;
        objectInfoDict.TryGetValue(id, out info);

        return info;
    }


}

