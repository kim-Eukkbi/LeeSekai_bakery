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
    private GameObject bullet; //원거리 공격할때 보여줄 오브젝트
    public void Start()
    {
        DungeonUIManager.instance.fightbuttons[0].GetComponent<Button>().onClick.AddListener(() => //공격버튼을 눌렀을때 뭘 할지 추가 해줌
        {
            StartCoroutine(AttackCo()); // 버튼이 눌렸다면 플레이어가 때리는 함수를 실행함
        });
    }
    public IEnumerator AttackCo() //플레이어가 때리는 함수
    {
        DungeonUIManager.instance.DownFightUI(); //플레이어가 때리기 위해 필요없는 UI를 밑으로 내림
        //fightPanel.transform.DOMoveY(characterStatePanel.transform.position.y - 5f, .8f);
        DungeonUIManager.instance.currentCharacterStateUI.transform.DOMoveY(DungeonUIManager.instance.currentCharacterStateUI.transform.position.y - 5, .8f); //선택된 캐릭터의 UI도 밑으로 내림
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.ponCharacterStateObjs[i].SetActive(true); //정렬을 위한 모든 비어있는 오브젝트를 켜서 정렬 준비
        }
        yield return new WaitForSeconds(.8f);
        StartCoroutine(PlayerSelectAttackType()); // 플레이어의 공격방식을 선택하여 때리는 함수
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.characterStateObjs[i].transform.position = //플레이어가 다 때리기 전에 밑에서 다시 올라올 UI를 정렬
                new Vector3(DungeonUIManager.instance.ponCharacterStateObjs[i].transform.position.x, //일단 밑에서 x 위치만 수정
                DungeonUIManager.instance.characterStateObjs[i].transform.position.y, DungeonUIManager.instance.characterStateObjs[i].transform.position.z); //나머지는 그냥 그대로
        }

    }

    public IEnumerator PlayerSelectAttackType() //플레이어의 공격방식을 선택하여 때리는 함수
    {
        Sequence sequence = DOTween.Sequence();
        switch (DungeonUIManager.instance.currentPlayer.GetComponent<Character>().cJobs) //캐릭터의 직업을 받아옴
        {
            case Jobs.Knights:
            case Jobs.MagicKnight: // 근거리 공격이면
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

                    DungeonUIManager.instance.AttackEachOther(true); // 데미지를 계산하기 위한 함수를 실행

                    if (DungeonUIManager.instance.monsterCurrentState.Equals(State.Dead)) // 만약 적이 죽었으면 중지해야하기 때문에 리턴
                        yield break;

                    StartCoroutine(DungeonUIManager.instance.SetDefaultUI());
                    break;
                }
            case Jobs.Druid:
            case Jobs.Hunter: //윤서가 만들어준거면
                break;
            case Jobs.Priest:
            case Jobs.Elementalist: //원거리 공격이면
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


                    if (DungeonUIManager.instance.monsterCurrentState.Equals(State.Dead))// 만약 적이 죽었으면 중지해야하기 때문에 리턴
                        yield break;


                    StartCoroutine(DungeonUIManager.instance.SetDefaultUI()); // 다 때렸으니까 UI 초기화

                    DungeonUIManager.instance.AttackEachOther(true);  // 데미지를 계산하기 위한 함수를 실행
                    break;
                }
        }
    }
}
