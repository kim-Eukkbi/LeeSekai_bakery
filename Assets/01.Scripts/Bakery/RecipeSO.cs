using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BakerySO/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public BreadSO bread;
    public List<IngredientSO> ingredients;
}
