using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Transform playButton; /**/ public Transform optionsButton; /**/ public Transform quitButton; /**/ public Transform backButton;
    bool playSelected = false; /**/ bool optionsSelected = false; /**/ bool quitSelected = false; /**/ bool backSelected = false;

    [Range(0, 1)] public float smoothIn = 0.05f;
    [Range(0, 1)] public float smoothOut = 0.01f;

    public Transform panel;

    [Space]

    public UnityEngine.Rendering.Universal.Light2D light2D;
    bool scale; /**/ float lerp;

    [Range(0, 15)] public float maxLerp;
    [Range(0, 15)] public float minLerp;

    AudioManager audioManager;

    void Awake()
    {
        lerp = light2D.pointLightOuterRadius;

        audioManager = FindObjectOfType<AudioManager>();
    }

    //Play Button
    public void PlayClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        audioManager.Play("ButtonPress");
        playSelected = false;
        PanelEffect();
    }
    public void PlayEnter()
    {
        playSelected = true;
        audioManager.Play("ButtonSelect");
    }
    public void PlayExit()
    {
        playSelected = false;
    }

    //Options Button
    public void OptionsClick()
    {
        optionsSelected = false;
        audioManager.Play("ButtonPress");
        PanelEffect();
    }
    public void OptionsEnter()
    {
        optionsSelected = true;
        audioManager.Play("ButtonSelect");
    }
    public void OptionsExit()
    {
        optionsSelected = false;
    }

    //Quit Button
    public void QuitClick()
    {
        Application.Quit();
        quitSelected = false;
        audioManager.Play("ButtonPress");
        PanelEffect();
    }
    public void QuitEnter()
    {
        quitSelected = true;
        audioManager.Play("ButtonSelect");
    }
    public void QuitExit()
    {
        quitSelected = false;
    }

    //Back Button
    public void BackClick()
    {
        backSelected = false;
        audioManager.Play("ButtonPress");
        PanelEffect();
    }
    public void BackEnter()
    {
        backSelected = true;
        audioManager.Play("ButtonSelect");
    }
    public void BackExit()
    {
        backSelected = false;
    }

    //Panel
    void PanelEffect()
    {
        panel.transform.localScale += new Vector3(1, 0);
    }

    Vector3 playVel = Vector3.zero; /**/ Vector3 optionsVel = Vector3.zero; /**/ Vector3 quitVel = Vector3.zero; /**/ Vector3 backVel = Vector3.zero; /**/ Vector3 panelVel = Vector3.zero;

    void FixedUpdate()
    {
        //Options Button
        if (playSelected) playButton.transform.localScale = Vector3.SmoothDamp(playButton.transform.localScale, new Vector3(1.25f, 1.25f), ref playVel, smoothIn);
        else playButton.transform.localScale = Vector3.SmoothDamp(playButton.transform.localScale, new Vector3(1, 1), ref playVel, smoothOut);

        //Options Button
        if (optionsSelected) optionsButton.transform.localScale = Vector3.SmoothDamp(optionsButton.transform.localScale, new Vector3(1.25f, 1.25f), ref playVel, smoothIn);
        else optionsButton.transform.localScale = Vector3.SmoothDamp(optionsButton.transform.localScale, new Vector3(1, 1), ref optionsVel, smoothOut);

        //Quit Button
        if (quitSelected) quitButton.transform.localScale = Vector3.SmoothDamp(quitButton.transform.localScale, new Vector3(1.25f, 1.25f), ref playVel, smoothIn);
        else quitButton.transform.localScale = Vector3.SmoothDamp(quitButton.transform.localScale, new Vector3(1, 1), ref quitVel, smoothOut);

        //Back Button
        if (backSelected) backButton.transform.localScale = Vector3.SmoothDamp(backButton.transform.localScale, new Vector3(1.25f, 1.25f), ref playVel, smoothIn);
        else backButton.transform.localScale = Vector3.SmoothDamp(backButton.transform.localScale, new Vector3(1, 1), ref backVel, smoothOut);

        //Panel Effect
        panel.transform.localScale = Vector3.SmoothDamp(panel.transform.localScale, new Vector3(1, 1), ref panelVel, 0.2f);

        //Light Effect
        if (lerp <= minLerp) scale = true; /**/ if (lerp >= maxLerp) scale = false;
        if (scale) lerp = Mathf.Lerp(lerp, maxLerp + 1, 0.01f); /**/ else lerp = Mathf.Lerp(lerp, minLerp - 1, 0.02f);
        light2D.pointLightOuterRadius = lerp;
    }
}
