using System.Collections;
using UnityEngine;

public class BirdFlocking : MonoBehaviour
{
    [Header("Main Settings")]
    public Vector2 behavioralCh = new Vector2(3.0f, 7.0f);
    public bool debug;

    [Header("Bird Settings")]
    public GameObject birdPref;
    [Range(1, 1000)] public int birdNumber = 120;
    [Range(0, 150)] public float birdSpeed = 1.7f;
    [Range(0, 100)] public int fragmentedBirds = 23;
    [Range(0, 1)] public float fragmentedBirdsYLimit = 0.9f;
    [Range(0, 10)] public float soaring = 0.74f;
    [Range(0.01f, 500)] public float verticalWawe = 15;
    public bool rotationClamp = false;
    [Range(0, 360)] public float rotationClampValue = 50;
    public Vector2 scaleRandom = new Vector2(1.0f, 1.5f);

    [Header("Flock Settings")]
    [Range(1, 150)] public int flockNumber = 4;
    [Range(0, 5000)] public int fragmentedFlock = 800;
    [Range(0, 1)] public float fragmentedFlockYLimit = 0.2f;
    [Range(0, 1.0f)] public float migrationFrequency = 0.015f;
    [Range(0, 1.0f)] public float posChangeFrequency = 0.2f;
    [Range(0, 100)] public float smoothChFrequency = 20;

    Transform thisTransform;
    Transform[] birdsTransform, flocksTransform;
    Vector3[] rdTargetPos, flockPos, velFlocks;
    float[] birdsSpeed, birdsSpeedCur, spVelocity;
    int[] currentFlock;
    float timeTime;
    static WaitForSeconds delay0;

    void Awake()
    {
        thisTransform = transform;
        FlockCreation();
        BirdCreation();
        StartCoroutine(BehavioralChange());
    }

    void LateUpdate()
    {
        FlocksMovement();
        BirdsMovement();
    }

    void FlocksMovement()
    {  
        for (int i = 0; i < flockNumber; i++)
        {
            flocksTransform[i].localPosition = Vector3.SmoothDamp(flocksTransform[i].localPosition, flockPos[i], ref velFlocks[i], smoothChFrequency);
        }
    }

    void BirdsMovement()
    {
        float deltaTime = Time.deltaTime;
        timeTime += deltaTime;
        Vector3 translateCur = Vector3.forward * birdSpeed * deltaTime;
        Vector3 verticalWaweCur = Vector3.up * ((verticalWawe * 0.5f) - Mathf.PingPong(timeTime * 0.5f, verticalWawe));
        float soaringCur = soaring * deltaTime;

        for (int i = 0; i < birdNumber; i++)
        {
            if (birdsSpeedCur[i] != birdsSpeed[i]) birdsSpeedCur[i] = Mathf.SmoothDamp(birdsSpeedCur[i], birdsSpeed[i], ref spVelocity[i], 0.5f);
            birdsTransform[i].Translate(translateCur * birdsSpeed[i]);
            Vector3 tpCh = flocksTransform[currentFlock[i]].position + rdTargetPos[i] + verticalWaweCur - birdsTransform[i].position;
            Quaternion rotationCur = Quaternion.LookRotation(Vector3.RotateTowards(birdsTransform[i].forward, tpCh, soaringCur, 0));
            if (rotationClamp == false) birdsTransform[i].rotation = rotationCur;
            else birdsTransform[i].localRotation = BirdsRotationClamp(rotationCur, rotationClampValue);
        }

    }

    IEnumerator BehavioralChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(behavioralCh.x, behavioralCh.y));

            for (int i = 0; i < flockNumber; i++)
            {
                if (Random.value < posChangeFrequency)
                {
                    Vector3 rdvf = Random.insideUnitSphere * fragmentedFlock;
                    flockPos[i] = new Vector3(rdvf.x, Mathf.Abs(rdvf.y * fragmentedFlockYLimit), rdvf.z);
                }
            }

            for (int i = 0; i < birdNumber; i++)
            {
                birdsSpeed[i] = Random.Range(3.0f, 7.0f);
                Vector3 lpv = Random.insideUnitSphere * fragmentedBirds;
                rdTargetPos[i] = new Vector3(lpv.x, lpv.y * fragmentedBirdsYLimit, lpv.z);
                if (Random.value < migrationFrequency) currentFlock[i] = Random.Range(0, flockNumber);
            } 
        }
    }

    void FlockCreation()
    {
        flocksTransform = new Transform[flockNumber];
        flockPos = new Vector3[flockNumber];
        velFlocks = new Vector3[flockNumber];
        currentFlock = new int[birdNumber];

        for (int i = 0; i < flockNumber; i++)
        {
            GameObject nobj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            nobj.SetActive(debug);
            flocksTransform[i] = nobj.transform;
            Vector3 rdvf = Random.onUnitSphere * fragmentedFlock;
            flocksTransform[i].position = thisTransform.position;
            flockPos[i] = new Vector3(rdvf.x, Mathf.Abs(rdvf.y * fragmentedFlockYLimit), rdvf.z);
            flocksTransform[i].parent = thisTransform;
        }
    }

    void BirdCreation()
    {
        birdsTransform = new Transform[birdNumber];
        birdsSpeed = new float[birdNumber];
        birdsSpeedCur = new float[birdNumber];
        rdTargetPos = new Vector3[birdNumber];
        spVelocity = new float[birdNumber];

        for (int i = 0; i < birdNumber; i++)
        {
            birdsTransform[i] = Instantiate(birdPref, thisTransform).transform;
            Vector3 lpv = Random.insideUnitSphere * fragmentedBirds;
            birdsTransform[i].localPosition = rdTargetPos[i] = new Vector3(lpv.x, lpv.y * fragmentedBirdsYLimit, lpv.z);
            birdsTransform[i].localScale = Vector3.one * Random.Range(scaleRandom.x, scaleRandom.y);
            birdsTransform[i].localRotation = Quaternion.Euler(0, Random.value * 360, 0);
            currentFlock[i] = Random.Range(0, flockNumber);
            birdsSpeed[i] = Random.Range(3.0f, 7.0f);
        }
    }

    static Quaternion BirdsRotationClamp(Quaternion rotationCur, float rotationClampValue)
    {
        Vector3 angleClamp = rotationCur.eulerAngles;
        rotationCur.eulerAngles = new Vector3(Mathf.Clamp((angleClamp.x > 180) ? angleClamp.x - 360 : angleClamp.x, -rotationClampValue, rotationClampValue), angleClamp.y, 0);
        return rotationCur;
    }
}