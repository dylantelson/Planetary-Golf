using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour {

    // Use this for initialization

    public bool opened;

    public void Open()
    {
        GetComponent<Animator>().SetBool("opened", true);
    }

}
