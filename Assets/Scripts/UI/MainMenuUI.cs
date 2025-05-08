using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartClicked()
    {
        SceneManager.LoadScene("Game");   // ����ڭ̷|��D�C�������R�W�� Game.unity
    }

    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �b�s�边����B��
#else
        Application.Quit(); // ���]��h�X�C��
#endif
    }
}

