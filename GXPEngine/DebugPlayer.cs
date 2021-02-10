using System;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

// used for freeroam debug movement (without gravity),
// and checking if collision returns information for the side that it happened with
public class DebugPlayer : AnimationSprite
{
    private int player_id;
    private enum control_scheme : int { WASD, ARROWS };
    private Collision coll_info;

    public DebugPlayer(string filename, int columns, int rows, TiledObject obj) : base(filename, columns, rows)
    {
        player_id = obj.GetIntProperty("player_id");
    }

    private void movementHandle()
    {
        switch (player_id)
        {
            case (int)control_scheme.WASD:
                if (Input.GetKey(Key.A)) coll_info = MoveUntilCollision(-1, 0);
                if (Input.GetKey(Key.D)) coll_info = MoveUntilCollision( 1, 0);

                if (Input.GetKey(Key.W)) coll_info = MoveUntilCollision(0, -1);
                if (Input.GetKey(Key.S)) coll_info = MoveUntilCollision(0,  1);
                break;
            case (int)control_scheme.ARROWS:
                if (Input.GetKey(Key.LEFT) ) coll_info = MoveUntilCollision(-1, 0);
                if (Input.GetKey(Key.RIGHT)) coll_info = MoveUntilCollision( 1, 0);

                if (Input.GetKey(Key.UP)   ) coll_info = MoveUntilCollision(0, -1);
                if (Input.GetKey(Key.DOWN) ) coll_info = MoveUntilCollision(0,  1);
                break;
            default:
                throw new IndexOutOfRangeException("Invalid player ID. Please only use 0 for Player 1, or 1 for Player 2.");
        }
    }

    private void debugDirectionPrint()      // works, so collision object normal vector provides the wall side info
    {
        if (coll_info != null)
        {
            if (coll_info.normal.x == -1) Console.WriteLine("hit right wall");
            if (coll_info.normal.x ==  1) Console.WriteLine("hit left wall" );
            if (coll_info.normal.y == -1) Console.WriteLine("hit down wall" );
            if (coll_info.normal.y ==  1) Console.WriteLine("hit up wall"   );
        }
    }

    private void Update()
    {
        movementHandle();
        //debugDirectionPrint();
    }
}

