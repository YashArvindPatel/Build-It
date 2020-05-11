using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ClickToSpawn : MonoBehaviour
{
    public GameObject cube;
    public float timer = 0f;
    public float setTimer = 1f;
    public Transform currentCube;
    List<Vector3> positions = new List<Vector3>();
    public bool save = false;
    Rigidbody rb;
    public bool gameOver = false;
    public Material material;
    public TextMeshProUGUI heightText;
    public TextMeshProUGUI scoreText;
    public int maxHeight = 1;
    public int ceilHeight = 10;
    public int level = 0;
    public AudioClip click;

    public int score = 0;
    public int maxScore = 10;

    public bool cameraFOV = false;
    public bool cameraMove = false;
    public float newCameraPos = 0;

    public float loadTimer = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        positions.Add(currentCube.localPosition);
    }

    public void UpdateScore()
    {
        score++;
        if (score >= maxScore)
        {
            maxScore += 10;
        }
        scoreText.text = score + " / " + maxScore;
    }

    void Update()
    {
        if (gameOver)
        {
            if (loadTimer < 0)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                loadTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (cameraFOV)
            {
                Camera.main.fieldOfView += 0.5f;

                if (Camera.main.fieldOfView % 15 == 0)
                {
                    cameraFOV = false;
                }
            }

            if (cameraMove && Camera.main.transform.position.y < newCameraPos)
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 0.25f, Camera.main.transform.position.z);

                if (Camera.main.transform.position.y >= newCameraPos){
                    cameraMove = false;
                }
            }


            if (rb.velocity.magnitude > 0.2)
            {
                gameOver = true;
                Destroy(currentCube.gameObject);
            }

            if (timer <= 0)
            {
                Vector3 tempPos = currentCube.localPosition;

                if (!save && positions.Count > 1)
                {
                    Destroy(currentCube.gameObject);
                    positions.Remove(positions[positions.Count - 1]);
                }
                else
                {
                    bool trigger = false;
                    currentCube.SetParent(transform);
                    currentCube.GetComponent<MeshRenderer>().material = material;
                    save = false;
                    if (maxHeight < Mathf.CeilToInt(currentCube.position.y))
                    {
                        maxHeight = Mathf.CeilToInt(currentCube.position.y);
                        trigger = true;
                    }

                    if (maxHeight % 10 == 0 && trigger)
                    {
                        ceilHeight += 10;
                        if (setTimer > 0.5f)
                        {
                            setTimer -= 0.25f;
                        }
                        cameraMove = true;
                        cameraFOV = true;
                        newCameraPos = Camera.main.transform.position.y + 5 + level * 2;
                        level += 1;
                    }

                    heightText.text = maxHeight + " / " + ceilHeight;
                }

                Vector3 pos = positions[positions.Count - 1];
                List<Vector3> possiblePos = new List<Vector3>();

                if (pos.y <= 0.5)
                {
                    possiblePos.Add(new Vector3(pos.x - 1, pos.y, pos.z));
                    possiblePos.Add(new Vector3(pos.x + 1, pos.y, pos.z));
                    possiblePos.Add(new Vector3(pos.x, pos.y + 1, pos.z));
                    possiblePos.Add(new Vector3(pos.x, pos.y, pos.z - 1));
                    possiblePos.Add(new Vector3(pos.x, pos.y, pos.z + 1));
                }
                else
                {
                    possiblePos.Add(new Vector3(pos.x - 1, pos.y, pos.z));
                    possiblePos.Add(new Vector3(pos.x + 1, pos.y, pos.z));
                    possiblePos.Add(new Vector3(pos.x, pos.y - 1, pos.z));
                    possiblePos.Add(new Vector3(pos.x, pos.y + 1, pos.z));
                    possiblePos.Add(new Vector3(pos.x, pos.y, pos.z - 1));
                    possiblePos.Add(new Vector3(pos.x, pos.y, pos.z + 1));
                }

                foreach (var item in possiblePos.ToArray())
                {
                    if (positions.Contains(item) || item == tempPos)
                    {
                        possiblePos.Remove(item);
                    }
                }

                GameObject newCube = Instantiate(cube) as GameObject;
                newCube.transform.localPosition = possiblePos[Random.Range(0, possiblePos.Count)];
                currentCube = newCube.transform;
                positions.Add(currentCube.localPosition);

                timer = setTimer;
            }
            else
            {
                timer -= Time.deltaTime;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                save = true;
                GetComponent<AudioSource>().PlayOneShot(click);
            }
        }    
    }
}
