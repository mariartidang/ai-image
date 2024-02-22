using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public int sceneNumber;

    public void SceneLoader()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
