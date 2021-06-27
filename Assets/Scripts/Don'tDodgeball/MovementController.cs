﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MovementController : MonoBehaviour
{ 
    [SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private float dashSpeed = 10f;

    [SerializeField]
    float startDashGauge = 2f;

    private float dashGauge = 2f;

    [SerializeField]
    Image dashBar;

    bool recharging;

    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    bool isStunned;

    [SerializeField]
    GameObject fpsCamera;

    [SerializeField]
    float stunDuration = 3.2f;

    [SerializeField] GameObject stunFx;
    [SerializeField] GameObject stunText;
    [SerializeField] GameObject hitFx;

    public bool isGrounded;
    Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
    float jumpForce = 3.0f;
    float jumpCD = 2f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float CameraUpAndDownRotation = 0f;
    private float CurrentCameraUpAndDownRotation = 0f;

    private Rigidbody rb;
    public Animator anim;
    public PhotonView pv;

    public bool disableInputs;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        stunFx.SetActive(false);
        stunText.SetActive(false);
        hitFx.SetActive(false);

        anim = GetComponentInChildren<Animator>();
        if (anim.layerCount == 2)
            anim.SetLayerWeight(1, 1);

        dashSpeed = 5 * speed;
        dashGauge = startDashGauge;
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine) //If it's the other player
        {
            Destroy(rb);
        }

        else //Our player
        {
            dashBar = GameObject.Find("Dash Bar").GetComponent<Image>();
        }
    }

    
    // Update is called once per frame
    private void Update()
    {
        if (!pv.IsMine || disableInputs) return;
        //Calculate movement velocity as a 3d vector
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;
        float moveSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) && dashGauge > 0 && !recharging)
        {
            moveSpeed = dashSpeed;
            dashGauge -= Time.deltaTime;
            UpdateUI(dashGauge / startDashGauge);
            if(dashGauge <= 0)
            {
                recharging = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        if (recharging)
        {
            dashGauge += Time.deltaTime;
            UpdateUI(dashGauge / startDashGauge);
            if (dashGauge >= startDashGauge)
            {
                dashGauge = startDashGauge;
                recharging = false;
            }
        }

        //Final movement velocty vector
        Vector3 _movementVelocity = (_movementHorizontal + _movementVertical).normalized * moveSpeed;
        anim.SetFloat("Move Speed", _zMovement * moveSpeed);
        anim.SetFloat("Horizontal", _xMovement * moveSpeed);
        //Apply movement
        Move(_movementVelocity);

        //calculate rotation as a 3D vector for turning around.
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0,_yRotation,0)*lookSensitivity;

        //Apply rotation
        Rotate(_rotationVector);

        //Calculate look up and down camera rotation
        float _cameraUpDownRotation = Input.GetAxis("Mouse Y")*lookSensitivity;

        //Apply rotation
        RotateCamera(_cameraUpDownRotation);
    }

    //runs per physics iteration
    private void FixedUpdate()
    {
        if (!pv.IsMine) return;
        if (velocity!=Vector3.zero)
        {
            rb.MovePosition(rb.position+velocity*Time.fixedDeltaTime);
        }
        rb.MoveRotation(rb.rotation*Quaternion.Euler(rotation));
        if (fpsCamera!=null)
        {

            CurrentCameraUpAndDownRotation -= CameraUpAndDownRotation;
            CurrentCameraUpAndDownRotation = Mathf.Clamp(CurrentCameraUpAndDownRotation,-85,85);

            fpsCamera.transform.localEulerAngles = new Vector3(CurrentCameraUpAndDownRotation,0,0);
        }
    }

    public IEnumerator StunCo()
    {
        speed = 0f;
        anim.SetBool("Stun", true);
        stunFx.SetActive(true);
        stunText.SetActive(true);
        yield return new WaitForSeconds(stunDuration);
        stunFx.SetActive(false);
        stunText.SetActive(false);
        hitFx.SetActive(false);
        speed = 5f;
        anim.SetBool("Stun", false);
        isStunned = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) return;
        if (other.TryGetComponent(out Dodgeball d))
        {
            if (d.isFlying == true && isStunned == false && !Input.GetKeyDown(KeyCode.E))
            {
                KenaStun();
                pv.RPC("PlayFx", RpcTarget.AllBuffered);
                isStunned = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!pv.IsMine) return;
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!pv.IsMine) return;
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void Move(Vector3 movementVelocity)
    {
        velocity = movementVelocity;
    }

    void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;

    }

    void RotateCamera(float cameraUpAndDownRotation)
    {
        CameraUpAndDownRotation = cameraUpAndDownRotation;
    }

    public void UpdateUI(float fill)
    {
        dashBar.fillAmount = fill;
    }

    public IEnumerator FxCo()
    {
        AudioManager.instance.Play("Boing");
        hitFx.SetActive(true);
        stunFx.SetActive(true);
        stunText.SetActive(true);
        yield return new WaitForSeconds(stunDuration);
        stunFx.SetActive(false);
        stunText.SetActive(false);
        hitFx.SetActive(false);
    }


    [PunRPC]
    public void PlayFx()
    {
        StartCoroutine("FxCo");
    }
    [PunRPC]
    public void KenaStun()
    {
        StartCoroutine("StunCo");
    }
}
