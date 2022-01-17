using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box_ForkliftMinigame1 : MonoBehaviour
{
    public bool isLeft, isOK;
    public ForkliftObj_ForkliftMinigame1 forkliftObj;
    public GameObject shineFXPrefab;
    public Image panelHeart;

    private void Start()
    {
        forkliftObj = GameController_ForkliftMinigame1.instance.forkliftObj;
        panelHeart = GameController_ForkliftMinigame1.instance.panelHeart;
    }

    void CompleteBox()
    {
        GameController_ForkliftMinigame1.instance.boxCount--;
        GameController_ForkliftMinigame1.instance.txtBox.text = GameController_ForkliftMinigame1.instance.boxCount.ToString();
        if(GameController_ForkliftMinigame1.instance.boxCount == 0)
        {
            GameController_ForkliftMinigame1.instance.Win();
        }
        var tmpFx = Instantiate(shineFXPrefab, transform.position, Quaternion.identity);
        tmpFx.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() =>
        {
            Destroy(tmpFx);
        });
        GameController_ForkliftMinigame1.instance.ClearFade();
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
        GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() =>
        {
            Destroy(gameObject);
            if (!GameController_ForkliftMinigame1.instance.isWin && !GameController_ForkliftMinigame1.instance.isLose)
            {
                GameController_ForkliftMinigame1.instance.SpawnBox(Random.Range(0, 6));
            }
        });

    }

    public void Move(bool isLeft, Vector2 posTarget)
    {
        if (!GameController_ForkliftMinigame1.instance.isWin && !GameController_ForkliftMinigame1.instance.isLose)
        {
            if (isLeft)
            {
                transform.DOMoveX(posTarget.x, 2).SetEase(Ease.InQuart).OnComplete(() =>
                {
                    if (isOK)
                    {
                        CompleteBox();
                    }
                    else
                    {
                        GetComponent<BoxCollider2D>().enabled = false;
                        transform.DOMoveX(7.5f, 1).SetEase(Ease.InQuart);
                        GameController_ForkliftMinigame1.instance.ClearFade();
                        panelHeart.transform.GetChild(GameController_ForkliftMinigame1.instance.heal - 1).GetComponent<Image>().DOFade(0, 1);
                        GameController_ForkliftMinigame1.instance.heal--;
                        if (GameController_ForkliftMinigame1.instance.heal == 0)
                        {
                            GameController_ForkliftMinigame1.instance.Lose();
                        }
                        GetComponent<SpriteRenderer>().DOFade(0, 2).OnComplete(() =>
                        {
                            Destroy(gameObject);
                            GameController_ForkliftMinigame1.instance.SpawnBox(Random.Range(0, 6));
                        });
                    }
                });
            }
            else
            {
                transform.DOMoveX(posTarget.x, 2).SetEase(Ease.InQuart).OnComplete(() =>
                {
                    if (isOK)
                    {
                        CompleteBox();
                    }
                    else
                    {
                        GetComponent<BoxCollider2D>().enabled = false;
                        transform.DOMoveX(-7.5f, 1).SetEase(Ease.InQuart);
                        GameController_ForkliftMinigame1.instance.ClearFade();
                        panelHeart.transform.GetChild(GameController_ForkliftMinigame1.instance.heal - 1).GetComponent<Image>().DOFade(0, 1);
                        GameController_ForkliftMinigame1.instance.heal--;
                        if (GameController_ForkliftMinigame1.instance.heal == 0)
                        {
                            GameController_ForkliftMinigame1.instance.Lose();
                        }
                        GetComponent<SpriteRenderer>().DOFade(0, 2).OnComplete(() =>
                        {
                            Destroy(gameObject);
                            GameController_ForkliftMinigame1.instance.SpawnBox(Random.Range(0, 6));
                        });
                    }
                });
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOK = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOK = false;
        }
    }
}
