using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHpUIControl : MonoBehaviour
{
    [Header("一个Hp血量对象")]
    public GameObject OneHpPoint;
    [Header("Hp血量的尺寸")]
    public float HpPointSize;

    private Hero heroObj;
    private List<GameObject> hpPointArray = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        this.heroObj = GameObject.FindObjectOfType<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        this.updateHp();
    }

    private void updateHp()
    {
        // 并没有寻找到英雄，则英雄死亡，销毁HP
        if (!this.heroObj)
        {
            Destroy(this.gameObject);
        }
        else
        {
            int nowHp = this.heroObj.getHeroHp();
            int beforeHp = this.hpPointArray.Count;
            if (nowHp > beforeHp)
            {
                for (int index = beforeHp + 1; index <= nowHp; index++)
                {
                    float xPos = (index - 0.5f) * this.HpPointSize;
                    GameObject hpPoint = Instantiate(this.OneHpPoint, this.transform);
                    hpPoint.transform.localPosition = new Vector3(xPos, 0, 0);
                    this.hpPointArray.Add(hpPoint);
                }
            }
            else if (beforeHp > nowHp)
            {
                for (int index = beforeHp - 1; index >= nowHp; index--)
                {
                    GameObject removeObj = this.hpPointArray[index];
                    this.hpPointArray.RemoveAt(index);
                    Destroy(removeObj);
                }
            }
        }
    }

}
