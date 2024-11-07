using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public GameObject button1; // Drag your Button 1 GameObject here
    public GameObject button2; // Drag your Button 2 GameObject here

    void Start()
    {
        // Pastikan kursor terlihat dan tidak terkunci di awal
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Fungsi untuk membuka scene tertentu tanpa menghancurkan main menu
    public void LoadSceneAdditive(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            // Menyembunyikan tombol sebelum memuat scene
            button1.SetActive(false);
            button2.SetActive(false);
            StartCoroutine(LoadSceneAsync(sceneName));
        }
    }

    // Coroutine untuk memuat scene secara asinkron
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Tunggu hingga scene selesai dimuat
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Pastikan kursor tetap terlihat dan tidak terkunci setelah scene dimuat
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Fungsi untuk kembali ke scene main menu dengan menghancurkan scene lain
    public void ReturnToMainMenu()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "MainMenu" && scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }

        // Menampilkan kembali tombol setelah kembali ke main menu
        button1.SetActive(true);
        button2.SetActive(true);

        // Pastikan kursor tetap terlihat dan tidak terkunci
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
