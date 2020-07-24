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

    private float hurtTimer = 0.0f;
    public float untouchTime = 1.0f;

    bool isUntouch = false;
    bool isJump;
    bool isFall;
    int currentEnergy = 100;

    float deltaDistance = 0;
    Vector2 lastPosition;

    Rigidbody2D rigidbody2d;
    Animator animator;
    EnergyTextController EnergyText;

    public GameObject projectilePrefab;
    GameObject EndText;

    CinemachineVirtualCamera camera;
    CinemachineTransposer transposer;

    public AudioClip hurtSE;
    public AudioClip shotSE;
    public AudioClip deadSE;
    AudioSource audioSource;
    AudioSource BGM;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
        camera = GameObject.Find("/CM vcam1").GetComponent<CinemachineVirtualCamera>();
        transposer = camera.GetCinemachineComponent<CinemachineTransposer>();
        animator.SetBool("IsJump", false);
        animator.SetBool("IsFall", false);
        EndText = GameObject.Find("/UI/EndText");
        EndText.SetActive(false);
        BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // input controll
        if (currentEnergy == 0)
        {
            GameOver();
        }

        deltaDistance = Vector2.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        Vector3 cameraOffset = transposer.m_FollowOffset;
        if (deltaDistance == 0)
        {
           cameraOffset.x = Mathf.Clamp(cameraOffset.x + 0.02f, 0, 8f); ;
           transposer.m_FollowOffset = cameraOffset;
        } else if (cameraOffset.x > 5.0f && cameraOffset.x < 8.0f)
        {
           cameraOffset.x = Mathf.Clamp(cameraOffset.x - 0.02f, 5.0f, 8f);
           transposer.m_FollowOffset = cameraOffset;
        }

        Vector2 speedNow = rigidbody2d.velocity;
        if (isJump && speedNow.y < 0)
        {
            isFall = true;
            isJump = false;
        }
        if (Input.GetKeyDown(KeyCode.J) && Time.timeScale != 0)
        {
            Launch();
            ChangeEnergy(-3);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isFall && !isJump && Time.timeScale != 0)
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

        if(isUntouch)
        {
            hurtTimer += Time.deltaTime;
            if(hurtTimer >= untouchTime)
            {
                hurtTimer = 0;
                isUntouch = false;
            }
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
        if (!(amount < 0 && isUntouch))
        {
          currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        }
        if(amount<-3) {
            audioSource.PlayOneShot(hurtSE);
        }
        EnergyText = GameObject.Find("/UI/Text").GetComponent<EnergyTextController>();
        EnergyText.UpdatePercentage(currentEnergy);
    }

    public void BeUntouch()
    {
        isUntouch = true;
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.right * 0.5f, Quaternion.identity);

        HeroBulletController projectile = projectileObject.GetComponent<HeroBulletController>();
        audioSource.PlayOneShot(shotSE);
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
        BGM.Stop();
        animator.SetBool("IsDead", true);
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        audioSource.PlayOneShot(deadSE);
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
