using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Weapon hoveredWeapon = null;
    public AmmoBox hoveredAmmoBox = null;
    private float interactionDistance = 5f; // Добавлено расстояние для взаимодействия

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit)) // Если луч сталкивается с каким-либо объектом
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;
            float distanceToObject = Vector3.Distance(Camera.main.transform.position, objectHitByRaycast.transform.position); // Вычисляем расстояние до объекта

            // Взаимодействие с оружием
            if (objectHitByRaycast.GetComponent<Weapon>() && distanceToObject <= interactionDistance) // Проверяем расстояние
            {
                if (hoveredWeapon != null) // Сбрасываем предыдущее подсвеченное оружие
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                
                hoveredWeapon = objectHitByRaycast.GetComponent<Weapon>(); // Устанавливаем новое оружие для подсветки
                hoveredWeapon.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickUpWeapon(objectHitByRaycast.gameObject); // Подбор оружия
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                    print("The player picked up a weapon! " + objectHitByRaycast.name);
                    hoveredWeapon = null; // Сброс после взаимодействия
                }
            }
            else
            {
                if (hoveredWeapon != null) 
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                    hoveredWeapon = null; // Сброс, если объект вне диапазона
                }
            }

            // Взаимодействие с патронами
            if (objectHitByRaycast.GetComponent<AmmoBox>() && distanceToObject <= interactionDistance) // Проверяем расстояние
            {
                if (hoveredAmmoBox != null) // Сбрасываем предыдущий подсвеченный AmmoBox
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                
                hoveredAmmoBox = objectHitByRaycast.GetComponent<AmmoBox>(); // Устанавливаем новый AmmoBox для подсветки
                hoveredAmmoBox.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickUpAmmo(hoveredAmmoBox); // Подбор патронов
                    Destroy(objectHitByRaycast.gameObject);
                    hoveredAmmoBox = null; // Сброс после взаимодействия
                }
            }
            else
            {
                if (hoveredAmmoBox != null)
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                    hoveredAmmoBox = null; // Сброс, если объект вне диапазона
                }
            }   
        }
        else // Если луч не попал ни в один объект
        {
            if (hoveredWeapon != null)
            {
                hoveredWeapon.GetComponent<Outline>().enabled = false;
                hoveredWeapon = null; // Сброс подсветки
            }
            if (hoveredAmmoBox != null)
            {
                hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                hoveredAmmoBox = null; // Сброс подсветки
            }
        }
    }
}
