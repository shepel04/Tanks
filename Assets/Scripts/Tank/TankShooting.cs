using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int PlayerNumber = 1;       
    public Rigidbody Shell;            
    public Transform FireTransform;    
    public Slider AimSlider;           
    public AudioSource ShootingAudio;  
    public AudioClip ChargingClip;     
    public AudioClip FireClip;         
    public float MinLaunchForce = 15f; 
    public float MaxLaunchForce = 30f; 
    public float MaxChargeTime = 0.75f;

    private string _fireButton;         
    private float _currentLaunchForce;  
    private float _chargeSpeed;         
    private bool _fired;                


    private void Start()
    {
        _fireButton = "Fire" + PlayerNumber;

        _chargeSpeed = (MaxLaunchForce - MinLaunchForce) / MaxChargeTime;
    }
    
    
    private void Update()
    {
        AimSlider.value = MinLaunchForce;

        if (_currentLaunchForce >= MaxLaunchForce && !_fired)
        {
            _currentLaunchForce = MaxLaunchForce;
            Fire ();
        }
        else if (Input.GetButtonDown(_fireButton))
        {
            _fired = false;
            _currentLaunchForce = MinLaunchForce;

            ShootingAudio.clip = ChargingClip;
            ShootingAudio.Play ();
        }
        else if (Input.GetButton(_fireButton) && !_fired)
        {
            _currentLaunchForce += _chargeSpeed * Time.deltaTime;

            AimSlider.value = _currentLaunchForce;
        }
        else if (Input.GetButtonUp(_fireButton) && !_fired)
        {
            Fire ();
        }
    }
    
    private void OnEnable()
    {
        _currentLaunchForce = MinLaunchForce;
        AimSlider.value = MinLaunchForce;
    }


    private void Fire()
    {
        _fired = true;

        Rigidbody shellInstance = Instantiate(Shell, FireTransform.position, FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = _currentLaunchForce * FireTransform.forward; ;

        ShootingAudio.clip = FireClip;
        ShootingAudio.Play ();

        _currentLaunchForce = MinLaunchForce;
    }
}
