using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    [HideInInspector] public int currentForm = 0;

    public Transform[] rightButtons = new Transform[4];

    void Awake()
    {
        for (int i = 0; i < rightButtons.Length; i++)
        {
            rightButtons[i].gameObject.SetActive(false);
        }
    }

    public void ToggleForm(int button)
    {
        rightButtons[button - 1].gameObject.SetActive(!rightButtons[button - 1].gameObject.activeInHierarchy);

        for (int i = 0; i < rightButtons.Length; i++)
        {
            if (i != button - 1)
                rightButtons[i].gameObject.SetActive(false);
        }
    }

    public void DecideForm()
    {
        if (rightButtons[0].gameObject.activeSelf) currentForm = 1;
        else if (rightButtons[1].gameObject.activeSelf) currentForm = 2;
        else if (rightButtons[2].gameObject.activeSelf) currentForm = 3;
        else if (rightButtons[3].gameObject.activeSelf) currentForm = 4;
        else currentForm = 0;
    }
}