using UnityEngine;

public class MainMenu : MonoBehaviour {

    public void PlayGame() {
        Loader.Load(Loader.Scene.MainScene);
    }

    public void Quit() {
        Application.Quit();
    }
}