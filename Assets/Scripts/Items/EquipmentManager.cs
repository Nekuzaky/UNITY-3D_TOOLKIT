using UnityEngine;
using GameJamToolkit.Combat;

namespace GameJamToolkit.Items
{
    /// <summary>Manages the currently equipped weapon. Spawns / despawns the prefab.</summary>
    public sealed class EquipmentManager : MonoBehaviour
    {
        [SerializeField] private Transform _weaponSlot;

        private GameObject _currentWeaponGO;
        public WeaponBase CurrentWeapon { get; private set; }

        public void Equip(WeaponItem item)
        {
            Debug.Assert(item != null, "[EquipmentManager.Equip] item is null"); // R5
            Debug.Assert(_weaponSlot != null, "[EquipmentManager.Equip] _weaponSlot is null"); // R5
            Unequip();
            if (item == null || item.WeaponPrefab == null) return;
            _currentWeaponGO = Instantiate(item.WeaponPrefab, _weaponSlot); // R3 exempt: one-shot equip, not gameplay runtime
            _currentWeaponGO.transform.localPosition = Vector3.zero;
            _currentWeaponGO.transform.localRotation = Quaternion.identity;
            CurrentWeapon = _currentWeaponGO.GetComponent<WeaponBase>();
            if (CurrentWeapon != null) CurrentWeapon.SetOwner(transform.root.gameObject);
        }

        public void Unequip()
        {
            if (_currentWeaponGO != null) Destroy(_currentWeaponGO);
            _currentWeaponGO = null;
            CurrentWeapon = null;
        }
    }
}
