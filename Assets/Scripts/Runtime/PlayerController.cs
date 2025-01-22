using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveRight()
    {
        if (this.gameObject.transform.position.x == 1) return;
        this.gameObject.transform.position += new Vector3(1, 0, 0);
    }
    public void MoveLeft()
    {
        if (this.gameObject.transform.position.x == -1) return;
        this.gameObject.transform.position += new Vector3(-1, 0, 0);
    }
}
