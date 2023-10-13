using CoinGame.GameSystems.Actors;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CoinGame.PlayerCharacters
{
    public class PlayerCharacterBehaviour : MonoBehaviour, IMovePresentation, IJumpPresentation, IEmotePresentation
    {
        [SerializeField] 
        private SpriteRenderer _renderer;
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private Animator _animator;
        [SerializeField] 
        private ContactFilter2D _ContactFilter;

        private PlayerCharacterParameter _parameter;

        private float NormalizeSpeed => _rigidbody.velocity.magnitude / _parameter.speedLimit;
        
        public void Construct(Vector3 spawnPosition, PlayerCharacterParameter parameter)
        {
            transform.position = spawnPosition;
            _parameter = parameter;
        }

        private void Update()
        {
            UpdateAnimation();
        }
        
        private void UpdateAnimation()
        {
            // 毎フレームアニメーションのパラメータを更新する
            _animator.SetFloat("Speed", NormalizeSpeed);
            _animator.SetFloat("JumpSpeed", _rigidbody.velocity.y);
            _animator.SetBool("IsGrounded", IsGround);
        }

        public void Jump(float jumpSpeed) => _rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

        public bool IsGround => _rigidbody.IsTouching(_ContactFilter);
        
        public void Move(Vector2 velocity)
        {
            _rigidbody.AddForce(velocity, ForceMode2D.Force);
            // 画像反転処理
            if (velocity.x != 0F)
            {
                _renderer.flipX = velocity.x < 0;
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            // 取得したことをコインに伝える
            ExecuteEvents.Execute<ICoin>(other.gameObject, null, (target, data) => target.Obtain());
        }

        public float CurrentSpeed => Mathf.Abs(_rigidbody.velocity.x);
        
        /// <summary>勝利ポーズの表示</summary>
        public void ShowWinEmote() => _animator.SetTrigger("GameClear");
        /// <summary>敗北ポーズの表示</summary>
        public void ShowLoseEmote() => _animator.SetTrigger("GameOver");

        /// <summary>
        /// ZenjectのFactory機能を使ったFactoryパターン
        /// </summary>
        public class Factory
        {
            private readonly PlayerCharacterBehaviour _prefab;
            
            public Factory(PlayerCharacterBehaviour prefab)
            {
                _prefab = prefab;
            }
            
            public PlayerCharacterBehaviour Create()
            {
                return Instantiate(_prefab, Vector3.zero, Quaternion.identity);
            }
        }
    }
}
