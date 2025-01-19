using UnityEngine;
using DG.Tweening;

public class BushManager : MonoBehaviour
{
    [SerializeField] private GameObject bushPrefab;
    [SerializeField] private BushConfig bushConfig;

    private float[] rowYPositions = new float[3];
    private const int STARTING_SORT_ORDER = 1000;
    private const int SORT_ORDER_LAYER_DIFFERENCE = 100;
    private const int BUSH_OFFSET_IN_ROW = 10;

    private void Start()
    {
        InitializeRowPositions();
        CreateBushRows();
    }

    private void InitializeRowPositions()
    {
        SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
        if (spawnManager != null)
        {
            rowYPositions[0] = spawnManager.spawnPosY3 - 110f;
            rowYPositions[1] = spawnManager.spawnPosY2 - 110f;
            rowYPositions[2] = spawnManager.spawnPosY - 110f;
        }
    }

    private void CreateBushRows()
    {
        float currentScale = bushConfig.baseScale;

        for (int row = 0; row < rowYPositions.Length; row++)
        {
            CreateBushRow(rowYPositions[row], currentScale, row);
            currentScale *= bushConfig.depthScaleFactor;
        }
    }

    private void CreateBushRow(float yPosition, float rowScale, int rowIndex)
    {
        float totalWidth = bushConfig.bushesPerRow * bushConfig.bushSpacing * rowScale;
        float startX = -totalWidth / 2;

        for (int i = 0; i < bushConfig.bushesPerRow; i++)
        {
            Vector3 position = new Vector3(
                startX + (i * bushConfig.bushSpacing * rowScale),
                yPosition,
                0
            );

            GameObject bush = Instantiate(bushPrefab, position, Quaternion.identity, transform);
            bush.transform.localScale = new Vector3(rowScale, rowScale, 1f);

            SpriteRenderer bushRenderer = bush.GetComponent<SpriteRenderer>();
            if (bushRenderer != null)
            {
                int rowSortingOrder = STARTING_SORT_ORDER - (rowIndex * SORT_ORDER_LAYER_DIFFERENCE);
                bushRenderer.sortingOrder = rowSortingOrder + BUSH_OFFSET_IN_ROW;
            }
            AddBushAnimation(bush);
        }
    }

    private void AddBushAnimation(GameObject bush)
    {
        float randomOffset = Random.Range(0f, 1f);

        bush.transform
            .DOLocalMoveY(bush.transform.localPosition.y + 0.15f, 1.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetDelay(randomOffset);

        bush.transform
            .DORotate(new Vector3(0, 0, 3f), 2f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetDelay(randomOffset);
    }
    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}