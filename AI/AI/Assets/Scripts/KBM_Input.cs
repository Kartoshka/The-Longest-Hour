using UnityEngine;
using System.Collections;


public class KBM_Input : MonoBehaviour {

    public bool keyA_Down;
    public bool keyA_Up;
    public bool keyA_Hold;

    public bool keyS_Down;
    public bool keyS_Up;
    public bool keyS_Hold;

    public bool keyD_Down;
    public bool keyD_Up;
    public bool keyD_Hold;

    public bool keyW_Down;
    public bool keyW_Up;
    public bool keyW_Hold;

    public bool keySpace_Down;
    public bool keySpace_Up;
    public bool keySpace_Hold;

    public float mouseAxis_X;
    public float mouseAxis_Y;

    public bool mouseLeft_Down;
    public bool mouseLeft_Up;
    public bool mouseLeft_Hold;

    public bool mouseMiddle_Down;
    public bool mouseMiddle_Up;
    public bool mouseMiddle_Hold;

    public bool mouseRight_Down;
    public bool mouseRight_Up;
    public bool mouseRight_Hold;


    // Use this for initialization
    void Awake () {

        keyA_Down = false;
        keyA_Up = false;
        keyA_Hold = false;

        keyW_Down = false;
        keyW_Up = false;
        keyW_Hold =false;

        keyS_Down = false;
        keyS_Up = false;
        keyS_Hold = false;

        keyD_Down = false;
        keyD_Up = false;
        keyD_Hold = false;

        mouseAxis_X = 0.0f;
        mouseAxis_Y = 0.0f;

        mouseLeft_Down = false;
        mouseLeft_Up = false;
        mouseLeft_Hold = false;

        mouseRight_Down = false;
        mouseRight_Up = false;
        mouseRight_Hold = false;

        mouseMiddle_Down = false;
        mouseMiddle_Up = false;
        mouseMiddle_Hold = false;
	
	}
	
	// Update is called once per frame
	void Update () {

        keyA_Down = Input.GetKeyDown(KeyCode.A);
        keyA_Up = Input.GetKeyUp(KeyCode.A);
        keyA_Hold = Input.GetKey(KeyCode.A);

        keyW_Down = Input.GetKeyDown(KeyCode.W);
        keyW_Up = Input.GetKeyUp(KeyCode.W);
        keyW_Hold = Input.GetKey(KeyCode.W);

        keyS_Down = Input.GetKeyDown(KeyCode.S);
        keyS_Up = Input.GetKeyUp(KeyCode.S);
        keyS_Hold = Input.GetKey(KeyCode.S);

        keyD_Down = Input.GetKeyDown(KeyCode.D);
        keyD_Up = Input.GetKeyUp(KeyCode.D);
        keyD_Hold = Input.GetKey(KeyCode.D);

        mouseAxis_X = Input.GetAxis("Mouse X");
        mouseAxis_Y = Input.GetAxis("Mouse Y");

        mouseLeft_Down = Input.GetMouseButtonDown(0);
        mouseLeft_Up = Input.GetMouseButtonUp(0);
        mouseLeft_Hold = Input.GetMouseButton(0);

        mouseRight_Down = Input.GetMouseButtonDown(1);
        mouseRight_Up = Input.GetMouseButtonUp(1);
        mouseRight_Hold = Input.GetMouseButton(1);

        mouseMiddle_Down = Input.GetMouseButtonDown(2);
        mouseMiddle_Up = Input.GetMouseButtonUp(2);
        mouseMiddle_Hold = Input.GetMouseButton(2);

    }
}
