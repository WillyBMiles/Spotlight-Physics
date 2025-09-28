using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && Time.time > .35f)
        {
            SceneManager.LoadScene(1);
        }
    }
}
