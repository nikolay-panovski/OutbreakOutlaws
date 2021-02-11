using System;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class Player : AnimationSprite
{
    protected Collision coll_info = null;
    protected int HP;                       // for HP
    protected float bullet_cooldown;        // for shooting
    //protected float cooldown;               // for abilities
    protected float shield_timer;           // for Energy Shield

    protected const float MAX_VELOCITY = -3.2f;     // bruteforced values that work kinda well
    protected const int MAX_JUMP_FRAMES = 72;

    protected float velocity;
    protected float pull_force;
    protected int jumps_left;
    protected int jump_frames;
    protected int air_frames;

    protected float x_speed = 1.2f;         // variable values here remain for polishing unless big complaints happen
    protected float y_speed = 1.2f;

    public Player(string filename, int columns, int rows, TiledObject obj) : base(filename, columns, rows)
    {
        /* common things between players:
         * have HP (communication with HUD)
         * have cooldowns on abilities (although different abilities)
         * can pick up objects (probably still put in the separate classes)
         * is affected by gravity (downward)
         */
    }

    protected void gravPullUntilFloor()         // why not just move this back to the separate classes
    {
        if (coll_info != null)
        {
            if (coll_info.normal.y == -1)       // that means the player is landing/standing on a floor
            {
                jumps_left = 3;
                jump_frames = 0;
                air_frames = 0;
            }
        }

        pull_force = (float)(-0.0008f * (Math.Pow(air_frames, 2)));       // welp, who cares about physics
                                                                          // coll_info nulls properly midair in this state
        if (air_frames < MAX_JUMP_FRAMES) air_frames++;
        coll_info = MoveUntilCollision(0, -pull_force);
    }

    protected void Update()
    {

    }
}