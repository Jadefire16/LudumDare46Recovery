using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class FOV : MonoBehaviour
{
    Mesh mesh;
    public Vector3[] vertices;
    Vector2[] uv;
    int[] tris;

    private void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetOrigin(Vector3 origin) {
        transform.position = origin;
    }

    private void SetTris(int x) {
        tris = new int[(x - 1) * 3];

        int c = 1;
        tris[0] = 0;
        for (int i = 1; i < tris.Length-2; i++) {
            for (int j = 0; j < 2; j++) {
                tris[i] = c;
                c++;
                i++;
            }
            c--;
            if (i < tris.Length) {
                tris[i] = 0;
            }
        }
    }

    public void SetVertices(Vector3[] vertices) {
        this.vertices = vertices;
        uv = new Vector2[vertices.Length];
        SetTris(vertices.Length);
        UpdateMesh();
    }

    public void UpdateMesh() {
        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.uv = uv;
    }


}
