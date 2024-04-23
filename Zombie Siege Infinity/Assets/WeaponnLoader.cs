using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoader : MonoBehaviour
{
    public Weapon weapon; // Основной компонент оружия (сделайте его публичным, чтобы назначить через редактор Unity)
    public List<GameObject> childComponents; // Список дочерних компонентов, таких как магазины, прицелы и т.д.

    void Start()
    {
        LoadWeapon();
    }

    void LoadWeapon()
    {
        // Активация основного компонента оружия
        weapon.gameObject.SetActive(true);
        // Деактивация всех дочерних компонентов
        foreach (var component in childComponents)
        {
            component.SetActive(false);
        }

        // Активация дочерних компонентов с задержкой
        StartCoroutine(ActivateComponentsWithDelay());
    }

    IEnumerator ActivateComponentsWithDelay()
    {
        foreach (var component in childComponents)
        {
            yield return new WaitForSeconds(0.2f);  // Задержка перед активацией каждого компонента
            component.SetActive(true);
        }
    }
}
