using System;
using GXPEngine;

public class PickupCoin : AnimationSprite
{
    private const int MIN_SPD = 2;
    private const int MAX_SPD = 5;
    private int speed = Utils.Random(MIN_SPD, MAX_SPD);
    public PickupCoin() : base("point_16x16.png", 1, 1)
    {
        SetOrigin(width / 2, height / 2);
    }

    private void Update()
    {
        Move(-speed, 0);
    }
}
