using System;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
public class AlienPlayer : Player
{
    public HumanPlayer other_player { get; set; }

    public AlienPlayer(string filename, int columns, int rows) : base(filename, columns, rows)
    {
        HP = 3;
    }

    private void movementHandle()
    {
        if (Input.GetKey(Key.LEFT) ) x -= x_speed;  // MoveUntilCollision(-x_speed, 0);
        if (Input.GetKey(Key.RIGHT)) x += x_speed;  // MoveUntilCollision(x_speed, 0);

        if (Input.GetKeyDown(Key.UP))   // for infinite jumps
        {
            jump_frames = 0;
            air_frames = 0;
        }
        if (Input.GetKey(Key.UP)) jumpHandle();
    }


    // TEST for now, move/delete later
    // currently because of this test, both characters shoot bullets affected by gravity
    private void spawnBullet()
    {
        RevolverBullet bullet = new RevolverBullet(this.x + this.width * direction.x, this.y + this.height * direction.y);
        bullet_handler.AddChild(bullet);

        if (direction.x == 1)
        {
            if (direction.y == 0) bullet.rotation = 0;
            else if (direction.y == -1) bullet.rotation = 135;
            else if (direction.y == 1) bullet.rotation = 225;
        }
        else if (direction.x == 0)
        {
            if (direction.y == -1) bullet.rotation = 90;
            else if (direction.y == 1) bullet.rotation = 270;
        }
        else if (direction.x == -1)
        {
            if (direction.y == 0) bullet.rotation = 180;
            else if (direction.y == -1) bullet.rotation = 45;
            else if (direction.y == 1) bullet.rotation = 315;
        }

        bullet.x_speed = 1.4f * direction.x;
        bullet.y_speed = 1.4f * direction.y;
    }

    private void Update()
    {
        GetDirectionVector();
        if (Input.GetKeyDown(Key.NUMPAD_4)) spawnBullet();

        HandleCollisions();
        movementHandle();
        ApplyGravityUntilFloor();
    }
}
