using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartClicked()
    {
        SceneManager.LoadScene("Game");   // 之後我們會把主遊戲場景命名為 Game.unity
    }

    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 在編輯器停止運行
#else
        Application.Quit(); // 打包後退出遊戲
#endif
    }
}

