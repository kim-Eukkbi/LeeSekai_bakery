using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BakerySO/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public BreadSO makingBread;
    public List<IngredientSO> ingredients;

    //������SO����Ʈ�� ���� Resources ������ �������� Load �Լ��� �����ǵ� �� �޾ƿ��� �ɵ�?
}
