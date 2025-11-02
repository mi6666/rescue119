using Interface.ViewInterface.Global;
using Module.EditorExtension.Runtime;
using UnityEngine;
using VContainer;
using View.InGame.Player;

namespace Installer.OutGame.StageSelect
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerInstaller : MonoBehaviour
    {
        // --- VContainer Property Injection ---
        [Inject] public IInput_MoveVectorView InputProvider { private get; set; }

        [Header("Movement Settings")] [SerializeField]
        private float moveForce = 50f;

        [SerializeField] private float maxSpeed = 6f;
        [SerializeField] private float groundDrag = 5f;
        [SerializeField] private float airDrag = 0.5f;
        [SerializeField] private PlayerAnimatorView playerAnimatorView;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation; // 回転を物理的に固定
        }

        private void FixedUpdate()
        {
            if (InputProvider == null)
            {
                Debug.LogWarning($"{nameof(PlayerInstaller)}: InputProvider not injected.");
                return;
            }

            Vector2 input = InputProvider.Pool();

            // 入力がない場合は水平速度を徐々に落とす
            if (input.sqrMagnitude < 0.0001f)
            {
                _rigidbody.drag = groundDrag;
                return;
            }

            // XZ平面での移動方向
            Vector3 moveDir = new Vector3(input.x, 0f, input.y).normalized;

            // 速度制限
            Vector3 horizontalVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            // var ratio = horizontalVel.magnitude / maxSpeed;
            // DebugLogger.Log("ratio", ratio.ToString("F1"));
            // playerAnimatorView.SetFloat("Walk", ratio);
            // if (ratio < 1)
            // {
            //     _rigidbody.AddForce(moveDir * moveForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
            // }
            if (horizontalVel.magnitude < maxSpeed)
            {
                _rigidbody.AddForce(moveDir * moveForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }

            // 摩擦（Drag）の調整
            _rigidbody.drag = IsGrounded() ? groundDrag : airDrag;

            // 向きを移動方向に合わせる（カメラではなく進行方向）
            if (moveDir.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(moveDir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 0.15f);
            }
        }

        private bool IsGrounded()
        {
            const float groundCheckDist = 0.1f;
            return Physics.Raycast(transform.position, Vector3.down, groundCheckDist + 0.01f);
        }
    }
}