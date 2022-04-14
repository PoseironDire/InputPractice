using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    [HideInInspector] public int currentForm = 0;

    GameObject[] rightButtons = new GameObject[4];

    void Awake()
    {
        for (int i = 0; i < rightButtons.Length; i++)
        {
            rightButtons[i] = GameObject.Find($"Button{i + 1}");
            rightButtons[i].SetActive(false);
        }
    }

    public void ToggleForm(int button)
    {
        rightButtons[button - 1].SetActive(!rightButtons[button - 1].activeInHierarchy);

        for (int i = 0; i < rightButtons.Length; i++)
        {
            if (i != button - 1)
                rightButtons[i].SetActive(false);
        }
    }

    public void DecideForm()
    {
        if (rightButtons[0].activeSelf) currentForm = 1;
        else if (rightButtons[1].activeSelf) currentForm = 2;
        else if (rightButtons[2].activeSelf) currentForm = 3;
        else if (rightButtons[3].activeSelf) currentForm = 4;
        else currentForm = 0;
    }
}