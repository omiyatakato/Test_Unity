using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FoodList", menuName = "MyGame/Food List")]
public class FoodList : ScriptableObject
{
    public List<foodEntity> foods;
}