using System;
using GXPEngine;

public class FloorTile : Sprite
{
    public FloorTile(int newX, int newY) : base("checkers_16x16.png")
    {
        SetOrigin(width / 2, height / 2);
        SetXY(newX, newY);
    }
}
