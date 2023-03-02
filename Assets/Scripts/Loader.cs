using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    public enum Scene
    {
        MainMenuScene,
        GaneScene, // Spelling mistake I am leaving in because it's funny :)
        LoadingScene
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene)
    {

        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());

    }

    // Jank AF
    public static void LoaderCallback() 
    { 

        SceneManager.LoadScene(Loader.targetScene.ToString());

    } 

}
