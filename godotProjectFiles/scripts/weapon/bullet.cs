using Godot;
using System;

public class bullet : KinematicBody
{
    // GAME OBJECTS : 
    Timer t;
    Spatial muzzle;
    Spatial PlayerHead;
    Spatial Player;
    
    RayCast raycast;
    
    PackedScene bulletHoleScene;
    Node BulletHolesSpawnPoint;
    
    
    
    // GAME VALUES : 
    float Lifetime = 5f;
    
    Vector3 velocity;
    Vector3 direction;
    float Speed = 2f;
    
    Vector3 raycastCollisionPoint;
    Vector3 raycastCollisionNormal;
    
    
    // PATHS : 
    string tpath = "Timer";
    string headpath = "../../Player/head";
    string playerpath = "../../Player";
    
    string raycastpath = "RayCast";
    string bulletholescenepath = "res://resources/scenes/bulletHole.tscn";
    string bulletholesspawnpointpath = "../../BulletHoles";
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        initializeValues();
        t.Start();
    }
    
    void initializeValues()
    {
        t = GetNode<Timer>(tpath);
        t.WaitTime = Lifetime;
        
        
        Player = GetNode<Spatial>(playerpath);
        PlayerHead = GetNode<Spatial>(headpath);
        
        raycast = GetNode<RayCast>(raycastpath);
        
        bulletHoleScene = (PackedScene)Godot.ResourceLoader.Load(bulletholescenepath);
        
        this.AddToGroup("bullet");
        
        BulletHolesSpawnPoint = GetNode(bulletholesspawnpointpath);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public override void _PhysicsProcess(float delta)
    {
        AccelerateBullet();
        
        CheckRayCast();

    }

    public void timerTimedOut()
    {
        // delete this node from the current scenetree . 
        this.QueueFree();
    }
    
    public void PositionSelf(Vector3 pos)
    {
        // set the muzzle , the position which the bullet will spawn from 
        Transform tt = this.GlobalTransform;
        tt.origin = pos;
        
        this.GlobalTransform = tt;
        
        // set the direction and rotation of heading of the bullet , according to the basis of the player head.
        direction = PlayerHead.GlobalTransform.basis.z * -Speed;
        
        // rotation of the bullet . 
        Vector3 newRot = this.Rotation;
        newRot.y = Player.Rotation.y;
        newRot.z = PlayerHead.Rotation.x;
        this.Rotation = newRot;
    }
    
    void AccelerateBullet()
    {
        MoveAndCollide(velocity + direction);
    }
    
    void CheckRayCast()
    {
        if(raycast.IsColliding())
        {
            Node surface = (Node)raycast.GetCollider();
            
            if(surface.IsInGroup("shootable"))
            {
//              GD.Print(surface.Name);
                
                raycastCollisionPoint = raycast.GetCollisionPoint();
                raycastCollisionNormal = raycast.GetCollisionNormal();
                
            }
        }
    }
    
    public void bulletHit()
    {
        // called when the current bullet hits a hitzone (which is an area)
        // a bullet hole will be spawned as a child of the area [bullethitzone] which detects impact of the bullet
        
        // GD.Print("hit shootable");
        
        Node bulletHoleNode = bulletHoleScene.Instance();
        BulletHolesSpawnPoint.AddChild(bulletHoleNode);
        
        bulletHoleNode.Call(method:"PositionSelf", raycastCollisionPoint ,raycastCollisionNormal);
        
        // destroy the bullet : 
        this.QueueFree();
        
    }
    
}









