using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// GameManager class responsible for managing the game
public class GameManager : MonoBehaviour
{
    // UI elements
    public Text CoinDisplayText;                // Display text for coins
    public Text ScoreDisplayText;               // Display text for current score
    public Text MaxScoreDisplayText;            // Display text for maximum score

    // Objects and components
    public ObstacleSpawner ObstacleSpawner_Script; // Script for obstacle spawning
    public GameObject StartButton;                 // Button to start the game
    public GameObject FlyButton;                   // Button to make the player fly
    public GameObject PauseButton;                 // Button to pause the game
    public Animator Background1Anim;               // Animator for background 1
    public Animator Background2Anim;               // Animator for background 2
    public Animator LosePanelAnim;                  // Panel for displaying loss animation

    // Audio sources
    public AudioSource LoseSFX;                   // Audio source for loss sound
    public AudioSource JetPackSFX;                // Audio source for jetpack sound

    // Particle system for jetpack
    public ParticleSystem JetPackParticle;


    int CoinAmount;                               // Number of collected coins
    float AnimatorsSpeed = 1;                     // Speed of background animators
    bool isDied;                                  // Flag indicating player death
    bool isFlying;                                // Flag indicating player flying
    float score;                                  // Current game score
    float maxScore;                               // Maximum achieved score
                                                  
    // Player-related variables
    Rigidbody2D PlayerRigidbody;                 // Player's rigidbody
    Animator PlayerAnimator;                      // Player's animator

    // Jetpack speed parameter
    public int JetPackSpeed = 1;

    //Function At Execution Start.
    void Start()
    {
        // Set Gameobject Rigidbody2D Component to PlayerRigidbody.
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        // Set Gameobject Animator Component to PlayerAnimator.
        PlayerAnimator = GetComponent<Animator>();
        // Set maxScore from integer PlayerPrefs "PlayerMaxScore".
        maxScore = PlayerPrefs.GetInt("PlayerMaxScore");
        // Display maxScore as int with addition of "M" at MaxScoreDisplayText.
        MaxScoreDisplayText.text = (int)maxScore + "M";
        // Set Enable state for Background1Anim to true.
        Background1Anim.enabled = true;
        // Set Enable state for Background2Anim to true.
        Background2Anim.enabled = true;
        // Set Enable state for ObstacleSpawner_Script to true.
        ObstacleSpawner_Script.enabled = true;
    }

    //Function Execute Every Frame.
    void Update()
    {
        // Check if isDied is false.
        if (!isDied)
        {
            // Increase the score over time and Multiply it by 50.
            score += Time.deltaTime * 50;

            ScoreDisplayText.text = ((int)score).ToString("D7") + "M";

            // Check if the current score is higher than the maxScore
            if (score > maxScore)
            {
                //Display at MaxScoreDisplayText the current score as int with addition of "M".
                MaxScoreDisplayText.text = (int)score + "M";
            }

            // Call HandlePlayerMovement Method.
            HandlePlayerMovement();
        }
        else
        {
            // Check if JetPackParticle is in Play mode.
            if (JetPackParticle.isPlaying)
            {
                // Stop JetPackParticle.
                JetPackParticle.Stop();
            }

            // Check if AnimatorsSpeed is less than 0.
            if (AnimatorsSpeed > 0)
            {
                // Decrease the AnimatorsSpeed over time and Divide it 2.
                AnimatorsSpeed -= Time.deltaTime / 2;
                // Set Background1Anim speed to AnimatorsSpeed.
                Background1Anim.speed = AnimatorsSpeed;
                // Set Background2Anim speed to AnimatorsSpeed.
                Background2Anim.speed = AnimatorsSpeed;
            }
        }
    }

    // Handle player movement and jetpack particle effects.
    void HandlePlayerMovement()
    {
        // Check if the player is flying
        if (isFlying)
        {
            // Play "Player_Idle" at PlayerAnimator.
            PlayerAnimator.Play("Player_Idle");
            // Add Force to the PlayerRigidbody at the direction of ((x:0)(y:1)) and multiply it by 500 and deltatime.
            PlayerRigidbody.AddForce(Vector2.up * JetPackSpeed * 500 * Time.deltaTime);

            // Check if JetPackParticle is in Stop mode.
            if (JetPackParticle.isStopped)
            {
                // Play JetPackParticle.
                JetPackParticle.Play();
            }
        }
        else
        {
            // Play "Player_Walk" at PlayerAnimator.
            PlayerAnimator.Play("Player_Walk");

            // Check if JetPackParticle is in Play mode.
            if (JetPackParticle.isPlaying)
            {
                // Stop JetPackParticle.
                JetPackParticle.Stop();
            }
        }
    }

    // Handle collisions with other colliders
    void OnTriggerEnter2D(Collider2D Object)
    {
        //Check if isDied is flase.
        if (!isDied)
        {
            // Check the tag of the collided object if it equal "Coin".
            if (Object.transform.tag == "Coin")
            {
                // Increment CoinAmount.
                CoinAmount++;
                //Display CoinAmount at CoinDisplayText with ": "
                CoinDisplayText.text = ": " + CoinAmount.ToString();

                //Get AudioSource Component and Play it.
                Object.GetComponent<AudioSource>().Play();
                //Get BoxCollider2D Component and Desable it.
                Object.GetComponent<BoxCollider2D>().enabled = false;
                //Get SpriteRenderer Component and Desable it.
                Object.GetComponent<SpriteRenderer>().enabled = false;
            }

            // Check the tag of the collided object if it equal "Obstacle".
            if (Object.transform.tag == "Obstacle")
            {
                // Set isDied to true.
                isDied = true;
                // Call ResetGame Coroutine Method.
                StartCoroutine(ResetGame());
            }
        }
    }

    // Reset the game after player loses.
    IEnumerator ResetGame()
    {
        // Play "Player_Damaged" at PlayerAnimator.
        PlayerAnimator.Play("Player_Damaged");
        // Play LoseSFX AudioSource.
        LoseSFX.Play();
        // Stop JetPackSFX AudioSource.
        JetPackSFX.Stop();

        // Set Enable state for ObstacleSpawner_Script to false.
        ObstacleSpawner_Script.enabled = false;
        //Deactive FlyButton.
        FlyButton.SetActive(false);
        //Deactive PauseButton.
        PauseButton.SetActive(false);

        // Check if the current score is higher than the max score
        if (score > maxScore)
        {
            // Set the score at a PlayerPrefs as int at "PlayerMaxScore".
            PlayerPrefs.SetInt("PlayerMaxScore", (int)score);
        }

        // Wait for 3 seconds.
        yield return new WaitForSeconds(3);
        // Play "Lose Panel Enter" at LosePanelAnim.
        LosePanelAnim.Play("Lose Panel Enter");

        // Wait for 2 seconds.
        yield return new WaitForSeconds(2);
        //Load the Current Scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Set the player's flying state
    public void SetPlayerFlyingState(bool value)
    {
        // Update isFlying to value.
        isFlying = value;
    }
}
