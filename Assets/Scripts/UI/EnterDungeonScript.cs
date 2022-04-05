using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnterDungeonScript : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = new Vector3(0,0,0);
        SceneManager.LoadScene("Dungeon",LoadSceneMode.Single);
    }
}
