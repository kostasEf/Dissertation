using UnityEngine;
using System.Collections;

public class TestingMesh : MonoBehaviour {

   
    void Start()
    {
        Vector3[] verts = {
                              new Vector3(0, 0, 0),
                              new Vector3(1, 0, 0),
                              new Vector3(1, 1, 0),
                              new Vector3(0, 1, 0)
                          };

        int[] tris = {
                         2, 1, 0,
                         0, 3, 2
                     };

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        mesh.vertices = verts;
        //mesh.uv = newUV;
        mesh.triangles = tris;

        
    }

    


}
