/*
    this script : is attached to the player main node . 
    It controls the activities of the player , Mainly receiving calls from the levelInput node of the current level , 
    for it to move and do stuff . 
*/

using Godot;
using System;

public class Player : KinematicBody
{
    // Game objects : 
    Spatial PlayerHead;



    // Game values : 
    float Delta;
    
    float mouseSens = 0.7F;            // should be a value between 0.5 and 1.0
    float maxLookAngle = 88;
    
    Vector3 PlayerVelocity;
    float gravity = 5.8F;
    float Speed = 5;
    Vector3 directionOfMovement;


    // nodepaths : 
    string playerheadpath = "head";




    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        initializeValues();
    }

    public void initializeValues()
    {
        PlayerHead = GetNode<Spatial>(playerheadpath);

        // convert the degrees maxlookangle to radians : 
        maxLookAngle = Godot.Mathf.Deg2Rad(maxLookAngle);

        // set gravity 
        PlayerVelocity.y = -gravity;
    }

    public override void _PhysicsProcess(float delta)
    {
        Delta = delta;
        CalculateForces();              // calculate the forces of the player. 
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {

    //  }

    // start of custom functions : 

    void CalculateForces()
    {
        // add forces to the player every physics process , according to the player velocity set
        this.MoveAndSlide(PlayerVelocity + directionOfMovement);
    }


    // player rotation and looking around : 
    public void updatePlayerRotation(Vector2 relativeMousePos)
    {
        // called by the level input script , to update the player rotation when the mouse is moved . 
        lookUpAndDown(relativeY: relativeMousePos.y);
        lookSides(relativeX: relativeMousePos.x);
    }

    void lookUpAndDown(float relativeY)
    {
        // enable the player to look up and down
        Vector3 newRot = PlayerHead.Rotation;
        newRot.x = newRot.x + -relativeY * Delta * mouseSens / 20;

        // clamp the rotation of the head in the x axis . 
        newRot.x = Godot.Mathf.Clamp(value: newRot.x, min: -maxLookAngle, max: maxLookAngle);

        // set rotation 
        PlayerHead.Rotation = newRot;
    }

    void lookSides(float relativeX)
    {
        // enable the player to look sideways . 
        Vector3 newRot = this.Rotation;
        newRot.y = newRot.y + -relativeX * Delta * mouseSens / 10;

        this.Rotation = newRot;
    }

    // End 

    // player movement in the x and z axes : 
    public void movePlayer(int directionIndex)
    {
        // the direction that the force should be applied is given in 4 indexes : 0,1,2,3
        // called when the directional keys WASD are called : 
        // the action string passed can either instruct the method , to stop or move the player . 
        
        switch (directionIndex)
        {
            case 1:
                // back
                directionOfMovement = Speed * Transform.basis.z * Delta * 50;
                break;
            case 2:
                // left
                directionOfMovement = -Speed * Transform.basis.x * Delta * 50;
                break;
            case 3:
                // right
                directionOfMovement = Speed * Transform.basis.x * Delta * 50;
                break;
            default:
                // forward or 0
                directionOfMovement = -Speed * Transform.basis.z * Delta * 50;
                break;
        }
    }

    public void stopPlayer(int directionIndex)
    {
        // reset the player direction of movement to zero in all axes , when the player stops
        directionOfMovement = new Vector3();        

        // stop the player movement in the axes specified by the caller method : 
        switch (directionIndex)
        {
            case 1:
                // back
                PlayerVelocity.z = 0;
                break;
            case 2:
                // left
                PlayerVelocity.x = 0;
                break;
            case 3:
                // right
                PlayerVelocity.x = 0;
                break;
            default:
                // forward or 0
                PlayerVelocity.z = 0;
                break;
        }
    }




    // End



}
