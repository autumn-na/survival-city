using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    Rigidbody PlayerRd;

    public GameObject Body;
    public Camera Camera;
    public AudioSource GunSound;
    public GameObject Light;
    public GameObject BurstParticle;
    public GameObject PistolPos;
    public GameObject RifePos;
    public GameObject ShotgunPos;
    public GameObject Pistol;
    public GameObject Rife;
    public GameObject Shotgun;
    public GameObject Larm_p;
    public GameObject Larm;
    public GameObject Aim;
    public NMHGun Gun;

    public float Speed = 4.0f;
    public float mouseMoveSpeed = 2.0f;
    public float JumpPower = 3.5f;

    
    public bool possShot = true;
    public bool isShoting;
    public bool change = false;
    bool Moveing = false;
    bool Running = false;

    

    public ParticleSystem[] Muzzle;//0=작은 폭팔, 1= 큰 폭팔, 2= 몬스터 사격, 3 = 지형 사격
    public AudioClip[] SEffect;//0=권총 소리,1 = 라이플 소리, 2 = 샷건 소리,

    //public int JumpCount = 0;
    public int KillCount = 0;
    int RunSpeed = 0;//0=걷기,1=뛰기



    // Use this for initialization
    void Start() {
        Gun = new NMHGun();
        Gun.nMaxBullet = 9999;
        Gun.nMaxMagazine =12;
        Gun.nCurBullet = 9999;
        Gun.nCurMagazine = 12;
        Gun.nMinFoundBullet = 9999;
        Gun.nMaxFoundBullet = 9999;
        Gun.fShootDelay =0.5f;
        Gun.fReloadDelay = 2.0f;
        Gun.nDamage = 50;
        Gun.bIsChangeble = false;
        Gun.tGunType = NMHGun.GunType.PISTOL; ;
        PlayerRd =  GetComponent<Rigidbody>();
        GunSound = GetComponent<AudioSource>();
        
        
        

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //DHand.rotation = new Quaternion(Hand.rotation.x, Hand.rotation.y-15, Hand.rotation.z, Hand.rotation.w);
    }

    private void FixedUpdate()
    {
        Playermove();
    }
    // Update is called once per frame
    void Update() {

        AimSet();
        MouseMove();
        GunShot();
        Shot();
        Moveset();
        SpeedStat();
        GunSet();
        GunBurst();
        GunSoundset();

        if(Input.GetKeyDown(KeyCode.F))
        {
            GetGun();

        }
        


    }
    void AimSet()
    {
        if (Gun.tGunType == NMHGun.GunType.PISTOL|| Gun.tGunType == NMHGun.GunType.RIPLE)
        {
            Aim.transform.localScale = new Vector3(3.5f, 2.6f, 1f);
        }
        else
        {
            Aim.transform.localScale = new Vector3(7.5f, 6.0f, 1f);
        }
    }
    void GunSoundset()
    {
        if (Gun.tGunType == NMHGun.GunType.PISTOL)
        {
            GunSound.clip = SEffect[0];
        }
        else if(Gun.tGunType == NMHGun.GunType.RIPLE)
        {
            GunSound.clip = SEffect[1];
        }
        else
        {
            GunSound.clip = SEffect[2];
        }
    }
    void GunSet()
    {
        if (Gun.tGunType == NMHGun.GunType.PISTOL)
        {
            Pistol.SetActive(true);
            Rife.SetActive(false);
            Shotgun.SetActive(false);
            Larm.SetActive(false);
            Larm_p.SetActive(true);
        }
        else if (Gun.tGunType == NMHGun.GunType.RIPLE)
        {
            Pistol.SetActive(false);
            Rife.SetActive(true);
            Shotgun.SetActive(false);
            Larm.SetActive(true);
            Larm_p.SetActive(false);
        }
        else
        {
            Pistol.SetActive(false);
            Rife.SetActive(false);
            Shotgun.SetActive(true);
            Larm.SetActive(true);
            Larm_p.SetActive(false);
        }
    }
    void GunBurst()
    {
        if (Gun.tGunType == NMHGun.GunType.PISTOL)
        {
            BurstParticle = Muzzle[0].gameObject;
            BurstParticle.transform.position = PistolPos.transform.position;
        }
        else if (Gun.tGunType == NMHGun.GunType.RIPLE)
        {
            BurstParticle = Muzzle[0].gameObject;
            BurstParticle.transform.position = RifePos.transform.position;
        }
        else
        {
            BurstParticle = Muzzle[1].gameObject;
            BurstParticle.transform.position = Shotgun.transform.position;
        }
    }
    void Playermove()
    {
        Vector3 PlayerMoveSp = Vector3.zero;
        PlayerMoveSp.y = PlayerRd.velocity.y;

        /*if (JumpCount == 0)
        {
            float PlayerSz = PlayerMoveSp.y;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerMoveSp.y = JumpPower;
            }
        }*/
        if (Input.GetKey(KeyCode.A))
        {
            PlayerMoveSp.x = -Speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            PlayerMoveSp.x = Speed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            PlayerMoveSp.z = Speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            PlayerMoveSp.z = -Speed;
        }
        
        
        if(Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine("Reload");
            }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            RunSpeed = 1;
        }
        /*
        if (Input.GetKeyUp(KeyCode.W))
        {
            isWalking = false;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            isWalking = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isWalking = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            isWalking = false;
        }*/



        PlayerMoveSp = transform.rotation * PlayerMoveSp;


        PlayerRd.velocity = PlayerMoveSp;

        if (Moveing)
        {
            Body.GetComponent<Animator>().SetBool("Walking", true);
        }

        if (!Moveing)
        {
            Body.GetComponent<Animator>().SetBool("Walking", false);
        }
        if(Running)
        {
            Body.GetComponent<Animator>().SetBool("Running", true);
        }
        if (!Running)
        {
            Body.GetComponent<Animator>().SetBool("Running", false);
        }

    }
    void Moveset()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D))
        {
            
            if(Input.GetKey(KeyCode.LeftShift)&&!isShoting)
            {
                Running = true;
            }
            else
                Moveing = true;
        }
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            
            
            Moveing = false;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Running = false;
            RunSpeed = 0;
        }
    }
    void SpeedStat()
    {
        if (!isShoting)
        {
            if (RunSpeed == 1)
            {
                Speed = 6.0f;
            }
            else
                Speed = 4.0f;
        }
    }
    void MouseMove()
    {
        transform.Rotate(0,Input.GetAxis("Mouse X") * mouseMoveSpeed,0);

        Camera.transform.Rotate(Input.GetAxis("Mouse Y") * - mouseMoveSpeed,0, 0);

        Vector3 mouseYAngle = Camera.transform.eulerAngles;

        float limiteAngle = mouseYAngle.x > 180 ? mouseYAngle.x - 360 : mouseYAngle.x;

        limiteAngle = Mathf.Clamp(limiteAngle, -60.0f, 60.0f);

        Camera.transform.localRotation = Quaternion.Euler(new Vector3(limiteAngle,0,0));
    }
    void GunShot()
    {
        if (Gun.nCurMagazine > 0)
        {
            if (Gun.bIsChangeble)
            {
                if (Input.GetMouseButton(0))

                {
                    if (!Running)
                        Guntrue();

                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (!Running)
                        Guntrue();
                }
            }
        }
    }
    void Guntrue()
    {
        if (possShot)
        {
            
            isShoting = true;
            StartCoroutine(GunStop());
            RaycastHit ShotHIt;
                if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out ShotHIt))
                {

                    if (ShotHIt.collider.gameObject.name == "zombie_big(Clone)")
                    {

                        int NowHp;
                        Muzzle[2].transform.position = ShotHIt.point;
                        Muzzle[2].Play();
                        NowHp = ShotHIt.collider.gameObject.GetComponent<NMHZombie>().GetHP();
                        NowHp -= Gun.nDamage;
                        ShotHIt.collider.gameObject.GetComponent<NMHZombie>().SetHP(NowHp);
                        if (NowHp <= 0)
                        {
                            Destroy(ShotHIt.collider.gameObject);
                            KillCount++;
                        }

                    }
                    Muzzle[3].transform.position = ShotHIt.point;
                    Muzzle[3].Play();
                    Muzzle[4].transform.position = ShotHIt.point;
                    Muzzle[4].Play();
                    Gun.Shoot();
                    if (ShotHIt.collider != null)
                    {
                        Debug.DrawRay(Camera.transform.position, Camera.transform.forward * 100, Color.red, 1);

                    }
                
            
                

            }
            if (Gun.tGunType == NMHGun.GunType.PISTOL|| Gun.tGunType == NMHGun.GunType.RIPLE)
            {
                Muzzle[0].Play();
            }
            else if (Gun.tGunType == NMHGun.GunType.SHOTGUN)
            {
                Muzzle[1].Play();
            }
            Light.SetActive(true);
            Invoke("Lightoff",0.1f);
            GunSound.PlayOneShot(GunSound.clip);
        }

    }
    void GetGun()
    {
        RaycastHit LookHIt;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out LookHIt,5 ))
        {
            Debug.Log(LookHIt.transform.name);
                Debug.Log(LookHIt.transform.tag);
            if (LookHIt.transform.tag == "Gun")
            {
                Gun.nMaxBullet = LookHIt.transform.GetComponent<NMHDroppedGun>().Property.nMaxBullet;
                Gun.nMaxMagazine = LookHIt.transform.GetComponent<NMHDroppedGun>().Property.nMaxMagazine;
                Gun.nCurBullet = LookHIt.transform.GetComponent<NMHDroppedGun>().Property.nCurBullet;
                Gun.nCurMagazine = LookHIt.transform.GetComponent<NMHDroppedGun>().Property.nCurMagazine;
                Gun.nMinFoundBullet = LookHIt.transform.GetComponent<NMHDroppedGun>().Property.nMinFoundBullet;
                Gun.nMaxFoundBullet = LookHIt.transform.GetComponent<NMHDroppedGun>().Property.nMaxFoundBullet;
                Gun.fShootDelay = LookHIt.transform.GetComponent<NMHDroppedGun>().Property.fShootDelay;
                Gun.fReloadDelay = LookHIt.transform.GetComponent<NMHDroppedGun>().Property.fReloadDelay;
                Gun.nDamage = LookHIt.transform.GetComponent<NMHDroppedGun>().Property.nDamage;
                Gun.bIsChangeble = LookHIt.transform.GetComponent<NMHDroppedGun>().Property.bIsChangeble;
                Gun.tGunType = LookHIt.transform.GetComponent<NMHDroppedGun>().Property.tGunType;
                Destroy(LookHIt.collider.gameObject);
            }
            

        }
    }
    void Lightoff()
    {
        Light.SetActive(false);
    }
    void Shot()
    {
        if(isShoting == true)
        {
            if (Gun.bIsChangeble)
            {

                if (Input.GetMouseButton(0))
                {
                    if (Gun.tGunType == NMHGun.GunType.PISTOL)
                    {
                        Body.GetComponent<Animator>().SetBool("PistolShooting", true);
                    }
                    else if (Gun.tGunType == NMHGun.GunType.RIPLE)
                    {
                        Body.GetComponent<Animator>().SetBool("RifeShooting", true);
                    }
                    else
                    {
                        Body.GetComponent<Animator>().SetBool("ShotgunShooting", true); ;
                    }
                    
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (Gun.tGunType == NMHGun.GunType.PISTOL)
                    {
                        Body.GetComponent<Animator>().SetBool("PistolShooting", false);
                    }
                    else if (Gun.tGunType == NMHGun.GunType.RIPLE)
                    {
                        Body.GetComponent<Animator>().SetBool("RifeShooting", false);
                    }
                    else
                    {
                        Body.GetComponent<Animator>().SetBool("ShotgunShooting", false); ;
                    }
                    isShoting = false;
                }
            }
            else
                StartCoroutine("ShotWait");
        }
    }
    public void PlayerHit(Vector3 Direction)
    {
        PlayerRd.AddForce(Direction * 6.5f, ForceMode.Impulse);
    }
    IEnumerator Reload()
    {
        if(Gun.nCurMagazine < Gun.nMaxMagazine)
        {
            if (Gun.tGunType == NMHGun.GunType.PISTOL)
            {
                Body.GetComponent<Animator>().SetBool("PistolReloading", true);
            }
            else if (Gun.tGunType == NMHGun.GunType.RIPLE)
            {
                Body.GetComponent<Animator>().SetBool("RifeReloading", true);
            }
            else
            {
                Body.GetComponent<Animator>().SetBool("ShotgunReloading", true); ;
            }

            Gun.Reload();

            yield return new WaitForSeconds(Gun.fReloadDelay);

            if (Gun.tGunType == NMHGun.GunType.PISTOL)
            {
                Body.GetComponent<Animator>().SetBool("PistolReloading", false);
            }
            else if (Gun.tGunType == NMHGun.GunType.RIPLE)
            {
                Body.GetComponent<Animator>().SetBool("RifeReloading", false);
            }
            else
            {
                Body.GetComponent<Animator>().SetBool("ShotgunReloading", false); ;
            }
        }

    }

    IEnumerator GunStop()
    {
        
        possShot = false;

        yield return new WaitForSeconds(Gun.fShootDelay);

        possShot = true;
    }

    IEnumerator ShotWait()
    {

        if (Gun.tGunType == NMHGun.GunType.PISTOL)
        {
            Body.GetComponent<Animator>().SetBool("PistolShooting", true);
        }
        else if (Gun.tGunType == NMHGun.GunType.RIPLE)
        {
            Body.GetComponent<Animator>().SetBool("RifeShooting", true);
        }
        else
        {
            Body.GetComponent<Animator>().SetBool("ShotgunShooting", true); ;
        }


        yield return new WaitForSecondsRealtime(0.1f);



        if (Gun.tGunType == NMHGun.GunType.PISTOL)
        {
            Body.GetComponent<Animator>().SetBool("PistolShooting", false);
        }
        else if (Gun.tGunType == NMHGun.GunType.RIPLE)
        {
            Body.GetComponent<Animator>().SetBool("RifeShooting", false);
        }
        else
        {
            Body.GetComponent<Animator>().SetBool("ShotgunShooting", false); ;
        }

        isShoting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Plane")
        {
            //JumpCount = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Plane")
        {
            //JumpCount++;
        }
    }
}
