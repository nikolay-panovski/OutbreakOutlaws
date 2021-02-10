using System;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
public class AlienPlayer : Player
{
    public HumanPlayer other_player { get; set; }

    public AlienPlayer(string filename, int columns, int rows, TiledObject obj) : base(filename, columns, rows, obj)
    {
        HP = 3;
    }

    private void movementHandle()
    {
        if (Input.GetKey(Key.LEFT) ) x -= x_speed;
        if (Input.GetKey(Key.RIGHT)) x += x_speed;
    }

    private void OnCollision(GameObject other)
    {

    }

    private void Update()
    {
        movementHandle();
        gravPullUntilFloor();
    }
}
