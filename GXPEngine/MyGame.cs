using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game
{
	public const int TILE_SIZE = 16;
	public MyGame() : base(1024, 768, false)	// base game/window size = ?
	{
		// "level" class probably only necessary because of possible menu/game over screens
		Level level1 = new Level("test_map.tmx");
		AddChild(level1);

	}

    void Update()
	{
		/*if (Input.GetKeyDown(Key.SPACE))
		{
			new Sound("ping.wav").Play();
		}*/
	}

	static void Main()
	{
		new MyGame().Start();
	}
}