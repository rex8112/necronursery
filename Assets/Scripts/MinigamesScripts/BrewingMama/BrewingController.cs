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

    private List<GameObject> children = new List<GameObject>();

    private void Start()
    {
        foreach (Transform c in cover.transform)
            children.Add(c.gameObject);

        StartCoroutine("TileFlip");
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
