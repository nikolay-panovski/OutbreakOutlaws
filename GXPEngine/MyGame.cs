using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game
{
	//private const int TILE_SIZE = 16;
	public MyGame() : base(1024, 768, false)	// base game/window size = ?
	{
		// init debug
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