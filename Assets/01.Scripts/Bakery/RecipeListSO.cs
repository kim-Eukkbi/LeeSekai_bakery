using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BakerySO/RecipeListSO")]
public class RecipeListSO : ScriptableObject
{
    //레시피들의 리스트 나중에 리소스로드 써서 로드할거임
    public List<RecipeSO> recipes;
}
