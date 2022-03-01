using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    bool alive = true;

    private float timeCounter = 0;
    private bool dayTime = true;

    private bool check = true;

    public Camera cam;
    public Color color1 = Color.blue;
    public Color color2 = Color.grey;

    public float speed = 5;
    public float horizontalSpeed = 3;
    [SerializeField] Rigidbody rb;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    public float speedIncreasePerPoint = 0.1f;

    private void Start()
    {
       // cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
    }
    private void FixedUpdate ()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * horizontalSpeed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
        //Debug.Log(horizontalMove);
    }

    private void Update () {
        horizontalInput = Input.GetAxis("Horizontal");

        if (transform.position.y < -5) {
            Die();
        }
        timeCounter++;
        if(timeCounter > 1000)
        {
            timeCounter = 0;
            dayTime = !dayTime;
        }

        if (check == true)
        {
            if (dayTime == true)
            {
                speed = speed / 2;
                horizontalSpeed = horizontalSpeed / 2;
                check = false;
                cam.backgroundColor = Color.Lerp(color2, color1, 3);
            }
        }

        if (check == false)
        {
            if (dayTime == false)
            {
                speed = speed * 2;
                horizontalSpeed = horizontalSpeed * 2;
                check = true;
                cam.backgroundColor = Color.Lerp(color1, color2, 3);
            }
        }
        //Debug.Log(speed);
	}

    public void Die ()
    {
        alive = false;
        // Restart the game
        Invoke("Restart", 0);
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}