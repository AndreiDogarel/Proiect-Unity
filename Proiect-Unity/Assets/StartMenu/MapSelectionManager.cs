using UnityEngine;
using UnityEngine.UI;

public class MapSelectionManager : MonoBehaviour
{
    public Button mapButton1; 
    public Button mapButton2;
    public Button mapButton3; 
    public Button mapButton4;

    public Image V1; 
    public Image V2;
    public Image V3; 
    public Image V4;

    private void Start()
    {
        V1.gameObject.SetActive(false);
        V2.gameObject.SetActive(false);
        V3.gameObject.SetActive(false);
        V4.gameObject.SetActive(false);

        mapButton1.onClick.AddListener(() => SelectMap(1));
        mapButton2.onClick.AddListener(() => SelectMap(2));
        mapButton3.onClick.AddListener(() => SelectMap(3));
        mapButton4.onClick.AddListener(() => SelectMap(4));
    }

    private void SelectMap(int mapIndex)
    {
        V1.gameObject.SetActive(false);
        V2.gameObject.SetActive(false);
        V3.gameObject.SetActive(false);
        V4.gameObject.SetActive(false);

        switch (mapIndex)
        {
            case 1:
                V1.gameObject.SetActive(true);
                break;
            case 2:
                V2.gameObject.SetActive(true);
                break;
            case 3:
                V3.gameObject.SetActive(true);
                break;
            case 4:
                V4.gameObject.SetActive(true);
                break;
        }
    }
}

