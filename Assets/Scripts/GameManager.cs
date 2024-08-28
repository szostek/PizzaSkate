using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Refs:")]
    public Button startGameButton;
    public Button jumpButton;
    public GameObject gameOverPanel;

    [Header("Points:")]
    [SerializeField] private GameObject pointsPanel;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text topScoreText;
    private float points;
    private float topScore;

    private Player player;

    private void Awake() 
    {
        player = FindObjectOfType<Player>();
        pointsPanel.SetActive(false);
    }

    private void Start() 
    {
        startGameButton.gameObject.SetActive(true);
        jumpButton.gameObject.SetActive(false);

        // Get current top score:
        topScore = PlayerPrefs.HasKey("TopScore") ? PlayerPrefs.GetFloat("TopScore") : 0;
        topScoreText.text = topScore.ToString();
    }

    public void StartButton()
    {
        startGameButton.gameObject.SetActive(false);
        jumpButton.gameObject.SetActive(true);
        player.hasStartedGame = true;
        pointsPanel.SetActive(true);
    }

    public void GameOver()
    {
        // Calc top score, save stats
        jumpButton.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);

        // Calc top score, save
        if (points > topScore) {
            topScore = points;
            PlayerPrefs.SetFloat("TopScore", topScore);
            topScoreText.text = topScore.ToString();
        }
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void AddPoints(float pointsToAdd)
    {
        points += pointsToAdd;
        pointsText.text = points.ToString();
    }
}
