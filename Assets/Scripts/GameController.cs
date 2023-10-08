using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    public GameObject Player;

    public bool GamePaused = false;
    private PlayerController playerController;
    private Vector3 playerInitialPosition;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        playerController = Player.GetComponent<PlayerController>();
        playerInitialPosition = Player.transform.position;
        playerController.playerDied.AddListener(PlayerDied);
    }

    private void PlayerDied()
    {
        GamePaused = true;
        StartCoroutine(Sleep(RestartGame));
    }

    private void RestartGame()
    {
        Player.transform.position = playerInitialPosition;
        GamePaused = false;
    }

    private IEnumerator Sleep(Action nextAction)
    {
        yield return new WaitForSeconds(1);
        nextAction.Invoke();
    }
}
