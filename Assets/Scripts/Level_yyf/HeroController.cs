using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;

public class HeroController : MonoBehaviour
{
    public float jumpSpeed = 8.0f;
    public int maxEnergy = 100;

    private float waitTime = 0.5f;
    private float timer = 0.0f;
    bool isJump;
    bool isFall;
    int currentEnergy = 100;

    Rigidbody2D rigidbody2d;
    Animator animator;
    EnergyTextController EnergyText;

    public GameObject projectilePrefab;
    GameObject EndText;

    CinemachineVirtualCamera camera;
    CinemachineTransposer transposer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        camera = GameObject.Find("/CM vcam1").GetComponent<CinemachineVirtualCamera>();
        transposer = camera.GetCinemachineComponent<CinemachineTransposer>();
        Debug.Log(transposer.m_FollowOffset);
        animator.SetBool("IsJump", false);
        animator.SetBool("IsFall", false);
        EndText = GameObject.Find("/UI/EndText");
        EndText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // input controll
        if (currentEnergy == 0)
        {
            GameOver();
        }

        Vector2 speedNow = rigidbody2d.velocity;
        if (isJump && speedNow.y < 0)
        {
            isFall = true;
            isJump = false;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Launch();
            ChangeEnergy(-3);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isFall && !isJump)
        {
            speedNow.y = jumpSpeed;
            isJump = true;
        }
        speedNow.x = 3.0f;
        rigidbody2d.velocity = speedNow;
        animator.SetBool("IsJump", isJump);
        animator.SetBool("IsFall", isFall);

        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            // Remove the recorded 0.5 seconds.
            timer = timer - waitTime;
            ChangeEnergy(-1);
        }
        if (Input.GetKey(KeyCode.R) && Time.timeScale == 0)
        {
            ResetScene();
        } else if (Input.GetKey(KeyCode.Escape) && Time.timeScale == 0)
        {
            ReturnHomePage();
        }
    }
    public void StopYSpeed ()
    {
        Vector2 speedNow = rigidbody2d.velocity;
        isFall = false;
        isJump = false;
        animator.SetBool("IsJump", isJump);
        animator.SetBool("IsFall", isFall);
        rigidbody2d.velocity = speedNow;
    }
    public void ChangeEnergy(int amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        EnergyText = GameObject.Find("/UI/Text").GetComponent<EnergyTextController>();
        EnergyText.UpdatePercentage(currentEnergy);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.right * 0.5f, Quaternion.identity);

        HeroBulletController projectile = projectileObject.GetComponent<HeroBulletController>();
        projectile.Launch();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
    }

    public void ReturnHomePage()
    {
        SceneManager.LoadScene("StartScene");
        ResumeGame();
    }
    public void GameOver()
    {
        PauseGame();
        EndText.SetActive(true);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
