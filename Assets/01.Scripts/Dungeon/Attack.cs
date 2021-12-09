using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Playables;

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
        StartCoroutine(PlayerSelectAttackType()); // �÷��̾��� ���ݹ���� �����Ͽ� ������ �Լ�
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.characterStateObjs[i].transform.position = //�÷��̾ �� ������ ���� �ؿ��� �ٽ� �ö�� UI�� ����
                new Vector3(DungeonUIManager.instance.ponCharacterStateObjs[i].transform.position.x, //�ϴ� �ؿ��� x ��ġ�� ����
                DungeonUIManager.instance.characterStateObjs[i].transform.position.y, DungeonUIManager.instance.characterStateObjs[i].transform.position.z); //�������� �׳� �״��
        }

    }

    public IEnumerator PlayerSelectAttackType() //�÷��̾��� ���ݹ���� �����Ͽ� ������ �Լ�
    {
        Sequence sequence = DOTween.Sequence();
        switch (DungeonUIManager.instance.currentPlayer.GetComponent<Character>().cJobs) //ĳ������ ������ �޾ƿ�
        {
            case Jobs.Knights:
            case Jobs.MagicKnight: // �ٰŸ� �����̸�
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = DungeonUIManager.instance.vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    DungeonUIManager.instance.currentPlayer.GetComponent<PlayableDirector>().Play();

                    yield return new WaitForSeconds(.5f);
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 20f;
                    cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 1f;
                    DungeonUIManager.instance.monsterObj.GetComponent<SpriteRenderer>().color = Color.red;
                    yield return new WaitForSeconds(.1f);
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                    cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
                    DungeonUIManager.instance.monsterObj.GetComponent<SpriteRenderer>().color = Color.white;

                    yield return new WaitForSeconds(.2f);
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 20f;
                    cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 1f;
                    DungeonUIManager.instance.monsterObj.GetComponent<SpriteRenderer>().color = Color.red;
                    yield return new WaitForSeconds(.1f);
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                    cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
                    DungeonUIManager.instance.monsterObj.GetComponent<SpriteRenderer>().color = Color.white;

                    yield return new WaitForSeconds(.6f);

                    DungeonUIManager.instance.AttackEachOther(true); // �������� ����ϱ� ���� �Լ��� ����

                    if (DungeonUIManager.instance.monsterCurrentState.Equals(State.Dead)) // ���� ���� �׾����� �����ؾ��ϱ� ������ ����
                        yield break;

                    StartCoroutine(DungeonUIManager.instance.SetDefaultUI());
                    break;
                }
            case Jobs.Druid:
            case Jobs.Hunter: //������ ������ذŸ�
                break;
            case Jobs.Priest:
            case Jobs.Elementalist: //���Ÿ� �����̸�
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = DungeonUIManager.instance.vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    DungeonUIManager.instance.currentPlayer.GetComponent<PlayableDirector>().Play();

                    yield return new WaitForSeconds(1.15f);
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 20f;
                    cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 1f;
                    DungeonUIManager.instance.monsterObj.GetComponent<SpriteRenderer>().color = Color.red;
                    yield return new WaitForSeconds(.1f);
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                    cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
                    DungeonUIManager.instance.monsterObj.GetComponent<SpriteRenderer>().color = Color.white;


                    if (DungeonUIManager.instance.monsterCurrentState.Equals(State.Dead))// ���� ���� �׾����� �����ؾ��ϱ� ������ ����
                        yield break;


                    StartCoroutine(DungeonUIManager.instance.SetDefaultUI()); // �� �������ϱ� UI �ʱ�ȭ

                    DungeonUIManager.instance.AttackEachOther(true);  // �������� ����ϱ� ���� �Լ��� ����
                    break;
                }
        }
    }
}
