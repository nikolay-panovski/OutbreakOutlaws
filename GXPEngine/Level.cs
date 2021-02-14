using System;
using System.Collections.Generic;
using GXPEngine;
using TiledMapParser;

class Level : GameObject
{
    public List<Sprite> colliders { get; set; }
    private HumanPlayer player1 = new HumanPlayer("colors.png", 1, 1);
    private AlienPlayer player2 = new AlienPlayer("square.png", 1, 1);
    //private DebugPlayer2 debug_ref;
    private Pivot bullet_handler = new Pivot();       // who the hell calls an empty container/handler class PIVOT??
    private Pivot acid_handler = new Pivot();

    private ObjectManager spawner = new ObjectManager();
    public Level(string filename) : base()
    {
        AddChild(spawner);
        AddChild(bullet_handler);
        AddChild(acid_handler);
        // tile spawning for floor
        colliders = new List<Sprite>();
        for (int tileX = -MyGame.TILE_SIZE; tileX <= game.width + MyGame.TILE_SIZE; tileX += MyGame.TILE_SIZE)
        {
            Sprite floor_tile = new FloorTile(tileX, game.height - MyGame.TILE_SIZE);
            //colliders.Add(floor_tile);
            AddChild(floor_tile);
        }


        player1.SetXY(256, 128);
        AddChild(player1);
        player1.other_player = player2;
        player1.bullet_handler = bullet_handler;

        player2.SetXY(512, 128);
        AddChild(player2);
        player2.other_player = player1;
        player2.bullet_handler = acid_handler;
    }
}