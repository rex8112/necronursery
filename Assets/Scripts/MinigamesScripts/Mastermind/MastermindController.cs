/*
Game things
- a function to check players code (via button)
    -if correct spot/color lit candle
    -if correct color, wrong spot smoking candle
-counter for number of tries (12)
    -maybe have a slider of some sort that shows a strength meter of connection to the other side???????????????????????????????????????????????
-list of prev guesses with outcome
-Win function

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastermindController : MonoBehaviour
{
    [SerializeField] private Vector3 touchPosWorld;
    public List<Sprite> PossibleRunes;
    [SerializeField] List<Sprite> CorrectCode;
    [SerializeField] List<Sprite> PlayerCode;
    private int candleSignal = 0;
    [SerializeField] private GameObject runes;
    private GameObject Candle;
    public Sprite defaultSprite;
    //[SerializeField] bool firstTime;
    public List<GameObject> flames;
    public List<GameObject> smoke;

    // Start is called before the first frame update
    void Start()
    {
        //firstTime = true;
        for(int i = 0; i < 4; i++)
        {
            Candle = flames[i];
            Hide(Candle);
            Candle = smoke[i];
            Hide(Candle);
        }
        candleSignal = 0;
        GenerateCode();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosWorld2D;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                    RaycastHit2D hit = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
                    if (hit.collider.CompareTag("Runes"))
                    {
                        runes = Instantiate(hit.collider.gameObject);
                        runes.transform.position = touchPosWorld2D;
                    }
                    break;
                case TouchPhase.Moved:
                    touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                    if (runes != null)
                        runes.transform.position = touchPosWorld2D;
                    break;
                case TouchPhase.Ended:
                    if (runes)
                        Destroy(runes);
                    runes = null;
                    break;
            }
        }

        
    }

    private void GenerateCode()
    {
         for (int i = 0; i < 4; i++)
        {
            int randomRune = Random.Range(0, 6);
            CorrectCode[i] = PossibleRunes[randomRune];
        }
    }

    private void GetPlayerCode()
    {
        for(int i = 0; i < 4; i++)
        {
            PlayerCode[i] = GameObject.Find("POI_" +(i + 1)).GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void CodeChecker(bool Submited) //this needs to be tested------------------------------------------------------
    {
        if(Submited)
        {
            //if (firstTime)
            //{
            //    firstTime = false;
            //    return;
            //}
            //else
            //{
                for (int i = 0; i < 4; i++)
                {
                    Candle = flames[i];
                    Hide(Candle);
                    Candle = smoke[i];
                    Hide(Candle);
                    candleSignal = 0;
//                    GameObject.Find("POI_" + (i+1)).GetComponent<SpriteRenderer>().sprite = defaultSprite;
                }
            //}

            GetPlayerCode();
            for(int n = 0; n < 4; n++)
            {
                for(int pp = 0; pp < 4; pp++) 
                {
                    if(PlayerCode[n] == CorrectCode[pp] && n == pp)
                    {
                        Candle = flames[candleSignal];
                        Show(Candle);
                        candleSignal++;
                    }
                    else if (PlayerCode[n] == CorrectCode[pp] && n != pp)
                    {
                        Candle = smoke[candleSignal];
                        Show(Candle);
                        candleSignal++;
                    }
                    else
                    {
                        break;
                    }
                }
                  
            }
        }
    }
    public void Show(GameObject TheObject)
    {
        TheObject.SetActive(true);
    }

    public void Hide(GameObject TheObject)
    {
        TheObject.SetActive(false);
    }
}
