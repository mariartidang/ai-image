using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadScene : MonoBehaviour
{
    static public LoadScene Instance { get; private set; }





    private void Awake()
    {
        Instance = this;
    }


}
