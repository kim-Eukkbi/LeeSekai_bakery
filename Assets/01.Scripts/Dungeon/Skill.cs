using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Skill : MonoBehaviour
{
    public Button backBtn;
    public void Start()
    {
        backBtn.onClick.AddListener(() =>
        {
            StartCoroutine(SkillUIDown());
        });
        DungeonUIManager.instance.buttons[1].GetComponent<Button>().onClick.AddListener(() =>
        {
            StartCoroutine(SkillUISet());
        });
    }

    public IEnumerator SkillUISet()
    {
        DungeonUIManager.instance.DownUI();
        yield return new WaitForSeconds(.8f);
        DungeonUIManager.instance.skillPanel.transform.DOMoveY
            (DungeonUIManager.instance.ponCharacterStateObjs[0].GetComponentInParent<Transform>().position.y, .5f);
    }

    public IEnumerator SkillUIDown()
    {
        DungeonUIManager.instance.skillPanel.transform.DOMoveY
            (DungeonUIManager.instance.fightPanel.transform.position.y, .5f);
        yield return new WaitForSeconds(.8f);
        Sequence Uiseq = DOTween.Sequence();
        foreach (var item in DungeonUIManager.instance.buttons) item.GetComponent<Button>().interactable = false;
        Uiseq.Append(DungeonUIManager.instance.buttons[0].transform.DOMoveY(DungeonUIManager.instance.buttons[0].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.1f, DungeonUIManager.instance.buttons[1].transform.DOMoveY(DungeonUIManager.instance.buttons[1].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.2f, DungeonUIManager.instance.buttons[2].transform.DOMoveY(DungeonUIManager.instance.buttons[2].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.3f, DungeonUIManager.instance.buttons[3].transform.DOMoveY(DungeonUIManager.instance.buttons[3].transform.position.y + 3.1f, .5f)).OnComplete(() =>
        { foreach (var item in DungeonUIManager.instance.buttons) item.GetComponent<Button>().interactable = true; });
    }

}
