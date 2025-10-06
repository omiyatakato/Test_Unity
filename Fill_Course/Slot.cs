using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Slot : MonoBehaviour
{
    private foodEntity food;
    private int index;
    private bool isSelected = false;

    [SerializeField] private Button button;
    private Image slotImage;
    private Color slotImage_Normal;

    public void Initialize(foodEntity f, int i)
    {
        food = f;
        index = i;

        button.onClick.AddListener(OnClick);
    }

    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    private void Start()
    {
        slotImage_Normal = slotImage.color;
    }

    private void OnClick()
    {
        if (!food.Discovery) return; // �������Ă��Ȃ��ꍇ�͖���

        // �I����Ԃ𔽓]
        isSelected = !isSelected;

        // ��Ԃɉ����ĐF�ƃX�P�[����ύX
        slotImage.color = isSelected ? Color.green : slotImage_Normal;
        transform.localScale = isSelected ? Vector3.one * 1.2f : Vector3.one;

        Debug.Log(isSelected ? food.foodName + " �I��" : food.foodName + " ��I��");

        // Registration ���ɒʒm�i�o�^�{�^���_�łȂǁj
        Registration.Instance.OnSlotSelected(this, isSelected);
    }
}