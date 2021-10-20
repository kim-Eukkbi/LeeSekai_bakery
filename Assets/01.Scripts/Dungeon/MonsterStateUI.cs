using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;   

public class MonsterStateUI : MonoBehaviour
{
    public Text mName;
    public Image mImage;
    public List<Slider> stateSliders;
    public float hp;
    public float mp;
    public float attackDamage;
    public float defense;
    public float attackTime;

    public Tween readyAttack;

    public void Start()
    {
        readyAttack = DOTween.To(() => stateSliders[2].value, x => stateSliders[2].value = x, 1, attackTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            SetMonsterAttack();
        });
    }


    public void SetMonsterAttack()
    {
        transform.DOMoveX(transform.position.x - 6.5f, .8f);
        DungeonUIManager.instance.SetDefaultUI();
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.StateTweens[i].Pause();
        }
        StartCoroutine(AttackMonster());
    }

    public IEnumerator AttackMonster()
    {
        yield return new WaitForSeconds(.8f);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(DungeonUIManager.instance.monsterObj.transform.DOMoveY
            (DungeonUIManager.instance.monsterObj.transform.position.y - 1f, .5f).SetLoops(2, LoopType.Yoyo));
        sequence.Insert(.25f, Camera.main.DOShakeRotation(.1f, 5f));
        yield return new WaitForSeconds(.5f);
        Debug.Log("Rewind");
        readyAttack.Rewind();
        StartCoroutine(EndAttackMonster());
    }

    public IEnumerator EndAttackMonster()
    {
        transform.DOMoveX(transform.position.x + 6.5f, .8f);
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.StateTweens[i].Play();
        }
        yield return null;
        Debug.Log("Play");
        readyAttack.Play();
    }
}
