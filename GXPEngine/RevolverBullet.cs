﻿using System;
using GXPEngine;
using GXPEngine.Core;

public class RevolverBullet : Sprite
{
    private Collision coll_info = null;
    public float x_speed { get; set; }
    public float y_speed { get; set; }

    // GRAVITY variables for ALIEN "bullets" (acid), should differentiate (move to different class/subclass)
    private float pull_force;
    private float seconds_after_spawn;
    public RevolverBullet(float newX, float newY) : base("bullet.png")
    {
        SetOrigin(width / 2, height / 2);
        x = newX;
        y = newY;
    }

    private void handleCollisions()     // what a surprise, OnCollision does not work!!
    {
        if (coll_info != null)
        {
            LateDestroy();
            //if (coll_info.other is HumanPlayer == false) LateDestroy();
            if (coll_info.other is AlienPlayer) LateDestroy();
            //if (coll_info.other is HumanPlayer) (other as HumanPlayer).HP--;
            //if (coll_info.other is AlienPlayer) (other as AlienPlayer).HP--;
        }
    }

    protected void ApplyGravityUntilFloor()             // needs to be adapted (-0.0008f is too little in *this* case)
    {

        pull_force = (float)(-1.2f * (Math.Pow( seconds_after_spawn, 2)));
        coll_info = MoveUntilCollision(0, -pull_force);
    }

    private void Update()
    {
        seconds_after_spawn += Time.deltaTime / 1000f;
        Console.WriteLine(pull_force);
        coll_info = MoveUntilCollision(x_speed, y_speed);
        if (parent.parent is AlienPlayer) ApplyGravityUntilFloor();

        handleCollisions();
    }
}
