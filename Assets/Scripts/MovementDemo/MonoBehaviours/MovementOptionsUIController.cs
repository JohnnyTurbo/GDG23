using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace TMG.GDG23
{
    public class MovementOptionsUIController : MonoBehaviour
    {
        public static MovementOptionsUIController Instance;
        
        [SerializeField] private Button _regularMoveButton;
        [SerializeField] private Button _hopMoveButton;

        private Image _regularMoveButtonImage;
        private Image _hopMoveButtonImage;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            _regularMoveButton.onClick.AddListener(EnableRegularMove);
            _hopMoveButton.onClick.AddListener(EnableHopMove);

            _regularMoveButtonImage = _regularMoveButton.GetComponent<Image>();
            _hopMoveButtonImage = _hopMoveButton.GetComponent<Image>();
            
            _regularMoveButtonImage.color = Color.green;
            _hopMoveButtonImage.color = Color.red;
        }

        public void SetMoveButtonColor(bool isGreen)
        {
            var color = isGreen ? Color.green : Color.red;
            _regularMoveButtonImage.color = color;
        }
        
        public void SetHopButtonColor(bool isGreen)
        {
            var color = isGreen ? Color.green : Color.red;
            _hopMoveButtonImage.color = color;
        }
        
        private void OnDisable()
        {
            _regularMoveButton.onClick.RemoveAllListeners();
            _hopMoveButton.onClick.RemoveAllListeners();
        }

        private void EnableRegularMove()
        {
            World.DefaultGameObjectInjectionWorld.Unmanaged.GetExistingSystemState<PlayerMoveSystem>().Enabled = true;
            World.DefaultGameObjectInjectionWorld.Unmanaged.GetExistingSystemState<PlayerHopSystem>().Enabled = false;
        }

        private void EnableHopMove()
        {
            World.DefaultGameObjectInjectionWorld.Unmanaged.GetExistingSystemState<PlayerMoveSystem>().Enabled = false;
            World.DefaultGameObjectInjectionWorld.Unmanaged.GetExistingSystemState<PlayerHopSystem>().Enabled = true;
        }
    }
}