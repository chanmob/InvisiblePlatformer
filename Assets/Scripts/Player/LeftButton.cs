using UnityEngine;
using System.Collections;

public class LeftButton : MonoBehaviour
{
    private bool IsPressed;
    private bool RightSide;
    private bool LeftSide;

    private int StoredTouchID;

    public Player player;

    void Start()
    {
        IsPressed = false;
        RightSide = false;
        LeftSide = false;
    }

    void Update()
    {
        if (IsPressed == true)
        {

            if (RightSide == true && TouchCamera.DragPos[StoredTouchID].x < this.transform.position.x)
            {
                RightSide = false;
                LeftSide = true;
                player.LeftButtonPress();
                player.RightButtonRelease();
            }

            else if (RightSide == false && TouchCamera.DragPos[StoredTouchID].x > this.transform.position.x)
            {
                RightSide = true;
                LeftSide = false;
                player.RightButtonPress();
                player.LeftButtonRelease();
            }
        }
    }



    public void OnPress_IE(int TouchID)
    {
        StoredTouchID = TouchID;

        IsPressed = true;

        if (TouchCamera.inputHitPos[StoredTouchID].x < this.transform.position.x)
        {
            LeftSide = true;
            player.LeftButtonPress();
        }
        else
        {
            RightSide = true;
            player.RightButtonPress();
        }

    }

    public void OnRelease_IE(int TouchID)
    {
        if (TouchCamera.inputHitPos[StoredTouchID].x < this.transform.position.x)
        {
            LeftSide = false;
            player.LeftButtonRelease();
        }

        else
        {
            RightSide = false;
            player.RightButtonRelease();
        }

        if (RightSide == false && LeftSide == false)
        {
            IsPressed = false;
        }
    }

}
