using Godot;
using System;

public class weapon : Spatial
{
    // GAME OBJECTS : 
    PackedScene bulletScene;
    Node bulletsParentNode;
    Spatial Muzzle;
    
    AnimationPlayer gunAnim;
    
    AudioStreamPlayer asp;
    
    
    // GAME VALUES : 
    bool canshoot = true;
    
    
    
    
    // PATHS : 
    string bulletscenepath = "res://resources/scenes/bullet.tscn";
    string bulletsparentnodepath = "Level1/Bullets";            // path of the bullets parent node relative to the viewport of the current scene.
    
    string muzzlepath = "Gun/muzzle";
    string asppath = "AudioStreamPlayer";
    string gunanimpath = "Gun/AnimationPlayer";
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        initializeValues();
    }

    void initializeValues()
    {
        bulletScene = (PackedScene)Godot.ResourceLoader.Load(bulletscenepath);
        
        bulletsParentNode = GetViewport().GetNode(bulletsparentnodepath);
        
        Muzzle = GetNode<Spatial>(muzzlepath);
        
        asp = GetNode<AudioStreamPlayer>(asppath);
        
        gunAnim = GetNode<AnimationPlayer>(gunanimpath);
    }
    
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(gunAnim.IsPlaying() == true)
        {
            canshoot = false;
        }
        else
        {
            canshoot = true;
        }
    }

    public void Fire()
    {
        // called when mouse button 1 is pressed 
        
        if(canshoot == true)
        {
            Node bulletNode = bulletScene.Instance();
        
            bulletsParentNode.AddChild(bulletNode);
            
            // position the bullet to the muzzle of the gun , by invoking its method 
            Vector3 muzzlepos = Muzzle.GlobalTransform.origin;
            bulletNode.Call(method:"PositionSelf" , args: muzzlepos);
            
            // play the firing sound :
            asp.Play();
            
            // play the recoil animation : 
            gunAnim.Play("recoilanim");
        }
        
    }
    
    public void Aim()
    {
        // called when mouse button 2 is pressed . 
    }
}
