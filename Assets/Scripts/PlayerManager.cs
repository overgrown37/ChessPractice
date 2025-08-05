using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private string player = "black";

    public string GetPlayer()
    {
        return player;
    }

    public void NextPlayer()
    {
        if (player == "black")
        {
            player = "white";
        }
        else
        {
            player = "black";
        }
    }
}
