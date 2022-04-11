using Godot;
using System;

public class bulletHole : Spatial
{
    // GAME OBJECTS : 
    CPUParticles particles;
    
    // GAME VALUES : 
    
    
    // PATHS :
    string particlespath = "particles/CPUParticles";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        particles = GetNode<CPUParticles>(particlespath);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void PositionSelf(Vector3 spawnpoint , Vector3 normal)
    {
        // positions the bullet hole according to the direction of normal and the spawnpoint.
        // set translation : 
        Transform newT = this.GlobalTransform;
        newT.origin = spawnpoint;
        
        
        this.GlobalTransform = newT;
        
        // align the rotation of bullet hole to the direction of the normal : 
        
        this.LookAt(target:normal + spawnpoint,up:Vector3.Up);
        
        // play the particles flying particle effect : 
        particles = GetNode<CPUParticles>(particlespath);
        particles.Emitting = true;
    }
}
