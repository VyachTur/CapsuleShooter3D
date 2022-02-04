using UnityEngine;

public class HeroBehaviour : MonoBehaviour
{
    private GameBehaviour _gameManager;

    public float moveSpeed = 10f;
    public float rotateSpeed = 90f;

    public float jumpVelocity = 5f;

    private bool _doJump = false;
    private bool _doFire = false;
    private bool _isGroundOut = false;

    private float _vInput;
    private float _hInput;

    private Rigidbody _rb;

    private float distanceToGround = 0.1f;

    public LayerMask groundLayer;

    private BoxCollider _col;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    public delegate void JumpingEvent();
    
    public event JumpingEvent playerJump;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<BoxCollider>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehaviour>();
    }

    // 1
    void FixedUpdate()
    {
        if (_doJump)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            _doJump = false;

            playerJump?.Invoke();
        }

        if (_doFire)
        {
            GameObject newBullet = Instantiate(bullet,
                this.transform.position + new Vector3(1, 0, 0),
                this.transform.rotation) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = this.transform.forward * bulletSpeed;

            _doFire = false;
        }

        if (_isGroundOut)
        {
            Utilities.RestartLevel();
        }

        Vector3 rotation = Vector3.up * _hInput;

        Quaternion angleRot = Quaternion.Euler(rotation *
        Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position +
        this.transform.forward * _vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 4;
        }
    }

    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * moveSpeed;
        _hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) _doJump = true;

        if (Input.GetMouseButtonDown(0)) _doFire = true;

        _isGroundOut = IsGroundOut();
    }


    private bool IsGrounded()
    {
         Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
         _col.bounds.min.y, _col.bounds.center.z);

        bool isGrounded = Physics.CheckCapsule(_col.bounds.
        center, capsuleBottom, distanceToGround, groundLayer,
        QueryTriggerInteraction.Ignore);

        return isGrounded;
    }

    private bool IsGroundOut()
    {
        if (transform.position.y < -20) return true;
        return false;
    }
}
