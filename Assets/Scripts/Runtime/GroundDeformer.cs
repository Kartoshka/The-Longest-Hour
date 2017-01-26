using UnityEngine;
using System.Collections;

public class GroundDeformer : MonoBehaviour {

    public float threshold;
    
    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("deformable") && (collision.gameObject.GetComponent<ModifyVertices>() != null))
        {
            
            ModifyVertices modifyVertices = collision.gameObject.GetComponent<ModifyVertices>();
            Mesh targetMesh = modifyVertices.getMesh();
            Vector3[] vertices = targetMesh.vertices;
            Vector3[] normals = targetMesh.normals;
            
            foreach (ContactPoint contact in collision.contacts)
            {
                int i = 0;
                while (i < vertices.Length)
                {
                    if (Vector3.Distance(transform.TransformPoint(vertices[i]), contact.point) <= threshold)
                    {
                        //vertices[i] = new Vector3(transform.TransformPoint(vertices[i]).x, transform.TransformPoint(vertices[i]).y - 2.0f, transform.TransformPoint(vertices[i]).z);
                        Vector3 newPoint = transform.TransformPoint(vertices[i]);
                        newPoint = new Vector3(newPoint.x, newPoint.y - 2.0f, newPoint.z);
                        Debug.DrawLine(transform.TransformPoint(vertices[i]), newPoint, Color.blue, 10.0f);
                        vertices[i] = transform.InverseTransformPoint(newPoint);
                    }
                    i++;
                }
                /*
                if(draw)
                {
                    Debug.DrawRay(contact.point, contact.normal, Color.red, 5.0f);
                    draw = false;
                }
                    //Debug.Log(Vector3.Distance(vertex, contact.point));)
                    */
                    
            }
            modifyVertices.UpdateMeshAndCollider(vertices, normals, true);
        }
    }
}
