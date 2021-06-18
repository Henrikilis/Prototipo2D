using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BookGenerator : MonoBehaviour
{
    [SerializeField]
    private BookPage pagePrefab;
    public List<GameObject> pages = new List<GameObject>();
    public int currentPower = 0;

    private void OnEnable()
    {
        foreach (string name in PowerFactory.GetPowerNames())
        {
            var page = Instantiate(pagePrefab);
            page.gameObject.name = name + " Page";
            page.transform.SetParent(transform);
            page.transform.position = gameObject.transform.position;
            pages.Add(page.gameObject);
        }

        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        pages[0].SetActive(true);
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
        }
    }
}
