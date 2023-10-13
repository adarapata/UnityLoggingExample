namespace CoinGame.PlayerCharacters
{
    public class Jumper
    {
        private readonly float _jumpSpeed;
        private readonly IJumpPresentation _presentation;

        public Jumper(IJumpPresentation presentation, float jumpSpeed)
        {
            _presentation = presentation;
            _jumpSpeed = jumpSpeed;
        }
        
        public void Jump()
        {
            if (_presentation.IsGround)
            {
                _presentation.Jump(_jumpSpeed);
            }
        }
    }
}