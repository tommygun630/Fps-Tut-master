﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI06 : MonoBehaviour {

    public float walkMoveForce = 0f;
    public float runMoveForce = 0f;
    public float moveForce = 0f;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsObstacle;
    private Rigidbody rbody;
    public float wallCheckDist = 0f;
    private Vector3 moveDir;
    private bool TargetFound;

    public float ShootRate = 0f;
    private float shootTimeStamp = 0f;
    public GameObject playerObject;
    public float distanceFromTarget = 0f;
    public float safeDistance = 0f;
    private bool shotFired = false;

    public float runTurnRate = 0f;
    private float runTurnTimeStamp = 0f;
    public float runTurnDistCheck = 0f;

    // Use this for initialization
    void Start() {

        rbody = GetComponent<Rigidbody>();
        moveDir = ChooseDirection();
        transform.rotation = Quaternion.LookRotation(moveDir);
        moveForce = walkMoveForce;
        runTurnRate = Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update() {

        if (TargetFound)
        {
            Shoot();
        }
        else
        {
            LookFortarget();
        }

        distanceFromTarget = Vector3.Distance(transform.position, playerObject.transform.position);
    }
        void Shoot()
        {
            if (!shotFired)
            {
                if (Time.time > shootTimeStamp)
                {
                    moveForce = runMoveForce;
                    shotFired = true;
                    moveDir = ChooseDirection();
                    transform.rotation = Quaternion.LookRotation(moveDir);
                    shootTimeStamp = Time.time + ShootRate;
                }

            }
            else
            {
                Hide();
            }
        }

        void Hide()
        {
            if (distanceFromTarget < safeDistance)
            {
                RunToHide();
            }
            else
            {
                moveForce = walkMoveForce;
                shotFired = false;
                TargetFound = false;
                moveDir = ChooseDirection();
                transform.rotation = Quaternion.LookRotation(moveDir);
            }
        }

        void RunToHide()
        {
            rbody.velocity = moveDir * moveForce;

            if (Time.time > runTurnTimeStamp)
            {
                if (Physics.Raycast(transform.position, transform.right, runTurnDistCheck, whatIsObstacle))
                {
                    moveDir = -transform.right;
                    transform.rotation = Quaternion.LookRotation(moveDir);
                }
                else if (Physics.Raycast(transform.position, -transform.right, runTurnDistCheck, whatIsObstacle))
                {
                    moveDir = transform.right;
                    transform.rotation = Quaternion.LookRotation(moveDir);
                }
                else
                {
                    moveDir = ChooseDirectionLR();
                    transform.rotation = Quaternion.LookRotation(moveDir);
                }
                runTurnRate = Random.Range(0.5f, 1.5f);
                runTurnTimeStamp = Time.time + runTurnRate;

            }
        }
   

    Vector3 ChooseDirectionLR()
    {
        System.Random ran = new System.Random();
        int i = ran.Next(0, 1);
        Vector3 temp = new Vector3();

        if (i == 0)
        {
            temp = transform.right;
        } else if (i == 1)
        {
            temp = -transform.right;
        }

        return temp;
    }

    Vector3 ChooseDirection()
    {
        System.Random ran = new System.Random();
        int i = ran.Next(0, 3);
        Vector3 temp = new Vector3();

        if (i == 0)
        {
            temp = transform.forward;
        }
        else if (i == 1)
        {
            temp = -transform.forward;
        }
        else if (i == 2)
        {
            temp = transform.right;
        }
        else if (i == 3)
        {
            temp = -transform.right;
        }

        return temp;
    }


    void LookFortarget()
    {
        Move();
        TargetFound = Physics.Raycast(transform.position, transform.forward, Mathf.Infinity, whatIsPlayer);
    }

    void Move()
    {
    rbody.velocity = moveDir * moveForce;

    if (Physics.Raycast(transform.position, transform.forward, wallCheckDist, whatIsObstacle))
     {
       moveDir = ChooseDirection();
       transform.rotation = Quaternion.LookRotation(moveDir);
     }

    }
    
}
