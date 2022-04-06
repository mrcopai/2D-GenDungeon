using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class ExitScript : MonoBehaviour
{
    public GameObject[] MoveNextScene;
    [SerializeField]
    GameObject weaponholder;
    [SerializeField]
    private Tilemap ExitMap;
    [SerializeField]
    TooltipScript ToolTip;
    private void Awake()
    {
        weaponholder = GameObject.FindGameObjectWithTag("WeaponHolder");
    }
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        StartCoroutine(GetComponent<TooltipScript>().GetTooltip());
        if (collision.gameObject == GameObject.Find("Player"))
        {
            ToolTip = GetComponent<TooltipScript>();
            MoveNextScene = new GameObject[weaponholder.transform.childCount];
            ToolTip.tooltipText.text = "Press Space to Exit";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                collision.gameObject.transform.position = new Vector3(0,0,0);
                for (int i = 0; i < weaponholder.transform.childCount; i++)
                {
                    GameObject thing = weaponholder.transform.GetChild(i).gameObject;
                    thing.transform.parent = null;
                    MoveNextScene [i] = thing;
                }
                StartCoroutine(LoadYourAsyncScene());
            }
            else
            {
                ToolTip.tooltipText.text = "";
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ToolTip.tooltipText.text = "";
    }
    IEnumerator LoadYourAsyncScene()
    {      
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainHub", LoadSceneMode.Additive);

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        foreach (GameObject item in MoveNextScene)
        {
            try
            {
                SceneManager.MoveGameObjectToScene(item, SceneManager.GetSceneByName("MainHub"));
            }
            catch (System.Exception)
            {
            }
        }
        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
            StartCoroutine(GetComponent<TooltipScript>().GetTooltip());
    }
}
