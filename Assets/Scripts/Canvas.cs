using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{
    static Canvas instance;
    int max;
    int current;
    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    CanvasGroup group;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Cubes Sorted: " + (DeliveryTrigger.numTriggers - DeliveryTrigger.triggers.Count) + "/" + DeliveryTrigger.numTriggers;

        if (group.alpha == 1)
        {
            if (Input.GetMouseButtonUp(0))
            {
                var s = SceneManager.GetActiveScene();
                try
                {
                    SceneManager.GetSceneAt(s.buildIndex + 1);
                    SceneManager.LoadScene(s.buildIndex + 1);
                }
                catch
                {
                    SceneManager.LoadScene(0);
                }

            }
        }
    }

    public static void SetCubes(int amount, int max = -1)
    {
        if (max != -1)
        {
            instance.max = max;
        }
        instance.current = amount;
        instance.text.text = "Cubes Sorted: " + instance.current + "/" + instance.max;
    }
    public static void SetWin()
    {
        instance.group.alpha = 1;
    }
}
