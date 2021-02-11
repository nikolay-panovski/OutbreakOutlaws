using System;
using GXPEngine;
using TiledMapParser;

class Level : GameObject
{
    private HumanPlayer player1_ref;
    private AlienPlayer player2_ref;
    private Pivot bullet_handler = new Pivot();       // who the hell calls an empty container/handler class PIVOT??
    //might need 2?
    public Level(string filename) : base()
    {
        AddChild(bullet_handler);
        TiledLoader level_loader = new TiledLoader(filename);
        level_loader.rootObject = this;

        level_loader.addColliders = true;
        level_loader.autoInstance = true;
        level_loader.LoadObjectGroups();
        level_loader.LoadTileLayers();      // sprite origins bad?

        player1_ref = FindObjectOfType<HumanPlayer>();
        player2_ref = FindObjectOfType<AlienPlayer>();

        player1_ref.other_player = player2_ref;
        player2_ref.other_player = player1_ref;
        player1_ref.bullet_handler = bullet_handler;
    }
}