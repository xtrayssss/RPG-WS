using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEventHandlerStartMenu : MonoBehaviour
{
    private const int INDEX_MAIN_SCENE = 1;
    
    public GameObject startCanvas;
    public GameObject selectHardLevel;

    public void OnPlayButton()
    {
        startCanvas.SetActive(false);

        selectHardLevel.SetActive(true);
        //SceneManager.LoadScene(1);
    }

    public void OnRegistrateButton()
    {
        
    }

}
