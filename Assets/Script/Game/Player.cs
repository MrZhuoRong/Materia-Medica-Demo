using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static bool canMove = true;

    private Rigidbody2D rb;//获得人物刚体 用于控制运动
    public Animator animator;

    Vector2 movement;
    private float moveH, moveV;
    public float moveSpeed = 1.0f;

    public bool isPicked = true;
    private bag bagObject;
    private patient patients;

    public GameObject DiaLogBox;

    public int maxHealth = 10;
    public int currentHealth;
    public HP healthBar;

    public GameObject gameui;
    public GameObject player;

    private FaceBookController faceBookController;

    public float timer = 1.0f;
    private int Player_condition = 0;//0为健康 1为中毒

    int num = 0;

    [Header("Gather Type Task")]
    public int amount = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();

        GameObject gameObject = GameObject.Find("bagSystem");
        bagObject = gameObject.GetComponent<bag>();

        GameObject Object = GameObject.Find("FaceBookSystem");
        faceBookController = Object.GetComponent<FaceBookController>();

        GameObject patientObject= GameObject.Find("Patient");
        patients = patientObject.GetComponent<patient>();

        player.SetActive(true);
        gameui.SetActive(true);
        DiaLogBox.SetActive(false);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        Player_Move();
        HandleHealth();
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveH, moveV);
    }

    private void Player_Move()
    {
        if (canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            moveH = movement.x * moveSpeed;
            moveV = movement.y * moveSpeed;

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }
    private void OnTriggerStay2D(Collider2D collision) //碰撞响应
    {
        //如果按下按键且该碰撞物体挂载该脚本
        if (Input.GetKey(KeyCode.F) == true && collision.GetComponent<Item_picked>() && collision.GetComponent<TaskTarge>())
        {

            if (collision.gameObject.tag == "DUPIN" || collision.gameObject.tag == "Medicine")
            {

                if (bagObject.pickItem(collision.GetComponent<Item_picked>()))  //调用存进背包
                {
                    for (int i = 0; i < PlayerTask.instance.taskList.Count; i++)
                    {
                        if (collision.GetComponent<TaskTarge>().taskName == PlayerTask.instance.taskList[i].taskName)
                        {
                            PlayerTask.instance.itemAmount += amount;
                            collision.GetComponent<TaskTarge>().TaskComplete();
                        }
                    }
                    Destroy(collision.gameObject);
                } 
            }
            else
            {
                //背包大小不足提示
            }
        }

        if(Input.GetKeyUp(KeyCode.F) == true&& collision.gameObject.tag == "Patient")
        {
            patients.TaskAccess();
        }


    }

        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DiaLogBox.SetActive(true);
        if (collision.gameObject.tag == "DUPIN")
        {
            Player_condition = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DiaLogBox.SetActive(false);
    }

    void TakeDamage(int damage)
    {
        
        currentHealth -= damage;
        num+= damage;
        
    }

    public void HandleHealth()
    {
        
        if (Player_condition == 1)
        {
            
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                TakeDamage(1);
                timer = 1.0f;
            }
            if(num==10)
            {
                Player_condition = 0;
                num = 0;
            }
        }
        if (currentHealth == 0)
        {
            canMove = false;
            Player_condition = 0;
            //gameOver.HandleGameOver();
            gameui.SetActive(false);
            player.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
        healthBar.SetHPtext(currentHealth, maxHealth);
    }

}
