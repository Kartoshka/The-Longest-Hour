using UnityEngine;
using System.Collections;


public class KBM_Input : MonoBehaviour {

    public bool keyA_Down
    {
        get
        {
            return keyA_Down;
        }
        private set
        {
            keyA_Down = value;
        }
    }

    public bool keyA_Up
    {
        get
        {
            return keyA_Up;
        }
        private set
        {
            keyA_Up = value;
        }
    }

    public bool keyA_Hold
    {
        get
        {
            return keyA_Hold;
        }
        private set
        {
            keyA_Hold = value;
        }
    }

    public bool keyS_Down
    {
        get
        {
            return keyS_Down;
        }
        private set
        {
            keyS_Down = value;
        }
    }

    public bool keyS_Up
    {
        get
        {
            return keyS_Up;
        }
        private set
        {
            keyS_Up = value;
        }
    }

    public bool keyS_Hold
    {
        get
        {
            return keyS_Hold;
        }
        private set
        {
            keyS_Hold = value;
        }
    }

    public bool keyD_Down
    {
        get
        {
            return keyD_Down;
        }
        private set
        {
            keyD_Down = value;
        }
    }

    public bool keyD_Up
    {
        get
        {
            return keyD_Up;
        }
        private set
        {
            keyD_Up = value;
        }
    }

    public bool keyD_Hold
    {
        get
        {
            return keyD_Hold;
        }
        private set
        {
            keyD_Hold = value;
        }
    }

    public bool keyW_Down
    {
        get
        {
            return keyW_Down;
        }
        private set
        {
            keyW_Down = value;
        }
    }

    public bool keyW_Up
    {
        get
        {
            return keyW_Up;
        }
        private set
        {
            keyW_Up = value;
        }
    }

    public bool keyW_Hold
    {
        get
        {
            return keyW_Hold;
        }
        private set
        {
            keyW_Hold = value;
        }
    }

    public bool keySpace_Down
    {
        get
        {
            return keySpace_Down;
        }
        private set
        {
            keySpace_Down = value;
        }
    }

    public bool keySpace_Up
    {
        get
        {
            return keySpace_Up;
        }
        private set
        {
            keySpace_Up = value;
        }
    }

    public bool keySpace_Hold
    {
        get
        {
            return keySpace_Hold;
        }
        private set
        {
            keySpace_Hold = value;
        }
    }

    public float mouseAxis_X
    {
        get
        {
            return mouseAxis_X;
        }
        private set
        {
            mouseAxis_X = value;
        }
    }

    public float mouseAxis_Y
    {
        get
        {
            return mouseAxis_Y;
        }
        private set
        {
            mouseAxis_Y = value;
        }
    }

    public bool mouseLeft_Down
    {
        get
        {
            return mouseLeft_Down;
        }
        private set
        {
            mouseLeft_Down = value;
        }
    }

    public bool mouseLeft_Up
    {
        get
        {
            return mouseLeft_Up;
        }
        private set
        {
            mouseLeft_Up = value;
        }
    }

    public bool mouseLeft_Hold
    {
        get
        {
            return mouseLeft_Hold;
        }
        private set
        {
            mouseLeft_Hold = value;
        }
    }

    public bool mouseMiddle_Down
    {
        get
        {
            return mouseMiddle_Down;
        }
        private set
        {
            mouseMiddle_Down = value;
        }
    }

    public bool mouseMiddle_Up
    {
        get
        {
            return mouseMiddle_Up;
        }
        private set
        {
            mouseMiddle_Up = value;
        }
    }

    public bool mouseMiddle_Hold
    {
        get
        {
            return mouseMiddle_Hold;
        }
        private set
        {
            mouseMiddle_Hold = value;
        }
    }

    public bool mouseRight_Down
    {
        get
        {
            return mouseRight_Down;
        }
        private set
        {
            mouseRight_Down = value;
        }
    }

    public bool mouseRight_Up
    {
        get
        {
            return mouseRight_Up;
        }
        private set
        {
            mouseRight_Up = value;
        }
    }

    public bool mouseRight_Hold
    {
        get
        {
            return mouseRight_Hold;
        }
        private set
        {
            mouseRight_Hold = value;
        }
    }


    // Use this for initialization
    void Awake () {

        keyA_Down = false;
        keyA_Up = false;
        keyA_Up = false;

        keyW_Down = false;
        keyW_Up = false;
        keyW_Down = false;

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
