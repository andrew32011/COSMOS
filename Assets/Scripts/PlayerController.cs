using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GravityController : MonoBehaviour
{
    [SerializeField] public float GravityForse;
    [SerializeField] public float JumpForse;
    [SerializeField] private TargetToPlanet Parent;
    [SerializeField] public bool Isgrownded;
    [SerializeField] public float _timer;
    [SerializeField] public float moveSpeed;
    [SerializeField] private UnityEngine.UI.Button leftButton;
    [SerializeField] private UnityEngine.UI.Button rightButton;

    private StickmanController _controller;
    private CameraSize _cameraSize;

    private bool isleftButtonPressed;
    private bool isRightButtonPressed;

    //private bool moveLeft;
    //private bool moveRight;

    private void Start()
    {
        Parent = gameObject.GetComponentInParent<TargetToPlanet>();
        _cameraSize = GameObject.FindObjectOfType<CameraSize>();
        _controller = gameObject.GetComponentInChildren<StickmanController>();

        //leftButton.onClick.AddListener(OnLeftButtonPressed);
        //leftButton.onClick.AddListener(OnLeftButtonReleased);
        

        //rightButton.onClick.AddListener(OnRightButtonPressed);
        //rightButton.onClick.AddListener(OnRightButtonReleased);
    }
    
    public void OnLeftButtonPressed()
    {
        isleftButtonPressed = true;
        Debug.Log("LeftPressed");
    }

    public void OnLeftButtonReleased()
    {
        isleftButtonPressed = false;
        Debug.Log("LeftReleased");
    }

    public void OnRightButtonPressed()
    {
        isRightButtonPressed = true;
          
    }

    public void OnRightButtonReleased()
    {
        isRightButtonPressed = false;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            Isgrownded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            Isgrownded = false;
        }
    }

    public void PlusJump(float Add)
    {
        JumpForse += Add;
    }

    private void Update()
    {
        if (JumpForse > GravityForse)
        {
            Isgrownded = false;
        }

        if (!Isgrownded)
        {
            ApplyGravity();
        }

        
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || isleftButtonPressed == true)
        {
            HandleMovement(1);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || isRightButtonPressed == true)
        {
            HandleMovement(-1);
        }
        
        

        
    }

    private void ApplyGravity()
    {
        Vector3 direction = Parent.planetCenter.transform.position - transform.position;
        float distance = direction.magnitude;
        direction.Normalize();

        if (distance > 0.1f)
        {
            Parent.transform.position += direction * (GravityForse - JumpForse) * Time.deltaTime;
        }

        _timer += Time.deltaTime;
        if (_timer >= 0.5f)
        {
            if (JumpForse > 0)
            {
                JumpForse -= 0.3f;
            }
            if (JumpForse < 0)
            {
                JumpForse = 0;
            }
            _timer = 0;
        }
    }

    public void HandleMovement(int Move)
    {
        if (Move == 1)
        {
            Parent.transform.RotateAround(Parent.planetCenter.transform.position, Vector3.forward, moveSpeed * Time.deltaTime);
            _controller.hasChangedDirection(true);
        }
        if (Move == -1)
        {
            Parent.transform.RotateAround(Parent.planetCenter.transform.position, Vector3.forward, -moveSpeed * Time.deltaTime);
            _controller.hasChangedDirection(false);
        }
    }
}
