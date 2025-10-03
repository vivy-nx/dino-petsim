using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void onStartClick()
    {
        SceneManager.LoadSceneAsync("dinoSim");
    }

    public void onLevelsClick()
    {
        SceneManager.LoadSceneAsync("Terrain");
    }


}
