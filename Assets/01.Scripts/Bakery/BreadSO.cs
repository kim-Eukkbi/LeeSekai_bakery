using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BakerySO/BreadSO")]
public class BreadSO : ItemSO
{
    //예는 그냥 틀임 아무것도 구현 안해도 됨
    //생각해보니까 레시피를 빵에다 넣어놓는게 더 편할거같음
    public List<IngredientSO> recipe;
}
