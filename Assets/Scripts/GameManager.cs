using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int turnCount;
    public GameObject naught;
    public GameObject cross;
    // O is odd, X is even
    int player = 1;
    // Says who owns each square
    int[] squares = new int[9];
    int gameWinner = 0;
    public GameObject endUI;
    public Text winText;
    bool isGameRunning = true;
    void Awake()
    {
        squares = new int[9];
    }

    void Update()
    {
        // Wait for input to restart
        if (!isGameRunning)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Level");
            }
        }
    }
    public void SquareClicked(GameObject square)
    {
        turnCount += 1;
        // Get square's id
        int squareId = square.GetComponent<ClickableSquare>().squareId;

        // Spawn the nought/cross
        SpawnPrefab(square.transform.position);

        // Set square ownership
        squares[squareId] = player;

        // Check for a winner
        CheckWinner();

        // Next turn (do last)
        AdvanceTurn();


    }

    void SpawnPrefab(Vector3 position)
    {
        // Stop object from spawning in the fucking background
        position.z = 0;
        // Check whose turn it is to spawn correct model
        if (player == 1)
        {
            Instantiate(naught, position, Quaternion.identity);
        }
        else if (player == 2)
        {
            Instantiate(cross, position, Quaternion.identity);
        }
    }

    void AdvanceTurn()
    {
        player++;
        if (player > 2)
        {
            player = 1;
        }
    }

    void CheckWinner()
    {
        if (turnCount >= 9)
        {
            winText.text = "DRAW";
            ShowEndUI();
        }
        // Middle Square options
        if (squares[4] == player)
        {
            if (squares[1] == player && squares[7] == player)
            {
                gameWinner = player;
            }
            else if (squares[2] == player && squares[6] == player)
            {
                gameWinner = player;
            }
            else if (squares[0] == player && squares[8] == player)
            {
                gameWinner = player;
            }
            else if (squares[3] == player && squares[5] == player)
            {
                gameWinner = player;
            }
        }
        // Right column
        else if (squares[5] == player)
        {
            if (squares[2] == player && squares[8] == player)
            {
                gameWinner = player;
            }
        }
        // Left column
        else if (squares[3] == player)
        {
            if (squares[1] == player && squares[6] == player)
            {
                gameWinner = player;
            }
        }
        // Top row
        else if (squares[1] == player)
        {
            if (squares[0] == player && squares[2] == player)
            {
                gameWinner = player;
            }
        }
        // Bottom row
        else if (squares[7] == player)
        {
            if (squares[6] == player && squares[8] == player)
            {
                gameWinner = player;
            }
        }

        // Do the win things
        if (gameWinner != 0)
        {
            // Set win text (not yet)
            if (gameWinner == 1)
            {
                winText.text = "Player 1 wins!";
            }
            else if (gameWinner == 2)
            {
                winText.text = "Player 2 wins!";
            }

            ShowEndUI();
        }
    }

    void ShowEndUI()
    {
        // Activate the UI
        endUI.SetActive(true);
        isGameRunning = !isGameRunning;
    }
}
