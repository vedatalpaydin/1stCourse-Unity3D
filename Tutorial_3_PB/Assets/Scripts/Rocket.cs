using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;  
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float loadLevelDelay = 2f;
    
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;
    
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;

    enum State{ Alive, Dying, Transcending };

     State state = State.Alive;
    
    Rigidbody rb;
    AudioSource audio;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        audio=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state==State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (state!=State.Alive) {return;}
        switch (col.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }
    void StartSuccessSequence()
    {
        state = State.Transcending;
        audio.Stop();
        audio.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextScene",loadLevelDelay);
    }

    void StartDeathSequence()
    {
        state = State.Dying;
        audio.Stop();
        audio.PlayOneShot(death);
        mainEngineParticles.Stop();
        deathParticles.Play();
        Invoke("LoadFirstScene",loadLevelDelay);
    }
    void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
    void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audio.Stop();
            mainEngineParticles.Stop();
        }
    }
    void RespondToRotateInput()
    {
        float rotateThisFrame = rcsThrust * Time.deltaTime;
        rb.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward*rotateThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rotateThisFrame);
        }
        rb.freezeRotation = false;
    }
    void ApplyThrust()
    {
        rb.AddRelativeForce(Vector3.up*mainThrust*Time.deltaTime);
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }
}
