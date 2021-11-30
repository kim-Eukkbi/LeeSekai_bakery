using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DungeonUIManager : MonoBehaviour
{
    public static DungeonUIManager instance;
    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log("�ټ��� UImanager�� �������Դϴ� �ϳ��� ����� �����մϴ�");
            Destroy(instance);
        }
        instance = this;
    }

    [Header("ĳ����")]
    public GameObject fightPanel;
    public GameObject skillPanel;
    public GameObject characterStatePanel;
    public List<GameObject> PlayerObjs;
    public List<GameObject> ponCharacterStateObjs;
    public List<StateUI> characterStateObjs;
    public List<GameObject> fightbuttons;

    [Header("����")]
    public GameObject monsterObj;
    public GameObject monsterStateUIobj;


    [Header("ECT")]
    public Canvas mainCanvas;
    public List<Tween> StateTweens;
    [HideInInspector]
    public StateUI currentCharacterStateUI;
    [HideInInspector]
    public GameObject currentPlayer;
    private float playerMaxHp;
    private float monsterMaxHp;

    public void Start()
    {
        StateTweens = new List<Tween>();
        StartCoroutine(UpStateUI()); 
        StartCoroutine(StartFightProcess());
        monsterMaxHp = monsterStateUIobj.GetComponent<MonsterStateUI>().hp;
    }

    public void AttackEachOther(bool isPlayer) //���� �����°� ����ϴ� �κ�
    {
        if(isPlayer)
        {
            MonsterStateUI monsterStateUI = monsterStateUIobj.GetComponent<MonsterStateUI>(); 
            monsterStateUI.stateSliders[0].DOValue((monsterStateUI.hp - currentCharacterStateUI.attackDamage) / monsterMaxHp, .8f); //���� ü�� ���������� ���� �������� ���� ����� �Ѵ�
            if(monsterStateUI.hp <= 0) //���� �ǰ� 0 ���϶�� 
            {
                monsterStateUI.DeadMonster(); //�� ���� �Լ��� ����
            }
            else
            {
                monsterStateUI.hp -= currentCharacterStateUI.attackDamage; // �ƴ϶�� �� ���
            } 
        }
        else
        {
            currentCharacterStateUI.stateSliders[0].DOValue((currentCharacterStateUI.hp - monsterStateUIobj.GetComponent<MonsterStateUI>().attackDamage) / playerMaxHp, .8f); //���� ���� �������� ���� �ֱٿ� ������ �÷��̾��� ü�� �������� ��� �Ͽ� ����
            currentCharacterStateUI.hp -= monsterStateUIobj.GetComponent<MonsterStateUI>().attackDamage; //�� ���
        }
    }

    public void DownFightUI() //�ѹ��� Ÿ���� ���� �� �����Ŵ��� ������ �Լ�
    {
        Sequence Uiseq = DOTween.Sequence();
        foreach (var item in fightbuttons) item.GetComponent<Button>().interactable = false; 
        Uiseq.Append(fightbuttons[3].transform.DOMoveY(fightbuttons[3].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.1f, fightbuttons[2].transform.DOMoveY(fightbuttons[2].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.2f, fightbuttons[1].transform.DOMoveY(fightbuttons[1].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.3f, fightbuttons[0].transform.DOMoveY(fightbuttons[0].transform.position.y - 3.1f, .5f));
    }

    public IEnumerator UpStateUI() // �� ó�� StateUI�� ����� �ø��� �Լ�
    {
        yield return null;
        Sequence Main = DOTween.Sequence();
        Main.Append(characterStateObjs[0].transform.DOMove(ponCharacterStateObjs[0].transform.position, .5f));
        Main.Insert(.1f, characterStateObjs[1].transform.DOMove(ponCharacterStateObjs[1].transform.position, .5f));
        Main.Insert(.2f, characterStateObjs[2].transform.DOMove(ponCharacterStateObjs[2].transform.position, .5f));
        Main.Join(monsterStateUIobj.transform.DOMoveX(monsterStateUIobj.transform.position.x - 6.5f, .8f));
    }

    #region ���� ����
    public IEnumerator StartFightProcess() // UpStateUI �� ������ ���� ��ٸ� ���Ŀ� ������ �غ��ϴ� �Լ�
    {
        yield return new WaitForSeconds(.8f);
        for (int i = 0; i < characterStateObjs.Count; i++)
        {
            int a = i;
            StateTweens.Add(characterStateObjs[a].stateSliders[2]
            .DOValue(1, characterStateObjs[a].attackTime).SetEase(Ease.Linear).OnComplete(() => //�÷��̾��� ������������ ä��� �ڵ�
            {
                monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Pause(); // �÷��̾��� ������������ �� á�ٸ� ������ ���� �������� ����
                SetFightUI(a); //�����Ŵ��� �ø� a�� 3���� �ϳ��� �˱� ���� Index
            }));
            StateTweens[i].SetAutoKill(false); //��Ʈ���� Rewind ��Ű�� ���� ����ų ����
        }

        for (int i = 0; i < characterStateObjs.Count; i++) //���� �������� �� á���� �ڿ������� �ٿ��ִ� ��Ʈ���� �߰�
        {
            int a = i; //��Ʈ���� i �ٷ� ���� �ȵ�
            StateTweens.Add(characterStateObjs[a].stateSliders[2].DOValue(0, .5f).SetEase(Ease.Linear));  //�ٿ��ִ� ��Ʈ���� ���� �ִ� ä��� Ʈ�� ���ʿ� �߰�
            StateTweens[3 + a].Pause().SetAutoKill(false);  //�ٷ� �پ��� �ȵǴϱ� ��Ʈ�� ���߰� �����ε� 
        }
    }
    public void SetFightUI(int i) //3���� 2�� UI�� ������ �����Ŵ��� �ø�
    {
        for (int j = 0; j < characterStateObjs.Count; j++) //������ 2���� UI�� ������ �׵��� Ʈ���� ������
        {
            if (i == j) continue;
            StateTweens[j].Pause(); // ������ 2�� Ʈ�� ����
            characterStateObjs[j].transform.DOMoveY(characterStateObjs[j].transform.position.y - 5, .8f);// ������ 2�� UI ������
            ponCharacterStateObjs[j].gameObject.SetActive(false); //�ε巯�� �������� ���� ����ִ� ������Ʈ ���� ���� �ϱ�
        }
        currentCharacterStateUI = characterStateObjs[i]; //���õ� ĳ������ UI�� ����
        currentPlayer = PlayerObjs[i]; //���õ� ĳ������ ������Ʈ�� ����
        playerMaxHp = currentCharacterStateUI.hp; // ���õ� ĳ������ �ִ� ä���� ����


        monsterObj.transform.DOMoveX(monsterObj.transform.position.x + .5f, .5f);// ������ ��ġ�� ���� �ڷ�
        StartCoroutine(SetFightMenuUiUP(i)); 
    }
    public IEnumerator SetFightMenuUiUP(int i) //2���� UI�� �������� ���õ� ĳ������ UI�� �������� �ȱ�� ���� �Ŵ��� �ø�
    {
        yield return new WaitForSeconds(.5f);
        StateTweens[3 + i].Rewind(); //���� �������� ���̴� Ʈ���� �ʱ�ȭ ��
        StateTweens[3 + i].Play().OnComplete(() => //������������ ���̰� �ʱ�ȭ �� �� �����ϰ� �ʱ�ȭ�� �Ϸ��ϸ� ���� �������� ä��� Ʈ���� �ʱ�ȭ ��
        {
            StateTweens[i].Rewind();//���������� Ʈ�� �ʱ�ȭ
        });

        //PlayerObjs[i].transform.DOMoveX(PlayerObjs[i].transform.position.x + .5f, .5f);
        characterStateObjs[i].transform.DOMove(ponCharacterStateObjs[i].transform.position, .8f); //���õ� ĳ���͸� ���ĵǾ��ִ� ����ִ� ������Ʈ�� ��ġ�� �ȱ�� Ʈ��
        yield return new WaitForSeconds(.8f);
        UPFightUI(); //FightUI�� �ø��� �Լ�
    }

    public void UPFightUI() //FightUI�� �ø��� �Լ�
    {
        Sequence Uiseq = DOTween.Sequence();
        foreach (var item in fightbuttons) item.GetComponent<Button>().interactable = false; //�ö󰡴� �߿� ��ư�� ������ ���װ� �� �� ������ interactable�� ����
        Uiseq.Append(fightbuttons[0].transform.DOMoveY(fightbuttons[0].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.1f, fightbuttons[1].transform.DOMoveY(fightbuttons[1].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.2f, fightbuttons[2].transform.DOMoveY(fightbuttons[2].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.3f, fightbuttons[3].transform.DOMoveY(fightbuttons[3].transform.position.y + 3.1f, .5f)).OnComplete(() => // ����� �ö���� ȿ���� ����� �������� ����
        { foreach (var item in fightbuttons) item.GetComponent<Button>().interactable = true; }); // ������ ��ư���� �ö���� ���� �Ʊ� ���״� interactable�� ����
    }
    #endregion

    public IEnumerator SetDefaultUI() //��� UI�� �ʱ���·� �����ִ� �Լ�
    {
        //���� ����ġ
        monsterObj.transform.DOMoveX(monsterObj.transform.position.x - .5f, .5f); // �Ʊ� ��¦ �ΰ� �ٽ� �����
        
        for (int i = 0; i < 3; i++) //�ٽ� 3���� ĳ���� UI�� �ø� 
        {
            int a = i;
            //�÷��̾� ������Ʈ ����ġ��
            // PlayerObjs[a].transform.DOMoveX(currentPlayer.transform.position.x - .5f, .5f);
            //StateUI ����ġ��
            characterStateObjs[a].transform.DOMoveY(ponCharacterStateObjs[a].transform.position.y, .5f);
        }
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < 3; i++) StateTweens[i].Play(); // �ٽ� ���� �������� ä���ִ� Ʈ���� ������
        monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Play(); //�� ���� �������� ä���ִ� Ʈ���� ������
    }


}
