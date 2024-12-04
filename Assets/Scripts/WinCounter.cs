using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinCounter : MonoBehaviour
{
    public TextMeshProUGUI WinCount;
    public GameObject WinScreen;

    private int Count = 0;

    private Menus Menus;

    void Start()
    {
        WinScreen.SetActive(false);
        WinCount.text = $"Enemies Defeated \n {Count} / 4";

        Menus = GetComponent<Menus>();
    }

    public void AddCount()
    {
        Count++;
        WinCount.text = $"Enemies Defeated \n {Count} / 4";

        if (Count > 4)
        {
            Count = 4;
        }

        Invoke("CheckCount", 2.0f);
    }

    void CheckCount()
    {
        if (Count == 4)
        {
            WinScreen.SetActive(true);
            Menus.Pause();
        }
    }
}
