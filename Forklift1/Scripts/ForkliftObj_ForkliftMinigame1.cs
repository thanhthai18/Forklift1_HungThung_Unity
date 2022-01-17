using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkliftObj_ForkliftMinigame1 : MonoBehaviour
{
    public bool isLeft, isRight;
    public Camera mainCamera;
    public Vector2 startMousePos;
    public Vector2 endMousePos;


    private void Update()
    {
        if (!GameController_ForkliftMinigame1.instance.isLose && !GameController_ForkliftMinigame1.instance.isWin && GameController_ForkliftMinigame1.instance.isBegin)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition) - mainCamera.transform.position;
            }
            if (Input.GetMouseButtonUp(0))
            {
                endMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition) - mainCamera.transform.position;

                if (startMousePos.y <= -2f)
                {
                    if (startMousePos.x == endMousePos.x)
                    {
                        return;
                    }
                    else if (startMousePos.x + 2 < endMousePos.x && Mathf.Abs(startMousePos.y - endMousePos.y) < 8)
                    {
                        transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
                        isLeft = false;
                    }
                    else if (startMousePos.x > endMousePos.x + 2 && Mathf.Abs(startMousePos.y - endMousePos.y) < 8)
                    {
                        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        isLeft = true;
                    }
                }

                if (GameController_ForkliftMinigame1.instance.tutorial1.activeSelf)
                {
                    GameController_ForkliftMinigame1.instance.tutorial1.SetActive(false);
                    GameController_ForkliftMinigame1.instance.tutorial1.transform.DOKill();
                    GameController_ForkliftMinigame1.instance.tutorial2.SetActive(false);
                    GameController_ForkliftMinigame1.instance.tutorial2.transform.DOKill();
                    GameController_ForkliftMinigame1.instance.Begin();
                }
            }
            
        }
    }


}
