using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject BulletTrail;
    public GameObject playerCharacter;
    public ParticleSystem muzzleFlash;

    //public GameObject Grenade;
    //public GameObject Rocket;

    Vector3 mousePosition;

    int bulletCount = 1;
    float weaponRange = 50f;
    float bulletDamage = 50f;

    float fireRateCount = 0f;
    float defaultFireRateCount = 0.05f;

    float spreadChanceCount = 0f;
    float spreadChanceIncrement = 0.2f;
    float spreadChanceMax = 1f;
    float spreadTimer = 0f;
    float spreadTimerMax = 0.5f;

    bool hasAmmo = false;
    bool reloading = true;
    float reloadTimeCount = 0f;
    float reloadTime = 1f;
    int magCount = 0;
    int defaultMagSize = 30;
    bool fullAuto = true;

    float investigateBubbleMod = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        combat();
        reload();
        spreadManager();
        if(spreadTimer <= 0){
            look();
        }
    }

    void look(){
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    Quaternion returnMousePosition(){
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z);
    }

    void combat(){
        if(fireRateCount < defaultFireRateCount){
            Debug.Log("Waiting on Firerate");
            fireRateCount += Time.deltaTime;
        }
        if(Input.GetMouseButton(0) && fireRateCount > defaultFireRateCount && hasAmmo && fullAuto){
            Debug.Log("Shot Fire");
            playerCharacter.GetComponent<PlayerCharacter>().shotFired(investigateBubbleMod);
            muzzleFlash.Play();
            shoot();
        }
        if(Input.GetMouseButtonDown(0) && fireRateCount > defaultFireRateCount && hasAmmo && !fullAuto){
            Debug.Log("Shot Fire");
            playerCharacter.GetComponent<PlayerCharacter>().shotFired(investigateBubbleMod);
            muzzleFlash.Play();
            shoot();
        }
        
    }

    void shoot(){
        fireRateCount = 0f;
        if(spreadChanceCount < spreadChanceMax){
            spreadChanceCount += spreadChanceIncrement;
            spreadTimer = spreadTimerMax;
        }

        for(int i = 0; i < bulletCount; i++){
            spawnBullet();
        }
        magCount--;
    }

    void reload(){
        if(magCount <= 0 && !reloading){
            reloadTimeCount = 0f;
            reloading = true;
            hasAmmo = false;
            Debug.Log("Begun Reloading");
        }
        if(reloadTimeCount < reloadTime && reloading){
            reloadTimeCount += Time.deltaTime;
            Debug.Log("Reloading");
        }
        if(reloadTimeCount > reloadTime && reloading){
            Debug.Log("Done Reloading");
            reloading = false;
            magCount = defaultMagSize;
            hasAmmo = true;
        }
    }

    void spreadManager(){
        if(spreadChanceCount > 0 && spreadTimer < 0){
            spreadChanceCount -= 0.01f;
        }else if(spreadChanceCount <= 0){
            spreadChanceCount = 0;
        }
        if(spreadTimer > 0){
            spreadTimer -= Time.deltaTime;
        }
    }

    void spawnBullet(){
        float spread = Random.Range(-spreadChanceCount,spreadChanceCount);
        spreadDealer(spread);
        RaycastHit2D bullet = Physics2D.Raycast(transform.position, transform.right);
        var bulletTrail = Instantiate(BulletTrail);
        var bulletTrailScript = bulletTrail.GetComponent<BulletTrail>();

        bulletTrail.transform.position = transform.position;
        bulletTrail.transform.rotation = transform.rotation;

        bulletTrailScript.setTargetPosition(bullet.point);
        
        if(bullet.collider.gameObject.tag == "Enemy"){
            var zombieScript = bullet.collider.GetComponent<Zombie>();
            zombieScript.takeDamage(bulletDamage);
        }
    }

    void spreadDealer(float spread){
        transform.Rotate(0,0,spread);
    }
}
