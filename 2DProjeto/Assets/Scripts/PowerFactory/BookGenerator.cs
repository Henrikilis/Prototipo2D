using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BookGenerator : MonoBehaviour
{
    [SerializeField]
    private BookPage powerPrefab;
    public List<GameObject> pages = new List<GameObject>();
    public int currentPower = 0;
    public string[] powerName;

    public Transform[] position;
    public GameObject pagePrefab;

    private void OnEnable()
    {
        int i = 0;
        foreach (string name in PowerFactory.GetPowerNames())
        {
            if (0 == i % 2)
            {
                var prefab = Instantiate(pagePrefab);
                prefab.transform.SetParent(transform);
                prefab.gameObject.name = ("Page "+ i);
                pages.Add(prefab.gameObject);

                var page = Instantiate(powerPrefab);
                page.gameObject.name = name;
                page.transform.SetParent(prefab.transform);
                page.transform.position = position[0].position;
                page.transform.localScale = new Vector3(1,1,1);
            }
            if (0 < i % 2)
            {
                GameObject prefab = GameObject.Find("Page " + (i - 1));

                var page = Instantiate(powerPrefab);
                page.gameObject.name = name;
                page.transform.SetParent(prefab.transform);
                page.transform.position = position[1].position;

                prefab.gameObject.name = ("Page " + (i / 2));
            }
            i++;
        }

        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        pages[0].SetActive(true);
        checkNames(pages[0]);
    }

    public void SwapLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            pages[currentPower].SetActive(false);
            if(currentPower > 0)
            currentPower--;
            else { currentPower = (pages.Count - 1); }
            pages[currentPower].SetActive(true);
            //DimensionSwap();
            checkNames(pages[currentPower]);
        }
    }
    public void SwapRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            pages[currentPower].SetActive(false);
            if (currentPower < (pages.Count - 1))
                currentPower++;
            else { currentPower = 0; }
            pages[currentPower].SetActive(true);
            //DimensionSwap();
            checkNames(pages[currentPower]);
        }
    }
    public void Power1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PowerFactory.GetPower(powerName[0]).Process();
        }
    }
    public void Power2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PowerFactory.GetPower(powerName[1]).Process();
        }
    }

    private void checkNames(GameObject page)
    {
        int i = 0;
        foreach (Transform child in page.transform)
        {
            powerName[i] = child.name;
            i++;
        }
    }
}
