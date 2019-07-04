﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int rewardPoints;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 pathTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;

    private void Start()
    {
        Init();
        pathTarget = pointA.position;
    }

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }


    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }

        Movement();
    }

    public virtual void Movement()
    {
        if (pathTarget == pointA.position)
        {
            if (sprite.flipX == false)
            {
                sprite.flipX = true;
                sprite.transform.position = new Vector3(sprite.transform.position.x - sprite.sprite.bounds.size.x,
                        sprite.transform.position.y, sprite.transform.position.z);
            }
        }
        else
        {
            if (sprite.flipX)
            {
                sprite.flipX = false;
                sprite.transform.position = new Vector3(sprite.transform.position.x + sprite.sprite.bounds.size.x,
                        sprite.transform.position.y, sprite.transform.position.z);
            }
        }

        if (transform.position == pointA.transform.position)
        {
            pathTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.transform.position)
        {
            pathTarget = pointA.position;
            anim.SetTrigger("Idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, pathTarget, speed * Time.deltaTime);
    }
}
