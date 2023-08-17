using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public bool LoadMainMenu;
    public GameObject win;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (LoadMainMenu)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                
               // win.transform.localScale = new Vector2(0, 0);
                win.SetActive(true);
               // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
  

}
