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

    public List<Tween> readyAttack;

    public void Start()
    {
        readyAttack = new List<Tween>();
        readyAttack.Add(DOTween.To(() => stateSliders[2].value, x => stateSliders[2].value = x, 1, attackTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            SetMonsterAttack();
        }));
        readyAttack.Add(DOTween.To(() => stateSliders[2].value, x => stateSliders[2].value = x, 0, .5f).SetEase(Ease.Linear));
        readyAttack[1].Pause().SetAutoKill(false);
        readyAttack[0].SetAutoKill(false);
    }


    public void SetMonsterAttack()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveX(transform.position.x - 6.5f, .8f));
        readyAttack[1].Rewind();
        sequence.Append(readyAttack[1].Play().OnComplete(()=>
        {
            readyAttack[0].Rewind();
        }));

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
            (DungeonUIManager.instance.monsterObj.transform.position.y - 1f, .3f).SetLoops(2, LoopType.Yoyo));
        sequence.Insert(.2f, Camera.main.DOShakeRotation(.1f, 5f));
        yield return new WaitForSeconds(1f);
        //Debug.Log("Rewind");
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
        //Debug.Log("Play");
        readyAttack[0].Play();
    }
}
