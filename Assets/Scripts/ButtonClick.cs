using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener( () =>
        {
            Selector.shapeName = this.transform.name;
        });
    }
}
