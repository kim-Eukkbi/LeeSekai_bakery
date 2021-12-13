using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BakerySO/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public ItemSO bread;
    public List<IngredientSO> ingredients;
}
