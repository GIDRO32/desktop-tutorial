using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Game : MonoBehaviour
{
    public AudioSource Button_Source;
    public AudioClip Use_Ability;
    public AudioClip Cant_Use_Ability;
    public AudioClip A2_End;
    public AudioClip Pause_Button;
    public AudioClip drag;
    public GameObject Player;
    public GameObject Island;
    public Text survivalTime;
    public Text Ability1;
    public Text Ability2;
    private int A1_Count;
    private int A2_Count;
    public float islandSize;
    private float shrinkSpeed = 0.1f;
    private float playerShrinkSpeed = 0.02f;
    public float playerSize;
    public float islandMoveSpeed;
    private float timer;
    private int coins; // New variable to track coins
    private bool isDragging = false;
    private bool isSpeedChangeActive = false;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeIslandPosition", 0f, 3f);
        Player.SetActive(true);
        Island.SetActive(true);
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
        Button_Source.PlayOneShot(Use_Ability);
        A2_Count--;
        isSpeedChangeActive = true;

        StartCoroutine(SlowIslandCoroutine());
    }
    else if (A2_Count == 0 || isSpeedChangeActive)
    {
        Button_Source.PlayOneShot(Cant_Use_Ability);
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

    Button_Source.PlayOneShot(A2_End);

    isSpeedChangeActive = false; // Allow pressing the button again
}
    public void IncreaseSize()
    {
        if(A1_Count > 0)
        {
            Button_Source.PlayOneShot(Use_Ability);
            A1_Count--;
            islandSize = 1;
            playerSize = 0.25f;
        }
        else if(A1_Count == 0)
        {
            Button_Source.PlayOneShot(Cant_Use_Ability);
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
            Button_Source.PlayOneShot(drag);
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
        Ability1.text = A1_Count.ToString();
        Ability2.text = A2_Count.ToString();

        // Convert survival time to coins (you can adjust the conversion logic)
        coins = Mathf.RoundToInt(timer);
        // Save the coins count to PlayerPrefs
        PlayerPrefs.SetInt("Earnings", coins);
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
            // Player exited the collider (left the island)
            // Continue the game
            SceneManager.LoadScene("8GameOver");
        }
    }
}
