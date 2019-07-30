using UnityEngine;
using System.Collections;

public class RightButton : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void OnPress_IE(int TouchID)
    {
        player.JumpButtonPress();
    }

    public void OnRelease_IE(int TouchID)
    {
        player.JumpButtonRelease();
    }

}
