using UnityEngine;
using System.Collections;

public class RightButton : MonoBehaviour
{
    public Player player;

    public void OnPress_IE(int TouchID)
    {
        player.JumpButtonPress();
    }

    public void OnRelease_IE(int TouchID)
    {
        player.JumpButtonRelease();
    }

}
