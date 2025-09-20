using UnityEngine;

public class PacStudentMovement : MonoBehaviour
{
    public float speed = 2f;

    private Animator animator;
    private int currentTargetIndex = 1;
    private AudioManager audioManager;

    private Vector2[] pathPoints = new Vector2[]
    {
        new Vector2(1, -3),
        new Vector2(1, -1),
        new Vector2(6, -1),
        new Vector2(6, -5),
        new Vector2(1, -5),
        new Vector2(1, -3)
    };

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = Object.FindFirstObjectByType<AudioManager>();

        transform.position = pathPoints[0];
        UpdateAnimation(currentTargetIndex);
        
        if (audioManager != null)
            audioManager.StartStepSound();
    }

    private void Update()
    {
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        Vector2 target = pathPoints[currentTargetIndex];
        transform.position = Vector2.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
        
        if ((Vector2)transform.position == target)
        {
            currentTargetIndex = (currentTargetIndex + 1) % pathPoints.Length;
            UpdateAnimation(currentTargetIndex);
        }
    }

    private void UpdateAnimation(int targetIndex)
    {
        switch (targetIndex)
        {
            case 1: animator.Play("PsUp"); break;
            case 2: animator.Play("PsRight"); break;
            case 3: animator.Play("PsDown"); break;
            case 4: animator.Play("PsLeft"); break;
            case 5: animator.Play("PsUp"); break;
        }
    }
}
