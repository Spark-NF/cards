using UnityEngine;

public class Player : MonoBehaviour
{
    public Player()
    {
        Game.current.player = this;
    }
}