using System.Collections;
using UnityEngine;

public class JumpEffect : MonoBehaviour
{
    public float jumpHeight = 50f;      // 한 번 점프 높이
    public float jumpDuration = 0.35f;  // 한 번 점프에 걸리는 시간 (조금 느리게)

    public void StartJump()
    {
        StartCoroutine(JumpSequence());
    }

    private IEnumerator JumpSequence()
    {
        Vector3 originalPos = transform.localPosition;

        for (int i = 0; i < 2; i++) // 2번만 점프
        {
            yield return MoveTo(originalPos + Vector3.up * jumpHeight, jumpDuration / 2);
            yield return MoveTo(originalPos, jumpDuration / 2);
        }
    }

    private IEnumerator MoveTo(Vector3 targetPos, float duration)
    {
        Vector3 startPos = transform.localPosition;
        float time = 0f;

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPos;
    }
}