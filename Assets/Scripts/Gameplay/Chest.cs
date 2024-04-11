using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoundChest : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(Random.Range(20f, 150f), 60, Random.Range(-150, 25f));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerInteractArea")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("EndGameScene");
        }
    }
}
