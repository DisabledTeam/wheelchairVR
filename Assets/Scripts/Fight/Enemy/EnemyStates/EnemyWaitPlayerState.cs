namespace Fight
{
    public class EnemyWaitPlayerState : EnemyState
    {
        private bool _isSubscribed;

        public override void Enter()
        {
            if (PlayerTargetMarker.GetInstance() == null)
            {
                PlayerTargetMarker.PlayerTargetMarkerSpawned.AddListener(OnPlayerSpawned);
                _isSubscribed = true;
            }
            else
            {
                enemy.SetPlayerMarker(PlayerTargetMarker.GetInstance());
                GoToFollow();
            }
        }

        public override void Leave()
        {
            if (_isSubscribed)
            {
                PlayerTargetMarker.PlayerTargetMarkerSpawned.RemoveListener(OnPlayerSpawned);
                _isSubscribed = false;
            }
        }


        private void OnPlayerSpawned()
        {
            enemy.SetPlayerMarker(PlayerTargetMarker.GetInstance());
            GoToFollow();
        }
    }
}