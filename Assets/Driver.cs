using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    float moveSpeed;
    [SerializeField] float steerSpeed = 100;
    [SerializeField] float defaultSpeed = 15;
    [SerializeField] float bumpSpeed = 7.5f;
    [SerializeField] float boostSpeed = 25;
    float speedUpStartTime;
    float slowDownStartTime;

    private void Start()
    {
        speedUpStartTime = Time.fixedTime - 100;
        slowDownStartTime = Time.fixedTime - 100;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Boost"))
        {
            speedUpStartTime = Time.fixedTime;
            Debug.Log("Speed up!");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        slowDownStartTime = Time.fixedTime;
        Debug.Log("Hit something!");
    }

    void Update()
    {
        if(Time.fixedTime - speedUpStartTime < 2)
        {
            moveSpeed = boostSpeed;
        }

        else if(Time.fixedTime - slowDownStartTime < 0.75f)
        {
            moveSpeed = bumpSpeed;
        }

        else
        {
            moveSpeed = defaultSpeed;
        }
        
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float speedAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, speedAmount, 0);
    }

}
