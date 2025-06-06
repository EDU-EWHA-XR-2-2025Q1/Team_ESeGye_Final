using System.Collections;
using UnityEngine;

public class SuccessRainManager : MonoBehaviour
{
    public GameObject successIconPrefab; // successIcon 프리팹 (Image 포함)
    public RectTransform spawnArea;      // Canvas의 영역
    public int count = 300;              // 떨어질 아이콘 수
    public float spawnDelay = 0.05f;     // 생성 간격

    public void StartRain()
    {
        StartCoroutine(SpawnIcons());
    }

    IEnumerator SpawnIcons()
    {
        Canvas parentCanvas = spawnArea.GetComponentInParent<Canvas>();
        if (parentCanvas != null)
        {
            parentCanvas.sortingOrder = 999;  // 맨 앞에 보이도록 설정
        }

        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPos = new Vector2(
                Random.Range(-spawnArea.rect.width / 2f, spawnArea.rect.width / 2f),
                spawnArea.rect.height / 2f + 100f
            );

            GameObject icon = Instantiate(successIconPrefab, spawnArea);
            icon.GetComponent<RectTransform>().anchoredPosition = spawnPos;

            // 아이콘이 형제 중 최상단에 오도록 설정
            icon.transform.SetAsLastSibling();

            // 아이콘에 중력 적용
            Rigidbody2D rb = icon.AddComponent<Rigidbody2D>();
            rb.gravityScale = 4f;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            // 10초 후 자동 삭제
            Destroy(icon, 10f);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}