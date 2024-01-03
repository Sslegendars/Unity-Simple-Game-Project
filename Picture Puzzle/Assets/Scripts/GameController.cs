using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Transform[] _pictures;

    [SerializeField]
    private GameObject _winText;

    public static bool youWin;



    void Start()
    {
        _winText.SetActive(false);
        youWin = false;
    }
    void Update()
    {
        if (_pictures[0].rotation.z == 0 &&
            _pictures[1].rotation.z == 0 &&
            _pictures[2].rotation.z == 0 &&
            _pictures[3].rotation.z == 0 &&
            _pictures[4].rotation.z == 0 &&
            _pictures[5].rotation.z == 0 &&
            _pictures[6].rotation.z == 0 &&
            _pictures[7].rotation.z == 0 
            )
        {
            youWin = true;
            _winText.SetActive(true);

        }
    }
}
