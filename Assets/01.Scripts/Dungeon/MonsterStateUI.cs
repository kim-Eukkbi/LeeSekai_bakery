using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;   

public class MonsterStateUI : MonoBehaviour //���� ��ġ�� �̴ϼ� ������ �ؼ� ������ �� �ʿ��� �۾��� ��
{
    public Text mName;
    public Image mImage;
    public List<Slider> stateSliders;
    public float hp;
    public float mp;
    public float str;
    public float def;
    public float sp;
    public State state;
    public Sprite dropItemSprite; //���Ͱ� ����߸��� �������� �̹���
    public int dropItemIndex; //���Ͱ� ����߸��� �������� ����

    public List<Tween> readyAttack = new List<Tween>(); //Ʈ�� ����Ʈ

    public void Start()
    {
        readyAttack.Add(stateSliders[2].DOValue(1,sp).SetEase(Ease.Linear).OnComplete(() => //������ ������������ ä���ִ� Ʈ���� ����Ʈ�� �߰�
        {
            SetMonsterAttack();//�������� �� á�ٸ� ���Ͱ� Ÿ���ϵ��� �Լ� �߰�
        }));
        readyAttack.Add(stateSliders[2].DOValue(0,.5f).SetEase(Ease.Linear)); //���� �غ� �������� �ٿ��ִ� Ʈ��
        readyAttack[1].Pause().SetAutoKill(false);//���� �غ� ���̴� Ʈ���� �ٷ� �����ϸ� �ȵǴ� �����ϰ� Rewind �� �� �ְ� AutoKill �� ����
        readyAttack[0].SetAutoKill(false);// ����������  Rewind �� �� �ְ� AutoKill �� ����
        DungeonUIManager.instance.monsterStateUIobj.transform.DOMoveX(DungeonUIManager.instance.monsterStateUIobj.transform.position.x - 6.5f, .8f); // ���� UI�� ����
    }


    public void SetMonsterAttack() // ���Ͱ� Ÿ���� �ϱ� ���� �غ� �ϴ� �Լ�
    {
        //transform.DOMoveX(transform.position.x - 6.5f, .8f);
        DungeonUIManager.instance.SetDefaultUI(); //���Ͱ� ������ ������ ��� UI�� �ʱ�ȭ ���� �ʿ䰡 ����
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.stateTweens[i].Pause(); //��� �÷��̾��� �������� ���°� ����
        }
       
        StartCoroutine(AttackMonster()); // ���Ͱ� Ÿ���ϱ� ���� �Լ�
    }

    public IEnumerator AttackMonster()// ���Ͱ� Ÿ���ϱ� ���� �Լ�
    {
        yield return new WaitForSeconds(.8f);
        readyAttack[1].Rewind(); // ������ ���� �غ� �������� ���̴� Ʈ���� �ʱ�ȭ
        readyAttack[1].Play().OnComplete(() => //�� �پ���
        {
            readyAttack[0].Rewind();//������ ���� �غ� �������� ä��� Ʈ���� �ʱ�ȭ
        });
        Sequence sequence = DOTween.Sequence();//�������� �ϳ� �����

        sequence.Append(DungeonUIManager.instance.monsterObj.transform.DOMoveX
            (DungeonUIManager.instance.monsterObj.transform.position.x - 10f, .3f).SetLoops(2, LoopType.Yoyo)); // ���Ͱ� �����ߴٰ� �ٽ� ���� ��ġ�� ���ƿ��� Ʈ��

        DungeonUIManager.instance.AttackEachOther(false);//���Ͱ� ������ ����ϱ� ���� �Լ��� �ҷ���

        sequence.Insert(.2f, Camera.main.DOShakeRotation(.1f, 5f)); //ī�޶� ����ũ ȿ��
        yield return new WaitForSeconds(1f);
        StartCoroutine(EndAttackMonster()); //���Ͱ� ������ ���� ���� �ٽ� �����δ� �Լ�
    }

    public IEnumerator EndAttackMonster()//���Ͱ� ������ ���� ���� �ٽ� �����δ� �Լ�
    {
        for (int i = 0; i < 3; i++)
        {
            if (DungeonUIManager.instance.playerState[i].Equals(State.Dead))
            {
                DungeonUIManager.instance.stateTweens[i].Pause();
                Debug.Log("Stop Living");
                continue;
            }
            DungeonUIManager.instance.stateTweens[i].Play(); // �ٽ� ��� ĳ������ ���� �غ� �������� �÷��ִ� �Լ��� ����
        }
        yield return null; //�������� ��ٷȴٰ�
        readyAttack[0].Play(); // �ٽ� ���͵� �����غ� �������� ä��
    }

    public void DeadMonster()// ���Ͱ� �׾����� ó���� ���� �Լ�
    {
        DungeonUIManager.instance.monsterObj.SetActive(false); // �ϴ� �׳� �������� �ɷ� �غ���
        DungeonUIManager.instance.monsterStateUIobj.transform.DOMoveX(DungeonUIManager.instance.monsterStateUIobj.transform.position.x + 6.5f, .8f); // ���� UI�� �ٽ� ����������
        readyAttack[0].Pause(); //���� �����غ� ������ ����
        DungeonUIManager.instance.stateTweens[0].Pause(); // �÷��̾ �� ����
        DungeonUIManager.instance.stateTweens[1].Pause();
        DungeonUIManager.instance.stateTweens[2].Pause();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(DungeonUIManager.instance.gameEndObj.GetComponent<CanvasGroup>().DOFade(1, .8f));

        sequence.Append(DungeonUIManager.instance.gameEndObjDoma.GetComponent<Image>().DOFade(1, .8f));

        sequence.Append(DungeonUIManager.instance.gameEndObjDomainsideImg.GetComponentInChildren<Image>().DOFade(1, .8f));
        sequence.Join(DungeonUIManager.instance.gameEndObjDomainsideImg.transform.DOLocalMoveY(-.1f, .8f));

        sequence.Append(DungeonUIManager.instance.gameEndObjDomainsideIndex.GetComponentInChildren<Text>().DOFade(1, .8f));

        sequence.Append(DungeonUIManager.instance.gameEndObjVic.GetComponent<Text>().DOFade(1, .8f));

        sequence.Append(DungeonUIManager.instance.gameEndObjContinue.GetComponent<Text>().DOFade(1, .8f));
    }




}
