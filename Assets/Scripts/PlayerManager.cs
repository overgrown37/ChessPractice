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
            Debug.Log("Next Player: White");
            player = "white";
        }
        else
        {
            Debug.Log("Next Player: Black");
            player = "black";
        }
    }
}
