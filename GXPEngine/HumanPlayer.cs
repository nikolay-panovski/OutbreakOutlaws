using System;
using System.Collections.Generic;
using GXPEngine;
using GXPEngine.Core;

public class HumanPlayer : Player
{
    public int ammo { get; set; }


    private Collision coll_info = null;
    public AlienPlayer other_player { get; set; }

    public HumanPlayer(string filename, int columns, int rows) : base(filename, columns, rows)
    {
        HP = 2;
        ammo = 12;
    }

    private void movementHandle()
    {
        if (Input.GetKey(Key.A)) /*Move(-x_speed, 0);*/  coll_info = MoveUntilCollision(-x_speed, 0);     // NOPE, because another MUC pulls
        if (Input.GetKey(Key.D)) /*Move(x_speed, 0);*/   coll_info = MoveUntilCollision(x_speed, 0);      // downwards (gravity)

        if (Input.GetKeyUp(Key.W)) can_jump = false;
        if (Input.GetKey(Key.W)) jumpHandle();
    }

    private void spawnBullet()
    {
        RevolverBullet bullet = new RevolverBullet(this.x + this.width * direction.x, this.y + this.height * direction.y);
        bullet_handler.AddChild(bullet);

        // patchwork, please don't leave as final if possible
        // however, this should be able to be reimplemented for alien / grappling hook
        // making to different function will take some untangling, as this operates both with a bullet and the player here
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

    //private void cameraCheck()
    //{
    /* Response by Bram den Hond:
     * I would make the scale directly related to the distance between the players:

     * float scale = 600f / player1.DistanceTo(player2); //note: you'd need to avoid division by zero error here.
     * scale = Mathf.Clamp(scale, 0.5f, 1.5f); //note: example values

     * On a sidenote, in any system, to reduce jittering, you can smooth out a transition by doing this:
     * currentValue = currentValue * 0.9f + newValue * 0.1f;
     * This will cause a small amount of lag, but it will reduce jittering.
     */

    /*if (other_player.x - other_player.width  / 2 < this.x - (viewport._renderTarget.width  / 2) * viewport.scale ||
        other_player.x + other_player.width  / 2 > this.x + (viewport._renderTarget.width  / 2) * viewport.scale ||
        other_player.y - other_player.height / 2 < this.y - (viewport._renderTarget.height / 2) * viewport.scale ||
        other_player.y + other_player.height / 2 > this.y + (viewport._renderTarget.height / 2) * viewport.scale)
    {
        viewport.scale += 0.01f;
    }
    else
    {
        if (viewport.scale > 1f) viewport.scale -= 0.01f;
    }*/

    /*viewport.scale = game.width / DistanceTo(other_player);           // does not work well in my case.
    viewport.scale = Mathf.Clamp(viewport.scale, 0.1f, 1.0f);*/
    //}

    /*private bool MoveAndCollide(float deltaX, float deltaY, List<Sprite> colliders)
    {

        x += deltaX;
        y += deltaY;

        bool isColliding = false;
        foreach (Sprite other in colliders)
        {
            if (other is FloorTile)
            {
                resolveCollision(this, other, deltaX, deltaY);
                isColliding = true;
            }
        }

        return !isColliding;

    }*/


    /*private void resolveCollision(Sprite subject, Sprite collider, float deltaX, float deltaY)
    {
        if (deltaX < 0) subject.x = collider.x + collider.width;
        if (deltaX > 0) subject.x = collider.x - subject.width;
        if (deltaY < 0) subject.y = collider.y + collider.height;
        if (deltaY > 0) subject.y = collider.y - subject.height;
    }*/

    /*private void OnCollision(GameObject other)
    {
        if (coll_info != null)
        {
            if (coll_info.normal.y == 1)        // hit ceiling (does not run with MoveUntilCollision...)
            {
                Console.WriteLine(jump_frames);
                jump_frames = MAX_JUMP_FRAMES;
            }
        }
    }*/

    private void Update()
    {
        GetDirectionVector();
        if (Input.GetKeyDown(Key.G)) spawnBullet();

        HandleCollisions();
        movementHandle();
        ApplyGravityUntilFloor();
        //cameraCheck();
    }
}
