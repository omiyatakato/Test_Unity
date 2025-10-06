using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FoodListUI : MonoBehaviour
{
    [SerializeField] private Transform contentParent; // ScrollView��Content
    [SerializeField] private GameObject foodSlotPrefab; // 1����UI�v���n�u
    [SerializeField] private List<foodEntity> foods; // �o�^�ς݂̐H�ނ���
    public List<foodEntity> Foods => foods;
    void Start()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var food in foods)
        {
            if (food.Discovery)
            {
                
                // �X���b�g����
                var slot = Instantiate(foodSlotPrefab, contentParent);

                // �q�I�u�W�F�N�g��T��
                var icon = slot.transform.Find("Icon").GetComponent<Image>();
                var name = slot.transform.Find("Name").GetComponent<Text>();

                // �f�[�^�𔽉f
                icon.sprite = food.icon;
                name.text = food.foodName;
            }
            else
            {
                // �X���b�g����
                var slot = Instantiate(foodSlotPrefab, contentParent);

                // �q�I�u�W�F�N�g��T��
                var icon = slot.transform.Find("Icon").GetComponent<Image>();
                var name = slot.transform.Find("Name").GetComponent<Text>();

                // �f�[�^�𔽉f
                icon.sprite = food.icon;
                icon.color = new Color(0f, 0f, 0f, 0.5f);
                name.text = "???";
            }
        }
    }
    public void AllRearrangement()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var food in foods)
        {
            if(food.Discovery)
            {
               // �X���b�g����
                var slot = Instantiate(foodSlotPrefab, contentParent);

                // �q�I�u�W�F�N�g��T��
                var icon = slot.transform.Find("Icon").GetComponent<Image>();
                var name = slot.transform.Find("Name").GetComponent<Text>();

                // �f�[�^�𔽉f
                icon.sprite = food.icon;
                name.text = food.foodName;
            }
            else
            {
                // �X���b�g����
                var slot = Instantiate(foodSlotPrefab, contentParent);

                // �q�I�u�W�F�N�g��T��
                var icon = slot.transform.Find("Icon").GetComponent<Image>();
                var name = slot.transform.Find("Name").GetComponent<Text>();

                // �f�[�^�𔽉f
                icon.sprite = food.icon;
                icon.color = new Color(0f, 0f, 0f, 0.5f);
                name.text = "???";
            }
        }
    }
    public void Rearrangement(int couse)
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < foods.Count; i++)
        {
            var food = foods[i];

            if (food.Corse != couse) continue;

            // �X���b�g����
            var slotObj = Instantiate(foodSlotPrefab, contentParent);
            var slot = slotObj.GetComponent<Slot>();

            // Slot �� food �� index ��n��
            slot.Initialize(food, i);

            // �q�I�u�W�F�N�g��T��
            var icon = slotObj.transform.Find("Icon").GetComponent<Image>();
            var name = slotObj.transform.Find("Name").GetComponent<Text>();

            icon.sprite = food.icon;

            if (food.Discovery)
            {
                name.text = food.foodName;
                icon.color = Color.white;
            }
            else
            {
                name.text = "???";
                icon.color = new Color(0f, 0f, 0f, 0.5f);
            }
        }
    }
}
