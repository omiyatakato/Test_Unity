using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Registration : MonoBehaviour
{
    [SerializeField] private FoodList foodList;
    [SerializeField] private GameObject Fill_Couse;
    [SerializeField] private GameObject Bar;
    [SerializeField] private GameObject Scroll;
    [SerializeField] private GameObject List_Food;
    public Image image;
    private Image slotImage;
    public static Registration Instance; // �V���O���g���ŌĂׂ�悤�ɂ���
    [SerializeField] private Button confirmButton;
    private Image confirmImage;
   
    private void Awake()
    {
        Instance = this;
        confirmImage = confirmButton.GetComponent<Image>();
    }

    public void OnSlotSelected(Slot slot,bool ISBool)
    {
        if (ISBool)
        {
            Debug.Log("Yes1111");
            // �o�^�{�^����_�ł�����
            DOTween.Kill(confirmImage); // ������Tween���~�߂�
            confirmImage.color = new Color(0.5f, 1f, 0.5f); ; // ���Z�b�g
            confirmImage.DOFade(0.3f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            Debug.Log("No11");
            DOTween.Kill(confirmImage); // �_�Œ�~
            confirmImage.color = Color.white; // ���ɖ߂�
        }
    }

    public void OnConfirm()
    {
        DOTween.Kill(confirmImage); // �_�Œ�~
        confirmImage.color = Color.white; // ���ɖ߂�
        Debug.Log("�o�^�m��I");
    }

   public void Rearrangement(int index)
    {
        // �f�[�^�𔽉f
      image.sprite = foodList.foods[index].icon;
    }
    public void Appetizer_List()
    {
        Fill_Couse.SetActive(false);
        Bar.SetActive(false);
        Scroll.SetActive(false);
        List_Food.SetActive(true);
    }
    public void Appetizer_ListOf()
    {
        Fill_Couse.SetActive(true);
        Bar.SetActive(true);
        Scroll.SetActive(true);
        List_Food.SetActive(false);
    }
}