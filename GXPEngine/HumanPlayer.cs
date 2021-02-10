using System;
using GXPEngine;
using TiledMapParser;

public class HumanPlayer : Player
    /* only he can jump (double, even)
     * and dash (idk why)
     * shooting - bullet type, for gun see atan2, or now more likely facing direction (4 directions/keyboard only)
     */
{
    // abilities ideas at
    // https://docs.google.com/document/d/1LpYGEeC7lE1ZL_0scx-RWHnhFhplDFFnN32FGQSOIfE/edit?ts=60226506
    public int ammo { get; set; }

    private Camera viewport;       // basic camera works
    public AlienPlayer other_player { get; set; }

    public HumanPlayer(string filename, int columns, int rows, TiledObject obj) : base(filename, columns, rows, obj)
    {
        HP = 2;         // starting values, see google doc
        ammo = 6;
        viewport = new Camera(0, 0, (game as MyGame).width, (game as MyGame).height);
        AddChild(viewport);
    }

    private void movementHandle()
    {
        if (Input.GetKey(Key.A)) x -= x_speed;
        if (Input.GetKey(Key.D)) x += x_speed;

        if (Input.GetKeyDown(Key.W))
        {
            jumps_left--;
            jump_frames = 0;
            air_frames = 0;
        }
        if (Input.GetKey(Key.W)) jumpHandle();
    }

    private void jumpHandle()
    {
        /* within the jump:
         * when pressing up, apply some "velocity" and increase player height - linearly in this part
         * regulate the above linear part by always applying gravity,
         * however let the gravity force reset to 0 and gradually increase after a jump initiation
         */
        if (jump_frames < MAX_JUMP_FRAMES && jumps_left > 0)
        {
            jump_frames++;
            velocity = MAX_VELOCITY;
            coll_info = MoveUntilCollision(0, velocity);
        }
    }

    private void cameraCheck()
    {
        if (other_player.x - other_player.width  / 2 < this.x - (viewport._renderTarget.width  / 2) * viewport.scale ||
            other_player.x + other_player.width  / 2 > this.x + (viewport._renderTarget.width  / 2) * viewport.scale ||
            other_player.y - other_player.height / 2 < this.y - (viewport._renderTarget.height / 2) * viewport.scale ||
            other_player.y + other_player.height / 2 > this.y + (viewport._renderTarget.height / 2) * viewport.scale)
        {
            viewport.scale += 0.01f;
        }
        else
        {
            if (viewport.scale > 1f) viewport.scale -= 0.01f;
        }
    }

    private void OnCollision(GameObject other)
    {
        if (coll_info != null)
        {
            if (coll_info.normal.y == 1)        // hit ceiling (does not run, the collision is only for 1 frame/not enough)
            {
                Console.WriteLine(jump_frames);
                jump_frames = MAX_JUMP_FRAMES;
            }
        }
    }

    private void Update()
    {
        movementHandle();
        gravPullUntilFloor();
        cameraCheck();
    }
}
