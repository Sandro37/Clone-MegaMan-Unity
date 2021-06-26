using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform[] limiteCam;
    [SerializeField] private float speedCam;
    [SerializeField] private Text life;

    private Camera cam;
    public Transform PlayerTransform
    {
        get => playerTransform;
        set => playerTransform = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        cameraMovement();
    }

   public void setLifeText(float life)
    {
        this.life.text = "LIFE: " + life.ToString();
    }
    void cameraMovement()
    {
        float posCamX = playerTransform.position.x;
        float posCamY = playerTransform.position.y;

        if(cam.transform.position.x < limiteCam[0].position.x && playerTransform.position.x < limiteCam[0].position.x)
        {
            posCamX = limiteCam[0].position.x;
        }else if(cam.transform.position.x > limiteCam[1].position.x && playerTransform.position.x > limiteCam[1].position.x)
        {
            posCamX = limiteCam[1].position.x;
        }

        if(cam.transform.position.y < limiteCam[3].position.y && playerTransform.position.y < limiteCam[3].position.y)
        {
            posCamY = limiteCam[3].position.y;
        }else if(cam.transform.position.y > limiteCam[2].position.y && playerTransform.position.y > limiteCam[2].position.y)
        {
            posCamY = limiteCam[2].position.y;
        }

        Vector3 posCam = new Vector3(posCamX, posCamY, cam.transform.position.z);

        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
