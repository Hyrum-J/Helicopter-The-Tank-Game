using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : Controller
{
    //Input Keys
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode moveUpKey;
    public KeyCode moveDownKey;
    public KeyCode rollLeftKey;
    public KeyCode rollRightKey;
    public KeyCode hoverKey;
    public KeyCode shootKey;
    public KeyCode PauseButton;

    //Game manager
    public GameManager gameManager = GameManager.instance;
     
    // Start is called before the first frame update
    public override void Start()
    {
        if (GameManager.instance != null && GameManager.instance.players != null)
        {
        

            GameManager.instance.players.Add(this);

        }

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        ProcessInputs();
    }

    //Destroys any mention of this if it is destroyed
    public void OnDestroy()
    {
        if (GameManager.instance != null && GameManager.instance.players != null)
        {
            GameManager.instance.players.Remove(this);
        }
    }

    //Gets all the inputs and tells pawn to do what it needs to do
    public override void ProcessInputs()
    {

        if(Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }

        if(Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }

        if(Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }

        if(Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
        }

        if(Input.GetKey(moveUpKey))
        {
            pawn.MoveUp();
        }

        if ( Input.GetKey(moveDownKey))
        {
            pawn.MoveDown();
        }

        if(Input.GetKeyDown(hoverKey))
        {
            if (pawn.isHovering == true)
            {
                pawn.isHovering = false;
            }
            else if (pawn.isHovering == false)
            {
                pawn.isHovering = true;
            }
            else
            {
                pawn.isHovering = true;
            }
        }

        if(Input.GetKey(rollLeftKey))
        {
            pawn.rollLeft();
        }    

        if(Input.GetKey(rollRightKey))
        {
            pawn.rollRight();
        }

        if(Input.GetKey(shootKey))
        {
            pawn.shoot();
        }

        if(Input.GetKeyDown(PauseButton))
        {
            gameManager.ActivatePauseScreen();
        }

    }
}
