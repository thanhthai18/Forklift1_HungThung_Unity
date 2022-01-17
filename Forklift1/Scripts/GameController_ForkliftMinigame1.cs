using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_ForkliftMinigame1 : MonoBehaviour
{
    public static GameController_ForkliftMinigame1 instance;

    public bool isWin, isLose, isBegin;
    public Camera mainCamera;
    public Box_ForkliftMinigame1 currentBox;
    public Box_ForkliftMinigame1 boxPrefab;
    public List<Transform> listPosTarget = new List<Transform>();
    public List<Transform> listPosSpawn = new List<Transform>();
    public ForkliftObj_ForkliftMinigame1 forkliftObj;
    public int boxCount;
    public float startSizeCamera;
    public Image panelUI;
    public Text txtBox;
    public Image panelHeart;
    public int heal;
    public GameObject tutorial1,tutorial2;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);

        isWin = false;
        isLose = false;
        boxCount = 30;
        heal = 3;
    }

    private void Start()
    {
        panelUI.gameObject.SetActive(false);
        startSizeCamera = mainCamera.orthographicSize;
        SetSizeCamera();
        mainCamera.orthographicSize *= 2.0f / 5;
        tutorial1.SetActive(false);
        tutorial2.SetActive(false);
        Intro();     
    }

    void SetSizeCamera()
    {
        float f1, f2;
        f1 = 16.0f / 9;
        f2 = Screen.width * 1.0f / Screen.height;
        mainCamera.orthographicSize *= f1 / f2;
    }

    void Intro()
    {
        mainCamera.DOOrthoSize(startSizeCamera, 3).SetEase(Ease.Linear).OnComplete(() =>
        {
            isBegin = true;
            panelUI.gameObject.SetActive(true);
            txtBox.text = boxCount.ToString();
            Tutorial();
        });
    }
    public void Begin()
    {
        ClearFade();
        SpawnBox(Random.Range(0, listPosSpawn.Count));
    }

    void Tutorial()
    {
        tutorial1.SetActive(true);
        tutorial2.SetActive(true);
        Tutorial1();
        Tutorial2();
    }

    void Tutorial1()
    {
        tutorial1.transform.DOMoveX(5.8f, 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            tutorial1.transform.DOMoveX(-5.13f, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (tutorial1.activeSelf)
                {
                    Tutorial1();
                }
            });
        });
    }

    void Tutorial2()
    {
        tutorial2.transform.DOMoveY(3, 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            tutorial2.transform.DOMoveY(-3, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (tutorial2.activeSelf)
                {
                    Tutorial2();
                }
            });
        });
    }

    public void ClearFade()
    {
        listPosTarget.ForEach(s => { s.GetChild(0).gameObject.SetActive(false); });        
    }

    public void SpawnBox(int indexPos)
    {
        listPosTarget[indexPos].GetChild(0).gameObject.SetActive(true);
        currentBox = Instantiate(boxPrefab, listPosSpawn[indexPos].position, Quaternion.identity);
        var checkDirection = (indexPos < 3) ? true : false;
        currentBox.Move(checkDirection, listPosTarget[indexPos].position);
    }

    public void Win()
    {
        Debug.Log("Win");
        isWin = true;
        forkliftObj.transform.DOMoveX(forkliftObj.transform.position.x + 20, 2);
    }

    public void Lose()
    {
        Debug.Log("Thua");
        isLose = true;
    }

}
