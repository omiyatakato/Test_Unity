using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGacha : MonoBehaviour
{
    [SerializeField] private FoodList foodList;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Gacha()
    {
        for(int i=0;i<3;i++)
        {
            int value = Random.Range(0, foodList.foods.Count);
            Debug.Log(foodList.foods[value].name);
            foodList.foods[value].Discovery=true;
        }
    }
}
