using UnityEngine;
using System.Collections;

public class ModifyVertices : MonoBehaviour {

    Mesh mesh;
    Vector3[] vertices;
    Vector3[] normals;

	
	// Update is called once per frame
	void Start () {

        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        normals = mesh.normals;
        int i = 0;
        while (i < vertices.Length)
        {
            vertices[i] += normals[i] * Mathf.Sin(Time.deltaTime) * Random.Range(2.0f, 15.0f);
            i++;
        }
        UpdateMeshAndCollider(vertices, normals, false);

    }	

    public void UpdateMeshAndCollider(Vector3[] newVertices, Vector3[] newNormals, bool draw)
    {
        if(draw)
        {

            int i = 0;
            while (i < newVertices.Length)
            {
                Debug.DrawLine(transform.TransformPoint(vertices[i]), transform.TransformPoint(newVertices[i]), Color.red, 30.0f);
                i++;
               
            }
        }
        GetComponent<MeshFilter>().mesh.vertices = newVertices;
        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
    }

    public Mesh getMesh()
    {
        return GetComponent<MeshFilter>().mesh;
    }
}
