using UnityEngine;
using System.Collections;

public class SpawnAtClick : MonoBehaviour {

    public GameObject go;
    Ray ray;
    RaycastHit hit;
    // Update is called once per frame
    void Update () {

        

        if (Input.GetButtonDown("Fire1"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
               GameObject obj = Instantiate(go, new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), Quaternion.identity) as GameObject;
            }
        }

    }
}
