// ACCESS THE BULLET THAT HITS THE HIT ZONE .

using Godot;
using System;

public class BulletHitZone : Area
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void BodyEntered(Node body)
    {
        // called when a body enters the hitzone
        if(body.IsInGroup("bullet"))
        {
            body.Call(method:"bulletHit");
        }
        
    }
}
