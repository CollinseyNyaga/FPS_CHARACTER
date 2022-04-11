/*
    this script is attached to every /utilities/levelInput node of each level .
    Purpose : manages all the inputs of the current level , provided its attached to the scene.

*/


using Godot;
using System;

public class levelInput : Node
{
    // game objects : 
    Node Player = new Node();
    Node Weapon = new Node();

    // game values : 



    // nodepaths : 
    string playerpath = "../../Player";
    string weaponspath = "../../Player/head/arms/weapon";


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        initializeValues();
    }

    void initializeValues()
    {
        // hide the mouse and lock the cursor at the center of the screen 
        Input.SetMouseMode(Input.MouseMode.Captured);

        // get objects : 
        Player = GetNode(playerpath);
        Weapon = GetNode(weaponspath);
    }

    // Called when there is an input event which is not handled 
    public override void _Input(InputEvent ev)
    {
        // mouse movement 
        Godot.InputEventMouseMotion mm = new Godot.InputEventMouseMotion();
        if (ev.GetType() == mm.GetType())
        {
            // check if the input type is mousemotion. if so , cast the input event to mouse motion input type: 
            InputEventMouseMotion mouseMotion = (InputEventMouseMotion)ev;
            mouseMoved(mm: mouseMotion);
        }

        // mouse Click controls : 
        Godot.InputEventMouseButton mb = new Godot.InputEventMouseButton();
        if(ev.GetType() == mb.GetType())
        {
            mb = (InputEventMouseButton)ev;
            if(mb.Pressed == true && mb.ButtonIndex == 1)
            {
                Weapon.Call(method:"Fire");
                
//              GD.Print("Fire ");
            }
            
            if(mb.Pressed == true && mb.ButtonIndex == 2)
            {
                Weapon.Call(method:"Aim");
                
//              GD.Print("Aim ");
            }
        }
    }




    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // check for inputs every process call .
        checkForKeyboardInputs();
    }


    // start of custom functions : 

    void checkForKeyboardInputs()
    {
        // check for the input events of the set input actions 

        // player movement : 
        if (Input.IsActionPressed("playerForward"))
        {
            Player.Call(method: "movePlayer", args: 0);
        }
        if (Input.IsActionPressed("playerBackward"))
        {
            Player.Call(method: "movePlayer", args: 1);
        }
        if (Input.IsActionPressed("playerLeft"))
        {
            Player.Call(method: "movePlayer", args: 2);
        }
        if (Input.IsActionPressed("playerRight"))
        {
            Player.Call(method: "movePlayer", args: 3);
        }

        // stopping player movement when the user releases the movement buttons : 
        if (Input.IsActionJustReleased("playerForward"))
        {
            Player.Call(method: "stopPlayer", args: 0);
        }
        if (Input.IsActionJustReleased("playerBackward"))
        {
            Player.Call(method: "stopPlayer", args: 1);
        }
        if (Input.IsActionJustReleased("playerRight"))
        {
            Player.Call(method: "stopPlayer", args: 2);
        }
        if (Input.IsActionJustReleased("playerLeft"))
        {
            Player.Call(method: "stopPlayer", args: 3);
        }

    }

    void mouseMoved(InputEventMouseMotion mm)
    {
        Player.Call(method: "updatePlayerRotation", args: mm.Relative);
    }
}
