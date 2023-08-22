using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float forwardSpeed;
    private Touch touch;
    private float speedMofifier;
    public bool canMove = false;
    Animator playerAnim;
    public GameObject[] players;
    public PlatformGeneration platformGeneration;
    public CurrencyManager currencyManager;
    public float timeDecrease;
    public Image countDownImage;
    float smooth = 5.0f;
    float tiltAngle = 45.0f;
    bool isSlow = false;
    public Rigidbody rb;
    public float jumpForce;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        speedMofifier = 0.01f;
        playerAnim = players[0].GetComponent<Animator>();
        countDownImage.fillAmount = 1;
    }
    private void Update()
    {
        // Clamp For Player
        if (canMove)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.5f, 2.5f), transform.position.y, transform.position.z + forwardSpeed * Time.deltaTime);
            playerAnim.SetBool("IsRunning", true);
        }

#region Keyboard Controls
        //Keyboard Controls
        if (Input.GetKey(KeyCode.RightArrow) && canMove)
        {
            transform.Translate(0.05f, 0f, 0f);
            playerAnim.SetBool("IsRunning", true);
            //Quaternion target = Quaternion.Euler(0, tiltAngle, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        }
        else
        {
            //Quaternion target = Quaternion.Euler(0, 0, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && canMove)
        {
            transform.Translate(-0.05f, 0f, 0f);
            playerAnim.SetBool("IsRunning", true);
            //Quaternion target = Quaternion.Euler(0, -tiltAngle, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        }
        else
        {
            //Quaternion target = Quaternion.Euler(0, 0, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        }
        if (Input.GetKey(KeyCode.Space) && canMove)
        {
            if (isSlow == false)
            {
                timeDecrease -= Time.deltaTime;
                countDownImage.fillAmount = timeDecrease / 2f;
                forwardSpeed = 1f;
                playerAnim.speed = 0.5f;
                foreach (GameObject item in platformGeneration.obstaclesTag)
                {
                    item.GetComponent<Animator>().speed = 0.3f;
                }
            }

            if (timeDecrease <= 0)
            {
                countDownImage.fillAmount = 1;
                forwardSpeed = 4f;
                playerAnim.speed = 1f;
                foreach (GameObject item in platformGeneration.obstaclesTag)
                {
                    item.GetComponent<Animator>().speed = 1f;
                }
                timeDecrease = 2f;
                isSlow = true;
            }
        }
        else
        {
            forwardSpeed = 4f;
            playerAnim.speed = 1f;
            countDownImage.fillAmount = 1;
            isSlow = false;
            if (canMove)
            {
                foreach (GameObject item in platformGeneration.obstaclesTag)
                {
                    item.GetComponent<Animator>().speed = 1f;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

#endregion
#region Touch Controls
        //Touch Controls
        if (Input.touchCount > 0 && canMove)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                playerAnim.SetBool("IsRunning", true);
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedMofifier,transform.position.y, transform.position.z);

                if (touch.deltaPosition.x > 0)
                {
                    Quaternion target = Quaternion.Euler(0, tiltAngle, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
                }
                if (touch.deltaPosition.x < 0)
                {
                    Quaternion target = Quaternion.Euler(0, -tiltAngle, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
                }
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                if (timeDecrease <= 0)
                {
                    countDownImage.fillAmount = 1;
                    forwardSpeed = 4f;
                    playerAnim.speed = 1f;
                    if (canMove)
                    {
                        foreach (GameObject item in platformGeneration.obstaclesTag)
                        {
                            item.GetComponent<Animator>().speed = 1f;
                        }
                    }
                }
                else
                {
                    timeDecrease -= Time.deltaTime;
                    countDownImage.fillAmount = timeDecrease / 2f;
                    forwardSpeed = 1f;
                    playerAnim.speed = 0.5f;
                    if (canMove)
                    {
                        foreach (GameObject item in platformGeneration.obstaclesTag)
                        {
                            item.GetComponent<Animator>().speed = 0.3f;
                        }
                    }
                }
            }
            else
            {
                forwardSpeed = 4f;
                playerAnim.speed = 1f;
                countDownImage.fillAmount = 1;
                if (canMove)
                {
                    foreach (GameObject item in platformGeneration.obstaclesTag)
                    {
                        item.GetComponent<Animator>().speed = 1f;
                    }
                }
                timeDecrease = 2f;
            }
        }
        else
        {
            if (canMove)
            {
                Quaternion target = Quaternion.Euler(0, 0, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }
        }
            #endregion
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            currencyManager.AddCoins(1);
            Destroy(other.gameObject);
        }
        if (other.tag == "LastPlatform")
        {
            LevelComplete();
        }
        if (other.tag == "Enemy")
        {
            LevelFail();
        }
    }
    private void Jump()
    {
        // Add jump logic here, e.g., apply a force to the Rigidbody
        Debug.Log("jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void LevelFail()
    {
        UIManager.instance.loosePanel.SetActive(true);
        transform.position = Vector3.zero;
        canMove = false;
        playerAnim.SetBool("IsRunning", false);
        platformGeneration.DeletePlatforms();
        UIManager.instance.gamePanel.SetActive(false);
        currencyManager.levelFailedTotalCoinsText.text = currencyManager.TempCoins.ToString();
    }

    public void LevelComplete()
    {
        UIManager.instance.winPanel.SetActive(true);
        transform.position = Vector3.zero;
        canMove = false;
        playerAnim.SetBool("IsRunning", false);
        platformGeneration.DeletePlatforms();
        UIManager.instance.gamePanel.SetActive(false);
        currencyManager.levelCompleteCoinText.text = currencyManager.TempCoins.ToString();
    }
}
