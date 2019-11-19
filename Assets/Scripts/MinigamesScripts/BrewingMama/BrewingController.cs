using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrewingController : MonoBehaviour
{
    [SerializeField] GameObject cauldron;
    [SerializeField] Image mainIngredient;
    [SerializeField] GameObject cover;
    [SerializeField] List<Sprite> ingredients;
    [SerializeField] GameObject loseCanvas;

    [Header("Debug Variables")]
    [SerializeField] private int correctCount = 0;
    [SerializeField] private int inCorrectCount = 0;
    [SerializeField] private Vector3 touchPosWorld;
    [SerializeField] private GameObject ingredient;

    private List<GameObject> children = new List<GameObject>();

    private void Start()
    {
        foreach (Transform c in cover.transform)
            children.Add(c.gameObject);

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

    public void MatchingImages()
    {
        Debug.Log("Matching");
        if(ingredient.GetComponent<SpriteRenderer>().sprite != mainIngredient.sprite)
        {
            Debug.Log("Not Same");
            inCorrectCount += 1;
            Destroy(ingredient);
            ingredient = null;

            if(inCorrectCount >= 2)
            {
                StopCoroutine("TileFlip");
                loseCanvas.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Same");
            correctCount += 1;
            Destroy(ingredient);
            ingredient = null;
            RandomMainIngredient();
        }
    }

    private void RandomMainIngredient()
    {
        int indx = Random.Range(0, ingredients.Count);
        Sprite old = mainIngredient.sprite;

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
                yield return new WaitForSeconds(0.05f);
            }
            while (tmp.color.a < 1)
            {
                Color t = tmp.color;
                t.a += 0.1f;
                tmp.color = t;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
