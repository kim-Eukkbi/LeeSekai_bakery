using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Attack : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet; //���Ÿ� �����Ҷ� ������ ������Ʈ
    public void Start()
    {
        DungeonUIManager.instance.fightbuttons[0].GetComponent<Button>().onClick.AddListener(() => //���ݹ�ư�� �������� �� ���� �߰� ����
        {
            StartCoroutine(AttackCo()); // ��ư�� ���ȴٸ� �÷��̾ ������ �Լ��� ������
        });
    }
    public IEnumerator AttackCo() //�÷��̾ ������ �Լ�
    {
        DungeonUIManager.instance.DownFightUI(); //�÷��̾ ������ ���� �ʿ���� UI�� ������ ����
        //fightPanel.transform.DOMoveY(characterStatePanel.transform.position.y - 5f, .8f);
        DungeonUIManager.instance.currentCharacterStateUI.transform.DOMoveY(DungeonUIManager.instance.currentCharacterStateUI.transform.position.y - 5, .8f); //���õ� ĳ������ UI�� ������ ����
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.ponCharacterStateObjs[i].SetActive(true); //������ ���� ��� ����ִ� ������Ʈ�� �Ѽ� ���� �غ�
        }
        yield return new WaitForSeconds(.8f);
        PlayerSelectAttackType(); // �÷��̾��� ���ݹ���� �����Ͽ� ������ �Լ�
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.characterStateObjs[i].transform.position = //�÷��̾ �� ������ ���� �ؿ��� �ٽ� �ö�� UI�� ����
                new Vector3(DungeonUIManager.instance.ponCharacterStateObjs[i].transform.position.x, //�ϴ� �ؿ��� x ��ġ�� ����
                DungeonUIManager.instance.characterStateObjs[i].transform.position.y, DungeonUIManager.instance.characterStateObjs[i].transform.position.z); //�������� �׳� �״��
        }

    }

    public void PlayerSelectAttackType() //�÷��̾��� ���ݹ���� �����Ͽ� ������ �Լ�
    {
        Sequence sequence = DOTween.Sequence();
        switch(DungeonUIManager.instance.currentPlayer.GetComponent<Character>().cJobs) //ĳ������ ������ �޾ƿ�
        {
            case Jobs.Knights:
            case Jobs.MagicKnight: // �ٰŸ� �����̸�
                {
                    DungeonUIManager.instance.AttackEachOther(true); // �������� ����ϱ� ���� �Լ��� ����
                    Vector3 originPos = DungeonUIManager.instance.currentPlayer.transform.position; //�÷��̾��� ��ġ�� ����
                    sequence.Append(DungeonUIManager.instance.currentPlayer.transform.DOMoveX(DungeonUIManager.instance.monsterObj.transform.position.x, .3f)).SetEase(Ease.OutCirc); //������ ����
                    sequence.Append(DungeonUIManager.instance.currentPlayer.transform.DOMoveX(originPos.x, .5f).SetEase(Ease.InBack).OnComplete(() => //�ٽ� ���� ��ġ�� ���ƿ�
                    {
                        if (DungeonUIManager.instance.monsterCurrentState.Equals(State.Dead)) // ���� ���� �׾����� �����ؾ��ϱ� ������ ����
                            return;

                        StartCoroutine(DungeonUIManager.instance.SetDefaultUI()); // �� ������ UI �ʱ�ȭ
                    }));
                    sequence.Insert(.2f, Camera.main.DOShakeRotation(.1f, 5f));  //ī�޶� ����ũ
                    break;
                }
            case Jobs.Druid:
            case Jobs.Hunter: //������ ������ذŸ�
                break;
            case Jobs.Priest:
            case Jobs.Elementalist: //���Ÿ� �����̸�
                {
                    DungeonUIManager.instance.AttackEachOther(true); // �������� ����ϱ� ���� �Լ��� ����
                    GameObject currB = Instantiate(bullet, DungeonUIManager.instance.currentPlayer.transform); //���� ������Ʈ�� ����
                    sequence.Append(currB.transform.DOMove(DungeonUIManager.instance.monsterObj.transform.position, .3f)).SetEase(Ease.OutCirc).OnComplete(() => //������Ʈ ������
                    {
                        Destroy(currB, .3f); // ������ �� �̵��� ����
                        if (DungeonUIManager.instance.monsterCurrentState.Equals(State.Dead))// ���� ���� �׾����� �����ؾ��ϱ� ������ ����
                            return;
                        StartCoroutine(DungeonUIManager.instance.SetDefaultUI()); // �� �������ϱ� UI �ʱ�ȭ
                    });
                    
                    sequence.Insert(.2f, Camera.main.DOShakeRotation(.1f, 5f));  //ī�޶� ����ũ
                    break;
                } 
        }
    }
}
