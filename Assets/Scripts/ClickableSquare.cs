using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableSquare : MonoBehaviour
{
    public int squareId = 0;

    void OnMouseDown()
    {
        // Send message to GameManager.cs
        GameObject.Find("Game Manager").SendMessage("SquareClicked", gameObject);
        Destroy(this);
    }
}
