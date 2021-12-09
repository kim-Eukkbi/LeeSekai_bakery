using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BakerySO/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public BreadSO makingBread;
    public List<IngredientSO> ingredients;

    //레시피SO리스트를 만들어서 Resources 폴더에 넣은다음 Load 함수로 레시피들 다 받아오면 될듯?
}
