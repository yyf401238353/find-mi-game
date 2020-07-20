using UnityEngine;
using System.Collections;

public class HeroParticlesControl : MonoBehaviour
{
    public GameObject DoubleJumpPrefab;

    public Transform BottomPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void doubleJump()
    {
        GameObject gameObject = Instantiate(DoubleJumpPrefab, this.transform.position, this.transform.rotation);
        Destroy(gameObject, 0.5f);
    }

    public void setHeroStatus(Hero.Status value)
    {
        if (value == Hero.Status.DOUBLE_JUMP_UP)
        {
            this.doubleJump();
        }
    }
}
