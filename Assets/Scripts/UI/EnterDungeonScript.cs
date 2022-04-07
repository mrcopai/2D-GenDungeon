using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnterDungeonScript : MonoBehaviour
{
    public GameObject[] MoveNextScene;   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            bool hasweapon = false;
            for (int i = 0; i < Inventory.isFull.Length; i++)
            {
                if (Inventory.isFull[i] == true)
                {
                    hasweapon = true;
                }
            }
            if (hasweapon == true)
            {
                StartCoroutine(LoadYourAsyncScene());
                collision.gameObject.transform.position = new Vector3(0, 0, 0);
            }
        }
    }
    IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Dungeon", LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        foreach (GameObject item in MoveNextScene)
        {
            SceneManager.MoveGameObjectToScene(item, SceneManager.GetSceneByName("Dungeon"));
        }
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
