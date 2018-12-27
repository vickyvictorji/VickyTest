using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public static Spawner instance;

    [SerializeField]
    Transform leftPos; 

    [SerializeField]
    Transform rightPos; 

    [SerializeField]
    Transform upPos; 

    [SerializeField]
    Transform downPos;

    [SerializeField]
     GameObject Cube;


    void Awake()
    {
        instance = this;
    }

    void Start (){
        SpawnCubes();
	}

    public void SpawnCubes()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = SpawnCubePos(Cube);
            Color cubeClr = CubeColor(Cube);
            GameObject cube = Instantiate(Cube,pos,Quaternion.identity);
            cube.GetComponent<MeshRenderer>().material.color = cubeClr;
            cube.transform.name = cube.tag + "Cube";
        }
    }

    public Vector3 SpawnCubePos(GameObject go)
    {
        float minX = leftPos.position.x - go.transform.localScale.x;
        float maxX = rightPos.position.x + go.transform.localScale.x;

        float minZ = upPos.position.z - go.transform.localScale.z;
        float maxZ = downPos.position.z + go.transform.localScale.z;

        Vector3 newVec = new Vector3(Random.Range (minX, maxX),
            go.transform.position.y,
            Random.Range (minZ, maxZ));
        return newVec;
    }

    public Color CubeColor(GameObject go)
    {
        Color cubeColor;
        if (Random.Range(0, 2) == 0)
        {
            cubeColor = Color.red;
            go.tag = "Red";
        }
        else
        {
            cubeColor = Color.blue;
            go.tag = "Blue";
        }
        return cubeColor;
    }

}
