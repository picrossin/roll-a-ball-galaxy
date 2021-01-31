using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(1f, 100f)] private float speed = 2f;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform gravityCenter;
    [SerializeField] [Range(0f, 100f)] private float gravityConstant = 9.81f;
    [SerializeField] private Transform movementHelper;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _ps;

    private bool _isRunning;
    public bool IsRunning => _isRunning;
    
    private ParticleSystem.MainModule _psMain;
    private ParticleSystem.TrailModule _psTrails;
    private Vector2 _input;
    private Rigidbody _rigidbody;
    private int _count;
    private int _goalCount;
    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _psMain = _ps.main;
        _psTrails = _ps.trails;
        SetCountText();
        winText.text = "";
        _isRunning = true;

        _goalCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
    }

    private void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isRunning = !_isRunning;
        }
        pauseText.text = _isRunning ? "" : "Paused";
        Time.timeScale = _isRunning ? 1 : 0;
        AudioListener.pause = !_isRunning;
    }

    private void FixedUpdate()
    {
        // Gravity
        _rigidbody.AddForce((gravityCenter.position - transform.position).normalized * gravityConstant);

        // Movement
        movementHelper.position = transform.position;
        movementHelper.rotation = Quaternion.FromToRotation(
                                      movementHelper.up, 
                                      (movementHelper.position - gravityCenter.position).normalized) * 
                                  movementHelper.rotation * Quaternion.Euler(0, _input.x, 0);

        _rigidbody.AddForce(movementHelper.forward * _input.y * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            _audioSource.PlayOneShot(collectSound, 0.5f);
            _count++;
            _psMain.maxParticles += 5;
            _psTrails.lifetimeMultiplier += 0.025f;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        countText.text = $"Count: {_count}";
        if (_count == _goalCount)
        {
            winText.text = "You Win!";
        }
    }
}