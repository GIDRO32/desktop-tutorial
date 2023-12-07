using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public AudioSource Game_Music;
    public AudioSource Game_SFX;
    public AudioClip Use_Ability;
    public AudioClip Cant_Use_Ability;
    public AudioClip A2_End;
    public AudioClip Pause_Click;
    public AudioClip drag;
    public GameObject Player;
    public GameObject Island;
    public GameObject A1_Button;
    public GameObject A2_Button;
    public GameObject Pause_Button;
    public Text survivalTime;
    public Text Ability1;
    public Text Ability2;
    public Text Earnings;
    private int A1_Count;
    private int A2_Count;
    private int music_volume;
    private int sfx_volume;
    public float islandSize;
    private float shrinkSpeed = 0.1f;
    private float playerShrinkSpeed = 0.02f;
    public float playerSize;
    public float islandMoveSpeed;
    public GameObject Pause;
    public GameObject LosePanel;
    private float timer;
    private int coins; // New variable to track coins
    private int income;
    private bool isDragging = false;
    private bool isSpeedChangeActive = false;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        InvokeRepeating("ChangeIslandPosition", 0f, 3f);
        Player.SetActive(true);
        Island.SetActive(true);
        Pause.SetActive(false);
        LosePanel.SetActive(false);
        coins = 0; // Initialize coins count
        A1_Count = PlayerPrefs.GetInt("A1 Num", A1_Count);
        A2_Count = PlayerPrefs.GetInt("A2 Num", A2_Count);
    }

    void ChangeIslandPosition()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomZ = Random.Range(-10f, 10f);
        Vector3 targetPosition = new Vector3(randomX, 0f, randomZ);
        StartCoroutine(MoveIslandSmoothly(targetPosition));
    }

    IEnumerator MoveIslandSmoothly(Vector3 targetPosition)
    {
        Vector3 startPosition = Island.transform.position;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (Island.transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * islandMoveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            Island.transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }
    }

    public void SlowIsland()
{
    if (!isSpeedChangeActive && A2_Count > 0)
    {
        Game_SFX.PlayOneShot(Use_Ability);
        A2_Count--;
        isSpeedChangeActive = true;

        StartCoroutine(SlowIslandCoroutine());
    }
    else if (A2_Count == 0 || isSpeedChangeActive)
    {
        Game_SFX.PlayOneShot(Cant_Use_Ability);
    }
}

IEnumerator SlowIslandCoroutine()
{
    playerShrinkSpeed = 0.01f;
    shrinkSpeed = 0.05f;
    islandMoveSpeed = 1;

    // Wait for 3 seconds
    yield return new WaitForSeconds(3f);

    // Reset speeds to normal
    shrinkSpeed = 0.1f;
    islandMoveSpeed = 3;
    playerShrinkSpeed = 0.02f;

    Game_SFX.PlayOneShot(A2_End);

    isSpeedChangeActive = false; // Allow pressing the button again
}
    public void IncreaseSize()
    {
        if(A1_Count > 0)
        {
            Game_SFX.PlayOneShot(Use_Ability);
            A1_Count--;
            islandSize = 1;
            playerSize = 0.25f;
        }
        else if(A1_Count == 0)
        {
            Game_SFX.PlayOneShot(Cant_Use_Ability);
        }
    }

    // Update is called once per frame
    void Update()
    {
        islandSize = Mathf.Max(0, islandSize - (Time.deltaTime * shrinkSpeed)); // Shrink island size gradually
        playerSize = Mathf.Max(0, playerSize - (Time.deltaTime * playerShrinkSpeed)); // Shrink player size gradually

        Island.transform.localScale = new Vector3(islandSize, islandSize, islandSize);
        Player.transform.localScale = new Vector3(playerSize, playerSize, playerSize);

        // Player control by dragging mouse or finger
        if (Input.GetMouseButtonDown(0))
        {
            // Mouse button pressed, start dragging
            isDragging = true;
            Game_SFX.PlayOneShot(drag);
            // Set the target position to the current mouse cursor position
            targetPosition = GetMouseWorldPosition();

        }

        if (isDragging)
        {
            // Mouse button is held down, smoothly move the player to the target position
            Player.transform.position = Vector3.Lerp(Player.transform.position, targetPosition, Time.deltaTime * 2f);

            if (Vector3.Distance(Player.transform.position, targetPosition) < 0.01f)
            {
                // If the player is close enough to the target position, stop dragging
                isDragging = false;
            }
        }

        // Update survival time
        timer += Time.deltaTime;
        survivalTime.text = Mathf.Round(timer).ToString();
        coins = Mathf.RoundToInt(timer);
        Ability1.text = A1_Count.ToString();
        Ability2.text = A2_Count.ToString();
    }

    public void GamePause()
    {
        Pause.SetActive(true);
        Pause_Button.SetActive(false);
        A1_Button.SetActive(false);
        A2_Button.SetActive(false);
        Game_SFX.PlayOneShot(Pause_Click);
        Time.timeScale = 0f;
    }

    public void KeepPlay()
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
        Pause_Button.SetActive(true);
        A1_Button.SetActive(true);
        A2_Button.SetActive(true);
        Game_SFX.PlayOneShot(Pause_Click);
    }
    public void Home()
    {
        income = Mathf.RoundToInt(timer);
        coins = PlayerPrefs.GetInt("Coins", coins);
        Earnings.text = "+" + income;
        coins = coins + income;
        PlayerPrefs.SetInt("Coins", coins);
        SceneManager.LoadScene("Menu");
    }

    Vector3 GetMouseWorldPosition()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z - Player.transform.position.z);
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Island)
        {
            Time.timeScale = 0f;
            LosePanel.SetActive(true);
            A1_Button.SetActive(false);
            A2_Button.SetActive(false);
            Pause_Button.SetActive(false);
            income = Mathf.RoundToInt(timer);
            coins = PlayerPrefs.GetInt("Coins", coins);
            Earnings.text = "+" + income;
            coins = coins + income;
            PlayerPrefs.SetInt("Coins", coins);
        }
    }
}
