using System;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class Selector : MonoBehaviour
{
    public static string shapeName;

    [SerializeField] TMP_Text selectionWarningText;
    CanvasGroup faderCanvas;
    [SerializeField] float to = 1, time = 1;

    [SerializeField] List<NetShapes> netShapes;
    [SerializeField] Dictionary<string, GameObject> dict = new Dictionary<string, GameObject>();

    RaycastHit hit;
    Vector3 mouseWorldPosition;

    void Start()
    {
        faderCanvas = selectionWarningText.GetComponent<CanvasGroup>();
        faderCanvas.alpha = 0f;

        foreach(NetShapes shape in netShapes)
        {
            dict.Add(shape.name, shape.obj);
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, transform.forward * 1000, Color.red);

            if(Input.GetMouseButtonDown(0))
            {
                if(String.IsNullOrEmpty(shapeName))
                {
                    FadeIn();
                    return;
                }
                Instantiate(dict[shapeName], hit.point, Quaternion.LookRotation(hit.transform.forward, hit.normal));
            }
        }
    }

    private void FadeIn()
    {
        LeanTween.alphaCanvas(faderCanvas, to, time).setOnComplete(FadeOut);
    }

    private void FadeOut()
    {
        LeanTween.alphaCanvas(faderCanvas, 0, time);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}

[Serializable]
public class NetShapes
{
    public string name;
    public GameObject obj;
}
