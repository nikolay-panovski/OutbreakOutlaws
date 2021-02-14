using System;
using GXPEngine;
using GXPEngine.Core;

public class Player : AnimationSprite
{
    protected Collision coll_info = null;
    protected Vector2 direction = new Vector2(0, 0);
    public Pivot bullet_handler { get; set; }
    public int HP { get; set; }               // for HP
    //protected float bullet_cooldown;        // for shooting
    //protected float cooldown;               // for abilities
    //protected float shield_timer;           // for Energy Shield

    protected const float LINEAR_VELOCITY = -3.2f;     // bruteforced values that work kinda well
    protected const int MAX_AIR_FRAMES = 72;

    protected float pull_force;
    protected bool can_jump = false;
    protected int jump_frames;
    protected int air_frames;

    protected float x_speed = 1.2f;
    protected float y_speed = 1.2f;

    public Player(string filename, int columns, int rows) : base(filename, columns, rows)
    {
        SetOrigin(width / 2, height / 2);
    }

    protected void jumpHandle()
    {
        if (jump_frames < MAX_AIR_FRAMES && can_jump)
        {
            jump_frames++;
            coll_info = MoveUntilCollision(0, LINEAR_VELOCITY);
        }
    }

    protected void GetDirectionVector()
    {
        if (Input.GetKey(Key.A))
        {
            direction.x = -1;
            if (!(Input.GetKey(Key.W)) && !(Input.GetKey(Key.S))) direction.y = 0;      // remove conditions here for 4 instead of 8 directions
        }
        if (Input.GetKey(Key.D))
        {
            direction.x = 1;
            if (!(Input.GetKey(Key.W)) && !(Input.GetKey(Key.S))) direction.y = 0;
        }
        if (Input.GetKey(Key.W))
        {
            direction.y = -1;
            if (!(Input.GetKey(Key.A)) && !(Input.GetKey(Key.D))) direction.x = 0;
        }
        if (Input.GetKey(Key.S))
        {
            direction.y = 1;
            if (!(Input.GetKey(Key.A)) && !(Input.GetKey(Key.D))) direction.x = 0;
        }

        // for ALIEN, DO NOT KEEP HERE!!!
        if (Input.GetKey(Key.LEFT))
        {
            direction.x = -1;
            if (!(Input.GetKey(Key.UP)) && !(Input.GetKey(Key.DOWN))) direction.y = 0;      // remove conditions here for 4 instead of 8 directions
        }
        if (Input.GetKey(Key.RIGHT))
        {
            direction.x = 1;
            if (!(Input.GetKey(Key.UP)) && !(Input.GetKey(Key.DOWN))) direction.y = 0;
        }
        if (Input.GetKey(Key.UP))
        {
            direction.y = -1;
            if (!(Input.GetKey(Key.LEFT)) && !(Input.GetKey(Key.RIGHT))) direction.x = 0;
        }
        if (Input.GetKey(Key.DOWN))
        {
            direction.y = 1;
            if (!(Input.GetKey(Key.LEFT)) && !(Input.GetKey(Key.RIGHT))) direction.x = 0;
        }
    }

    protected void ApplyGravityUntilFloor()         // why not just move this back to the separate classes
    {
        RestoreJumpsOnFloor();

        pull_force = (float)(-0.0008f * (Math.Pow(air_frames, 2)));
        if (air_frames < MAX_AIR_FRAMES) air_frames++;
        if (coll_info == null) coll_info = MoveUntilCollision(0, -pull_force);     // !!!!
    }

    protected void RestoreJumpsOnFloor()
    {
        if (coll_info != null)
        {
            if (coll_info.normal.y == -1)       // that should mean the player is landing/standing on a floor
            {
                /*jumps_left = max_jumps_left;*/
                jump_frames = 0;
                air_frames = 0;
                can_jump = true;
            }
        }
    }

    protected void HandleCollisions()       // it just doesn't detect collisions!
    {
        if (coll_info != null)
        {
            if (coll_info.other is PickupCoin)
            {
                coll_info.other.LateDestroy();
            }
            if (coll_info.other is PickupHeart)
            {
                coll_info.other.LateDestroy();
                HP++;
                Console.WriteLine(HP);
            }
            if (coll_info.other is PickupShield)
            {
                coll_info.other.LateDestroy();
            }
        }
    }

    /*protected void Update()
    {
        
    }*/
}