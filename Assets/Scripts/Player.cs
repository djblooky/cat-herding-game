using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    private MoveController moveController;
    private AudioSource audioSource;

    private void Start()
    {
        moveController = GetComponent<MoveController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Move();
    }

    private void PlayFootsteps()
    {
        audioSource.volume = Random.Range(0.01f, 0.05f);
        audioSource.pitch = Random.Range(0.8f, 1.1f);
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

    }

    protected void Move()
    {
        var xInput = 0f;
        var yInput = 0f;

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
            PlayFootsteps();
        }
        else
            audioSource.Stop();


        moveController.SetMoveInput(xInput, yInput);
    }
}