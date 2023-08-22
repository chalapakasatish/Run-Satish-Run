using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject lastPlatform;
    public GameObject firstPlatformObject;
    public GameObject lastPlatformOject;
    public float platformDistance;
    public Texture[] platformtextures;
    public int howManyPlatforms;
    public GameObject[] obstaclesTag;
    public List<GameObject> allPlatforms = new List<GameObject>();
    void Start()
    {
        PlatformGenerator();
    }
    public void PlatformGenerator()
    {
        int platfomrTextureRandomNum = Random.Range(0, platformtextures.Length);
        firstPlatformObject =  Instantiate(platforms[0], transform.position, transform.rotation);
        firstPlatformObject.GetComponent<Renderer>().material.mainTexture = platformtextures[platfomrTextureRandomNum];
        firstPlatformObject.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = platformtextures[platfomrTextureRandomNum];
        firstPlatformObject.transform.GetChild(1).GetComponent<Renderer>().material.mainTexture = platformtextures[platfomrTextureRandomNum];
        
        platformDistance = 15;
        for (int i = 0; i < howManyPlatforms; i++)
        {
            GameObject nObject =  Instantiate(platforms[Random.Range(1, platforms.Length)], new Vector3(0, 0, transform.position.z + platformDistance), transform.rotation);
            nObject.GetComponent<Renderer>().material.mainTexture = platformtextures[platfomrTextureRandomNum];
            nObject.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = platformtextures[platfomrTextureRandomNum];
            nObject.transform.GetChild(1).GetComponent<Renderer>().material.mainTexture = platformtextures[platfomrTextureRandomNum];
            platformDistance += 15f;
            allPlatforms.Add(nObject);
        }
       
        lastPlatformOject = Instantiate(lastPlatform, new Vector3(0, 0, transform.position.z + platformDistance), transform.rotation);
        lastPlatformOject.GetComponent<Renderer>().material.mainTexture = platformtextures[platfomrTextureRandomNum];
        lastPlatformOject.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = platformtextures[platfomrTextureRandomNum];
        lastPlatformOject.transform.GetChild(1).GetComponent<Renderer>().material.mainTexture = platformtextures[platfomrTextureRandomNum];
        
        obstaclesTag = GameObject.FindGameObjectsWithTag("Obstacles");  
    }
    public void DeletePlatforms()
    {
        foreach (GameObject item in allPlatforms)
        {
            Destroy(item);
        }
        Destroy(lastPlatformOject);
        Destroy(firstPlatformObject);
    }
}
