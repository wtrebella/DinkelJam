using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.CSG;

public class Destructor : MonoBehaviour 
{
   [SerializeField] private GameObject man;
   [SerializeField] private GameObject bullet;
   
   private GameObject manShotObj;
   private GameObject manChunkObj;
   private Mesh manShotMesh;
   private Mesh manChunkMesh;
   
   void Start()
   {
      manShotMesh = CSG.Subtract(man, bullet);
      manChunkMesh = CSG.Intersect(man, bullet);
      
      manShotObj = new GameObject();
      manShotObj.AddComponent<MeshFilter>().sharedMesh = manShotMesh;
      manShotObj.AddComponent<MeshRenderer>().sharedMaterial = man.GetComponent<MeshRenderer>().sharedMaterial;
      
      manChunkObj = new GameObject();
      manChunkObj.AddComponent<MeshFilter>().sharedMesh = manChunkMesh;
      manChunkObj.AddComponent<MeshRenderer>().sharedMaterial = man.GetComponent<MeshRenderer>().sharedMaterial;
      
      Destroy(man);
      Destroy(bullet);
   }
   
   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         if (manShotObj != null)
         {
            for (int i = 0; i < manShotMesh.vertexCount; i++)
            {
               Vector3 normal = manShotMesh.normals[i];
               Vector3 vert = manShotMesh.vertices[i];
   
               Debug.DrawLine(vert, vert + normal * 0.05f, Color.green, 100.0f);
            }
         }
      }
   }
}
