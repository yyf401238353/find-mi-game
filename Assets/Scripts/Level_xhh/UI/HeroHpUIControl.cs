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

    }

    // Update is called once per frame
    void Update()
    {
        this.updateHp();
    }

    private void updateHp()
    {
        if (!this.heroObj)
        {
            this.heroObj = GameObject.FindObjectOfType<Hero>();
        }

        if (this.heroObj)
        {
            int nowHp = this.heroObj.getHeroHp();
            int beforeHp = this.hpPointArray.Count;
            if (nowHp > beforeHp)
            {
                int diff = nowHp - beforeHp;

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
                int diff = beforeHp - nowHp;

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
