using UnityEngine;

public class PacStudentMovement : MonoBehaviour
{
    public float speed = 2f; // 移动速度

    private Animator animator;
    private int currentTargetIndex = 1; // 从第一个目标点开始
    private AudioManager audioManager;   // 引用 AudioManager

    private Vector2[] pathPoints = new Vector2[]
    {
        new Vector2(1, -3), // 起点
        new Vector2(1, -1), // 上
        new Vector2(6, -1), // 右
        new Vector2(6, -5), // 下
        new Vector2(1, -5), // 左
        new Vector2(1, -3)  // 回到起点
    };

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();

        transform.position = pathPoints[0];
        UpdateAnimation(currentTargetIndex);
        
        if (audioManager != null)
            audioManager.StartStepSound();

        //if (audioManager != null)
            //audioManager.PlayStep1();
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

        // 一旦到达目标点，立刻切换到下一个目标点（不中断）
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
            case 5: animator.Play("PsUp"); break; // 回到起点
        }
    }
}