using UnityEditor;
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

    bool isTranscending = false;
    bool collisionDisabled = false;
    
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
        if (!isTranscending)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }

        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
    }

     void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (isTranscending || collisionDisabled) {return;}
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
        isTranscending=true;
        audio.Stop();
        audio.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel",loadLevelDelay);
    }

    void StartDeathSequence()
    {
        isTranscending=true;
        audio.Stop();
        audio.PlayOneShot(death);
        mainEngineParticles.Stop();
        deathParticles.Play();
        Invoke("LoadFirstLevel",loadLevelDelay);
    }
    void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+1;
        if (nextSceneIndex==SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        }
    void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            StopApplyingThrust();
        }
    }

     void StopApplyingThrust()
    {
        audio.Stop();
        mainEngineParticles.Stop();
    }

    void RespondToRotateInput()
    {
        rb.angularVelocity = Vector3.zero;
        float rotateThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward*rotateThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rotateThisFrame);
        }
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
