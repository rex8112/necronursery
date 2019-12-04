using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BrewingController : MonoBehaviour
{
    [SerializeField] resourceManager rm;
    [SerializeField] GameObject cauldron;
    [SerializeField] Image mainIngredient;
    [SerializeField] GameObject cover;
    [SerializeField] List<GameObject> templates;
    [SerializeField] List<Sprite> allIngredients;
    [SerializeField] List<GameObject> progressObjects;
    [SerializeField] GameObject loseCanvas;
    [SerializeField] GameObject winCanvas;

    [Header("Debug Variables")]
    [SerializeField] private int progressCount = 0;
    [SerializeField] private int correctCount = 0;
    [SerializeField] private int inCorrectCount = 0;
    [SerializeField] private Vector3 touchPosWorld;
    [SerializeField] private GameObject ingredient;
    [SerializeField] private List<Sprite> ingredients;

    private List<GameObject> children = new List<GameObject>();

    private void Awake()
    {
        List<Sprite> tmp = new List<Sprite>();
        foreach (Sprite s in allIngredients)
            tmp.Add(s);

        for (int i = 0; i < 4; i++)
        {
            int indx = Random.Range(0, tmp.Count);

            ingredients.Add(tmp[indx]);
            tmp.RemoveAt(indx);
        }
    }

    private void Start()
    {
        foreach (Transform c in cover.transform)
            children.Add(c.gameObject);

        GenerateNew();
        StartCoroutine("TileFlip");
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
                    if (hit.collider.CompareTag("ingredient"))
                    {
                        ingredient = Instantiate(hit.collider.gameObject);
                        ingredient.transform.position = touchPosWorld2D;
                    }
                    break;
                case TouchPhase.Moved:
                    touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                    if (ingredient != null)
                        ingredient.transform.position = touchPosWorld2D;
                    break;
                case TouchPhase.Ended:
                    if (ingredient)
                        Destroy(ingredient);
                    ingredient = null;
                    break;
            }
        }
    }

    public void GenerateNew()
    {
        int count = 0;
        foreach (GameObject t in templates)
        {
            t.GetComponent<SpriteRenderer>().sprite = ingredients[count];
            count++;
        }

        RandomMainIngredient();
    }

    public void MatchingImages()
    {
        if(ingredient.GetComponent<SpriteRenderer>().sprite != mainIngredient.sprite)
        {
            inCorrectCount += 1;
            Progress(false);
            Destroy(ingredient);
            ingredient = null;

            if(inCorrectCount >= 2)
            {
                Loser();
            }
        }
        else
        {
            correctCount += 1;
            Progress(true);
            Destroy(ingredient);
            ingredient = null;

            if(correctCount >= 4)
            {
                Winner();
            }
            else
                RandomMainIngredient();

        }
    }

    public void Progress(bool success)
    {
        GameObject obj = progressObjects[progressCount];
        GameObject child;
        if (success)
        {
            child = obj.transform.GetChild(0).gameObject;
            child.SetActive(true);
        }
        else
        {
            child = obj.transform.GetChild(1).gameObject;
            child.SetActive(true);
        }
        progressCount++;
    }

    public void Loser()
    {
        StopCoroutine("TileFlip");
        loseCanvas.SetActive(true);
    }

    public void Winner()
    {
        int magic, teeth;
        StopCoroutine("TileFlip");
        winCanvas.SetActive(true);

        magic = Random.Range(2, 7);
        teeth = Random.Range(4, 11);

        rm.resources.Find(r => r.name == "Magic").Add(magic);
        rm.resources.Find(r => r.name == "Teeth").Add(teeth);
    }

    private void RandomMainIngredient()
    {
        int indx = Random.Range(0, ingredients.Count);
        Sprite old = mainIngredient.sprite;
        ingredients.Remove(old);

        while (mainIngredient.sprite == old)
        {
            indx = Random.Range(0, ingredients.Count);
            mainIngredient.sprite = ingredients[indx];
        }
    }

    IEnumerator TileFlip()
    {
        while (true)
        {
            int indx = Random.Range(0, children.Count);
            Image tmp = children[indx].GetComponent<Image>();

            while (tmp.color.a > 0)
            {
                Color t = tmp.color;
                t.a -= 0.1f;
                tmp.color = t;
                yield return new WaitForSeconds(0.016f);
            }
            while (tmp.color.a < 1)
            {
                Color t = tmp.color;
                t.a += 0.1f;
                tmp.color = t;
                yield return new WaitForSeconds(0.016f);
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
