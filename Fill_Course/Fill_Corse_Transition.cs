using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SmoothScrollRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private int pageCount = 8;        // �S�y�[�W��
    [SerializeField] private float snapSpeed = 15f;   // �␳�X�s�[�h
    [SerializeField] private float snapThreshold = 200f; // velocity ������ȉ��ŃX�i�b�v

    private int currentPage = 0;
    private Vector2 targetPos;
    private bool isSnapping = false;
    private bool isDragging = false;
    private Coroutine scrollCoroutine;
    private Vector2 dragStartPos;
    private void Awake()
    {
        if (scrollRect == null)
            scrollRect = GetComponent<ScrollRect>();
    }

    private void Update()
    {
        if (scrollRect == null || scrollRect.content == null) return;

        if (!isDragging && !isSnapping)
        {
            // ���x���ቺ������␳�J�n
            if (scrollRect.velocity.magnitude < snapThreshold)
            {
                SnapToNearest();
            }
        }

        if (isSnapping)
        {
            scrollRect.content.anchoredPosition = Vector2.Lerp(
                scrollRect.content.anchoredPosition,
                targetPos,
                Time.deltaTime * snapSpeed
            );

            if (Vector2.Distance(scrollRect.content.anchoredPosition, targetPos) < 0.1f)
            {
                scrollRect.content.anchoredPosition = targetPos;
                scrollRect.velocity = Vector2.zero;
                isSnapping = false;
            }
        }
    }

    public void NextPage()
    {
        if (currentPage < pageCount - 1)
        {
            currentPage++;
            ScrollToPage(currentPage);
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ScrollToPage(currentPage);
        }
    }

    private void ScrollToPage(int pageIndex)
    {
        float pageWidth = ((RectTransform)scrollRect.content).rect.width / pageCount;
        targetPos = new Vector2(-pageIndex * pageWidth, scrollRect.content.anchoredPosition.y);
        isSnapping = true;

        if (scrollCoroutine != null)
            StopCoroutine(scrollCoroutine);

        scrollCoroutine = StartCoroutine(SmoothScrollCoroutine(pageIndex));
    }

    private IEnumerator SmoothScrollCoroutine(int pageIndex)
    {
        isSnapping = true;
        float elapsed = 0f;
        float duration = 0.5f;
        Vector2 startPos = scrollRect.content.anchoredPosition;

        Vector2 endPos = new Vector2(-pageIndex * ((RectTransform)scrollRect.content).rect.width / pageCount,
                                     scrollRect.content.anchoredPosition.y);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            scrollRect.content.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        scrollRect.content.anchoredPosition = endPos;
        scrollRect.velocity = Vector2.zero;
        isSnapping = false;
        scrollCoroutine = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        dragStartPos = scrollRect.content.anchoredPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        float dragDistance = dragStartPos.x - scrollRect.content.anchoredPosition.x;
        float pageWidth = ((RectTransform)scrollRect.content.GetChild(0)).rect.width;

        if (Mathf.Abs(dragDistance) > pageWidth * 0.1f) // 10%�ŗ׃y�[�W�ړ�
        {
            if (dragDistance > 0 && currentPage < pageCount - 1) currentPage++;
            else if (dragDistance < 0 && currentPage > 0) currentPage--;
        }

        ScrollToPage(currentPage);
    }

    private void SnapToNearest()
    {
        if (scrollRect.content.childCount == 0) return;

        float pageWidth = ((RectTransform)scrollRect.content).rect.width / pageCount;
        float posX = -scrollRect.content.anchoredPosition.x;
        int pageIndex = Mathf.RoundToInt(posX / pageWidth);
        pageIndex = Mathf.Clamp(pageIndex, 0, pageCount - 1);

        // �O��Ɠ����y�[�W�Ȃ�␳���Ȃ�
        if (pageIndex == currentPage) return;

        currentPage = pageIndex;
        targetPos = new Vector2(-pageIndex * pageWidth, scrollRect.content.anchoredPosition.y);
        isSnapping = true;
    }
}