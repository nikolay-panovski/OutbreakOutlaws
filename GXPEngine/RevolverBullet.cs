using System;
using GXPEngine;

public class RevolverBullet : Sprite
{
    public float playerDirection { get; private set; }
    public float x_speed { get; set; }
    public float y_speed { get; set; }
    public RevolverBullet(float newX, float newY) : base("bullet.png")
    {
        SetOrigin(width / 2, height / 2);
        x = newX;
        y = newY;

    }

    private void OnCollision(GameObject other)
    {
        if (other is HumanPlayer == false) LateDestroy();
    }

    private void Update()
    {
        MoveUntilCollision(x_speed, y_speed);
    }
}
