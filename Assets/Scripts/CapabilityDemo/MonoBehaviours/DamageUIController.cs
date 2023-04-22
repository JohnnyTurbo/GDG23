using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace TMG.GDG23
{
    public class DamageUIController : MonoBehaviour
    {
        [SerializeField] private GameObject _damageIconPrefab;
        [SerializeField] private Transform _worldSpaceUICanvas;

        private Dictionary<int, int> _damageTextCounter;
        private Vector3[] _offsets;

        private void Start()
        {
            _offsets = CalculateOffsets(2.5f, 60f);
        }

        private Vector3[] CalculateOffsets(float distance, float angle)
        {
            var rad = angle * Mathf.Deg2Rad;
            var pos0 = new Vector3(distance * Mathf.Cos(rad), distance * Mathf.Sin(rad), 0f);
            var pos1 = new Vector3(0f, distance, 0f);
            var pos2 = new Vector3(-distance * Mathf.Cos(rad), distance * Mathf.Sin(rad), 0f);

            return new[] { pos0, pos1, pos2 };
        }


        private void OnEnable()
        {
            _damageTextCounter = new Dictionary<int, int>();
            World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<DisplayDamageIconSystem>().OnDamage +=
                DisplayDamageIcon;
        }

        private void OnDisable()
        {
            if (World.DefaultGameObjectInjectionWorld == null) return;
            World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<DisplayDamageIconSystem>().OnDamage -=
                DisplayDamageIcon;
        }
        
        private void DisplayDamageIcon(int entityIndex, float damageAmount, float3 entityPosition)
        {
            var newIcon = Instantiate(_damageIconPrefab, entityPosition, Quaternion.identity, _worldSpaceUICanvas);
            var newIconText = newIcon.GetComponent<TextMeshProUGUI>();
            newIconText.text = damageAmount.ToString();
            _damageTextCounter.TryAdd(entityIndex, 0);
            var offset = _offsets[_damageTextCounter[entityIndex]];
            var newPosition = (Vector3)entityPosition + offset;
            _damageTextCounter[entityIndex] += 1;
            _damageTextCounter[entityIndex] %= 3;
            var uiController = newIcon.GetComponent<MoveDamageIconText>();
            uiController.EndPosition = newPosition;
        }
    }
}