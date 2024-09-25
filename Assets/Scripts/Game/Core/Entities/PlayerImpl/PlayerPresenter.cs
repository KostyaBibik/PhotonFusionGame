using Fusion;
using Game.Core.Installers;
using Game.Core.Services;
using UniRx;
using UnityEngine;

namespace Game.Core.Entities.PlayerImpl
{
    public class PlayerPresenter : NetworkEntityPresenter<PlayerView, PlayerModel>
    {
        [SerializeField] private PlayerView _view;
        [SerializeField] private PlayerModel _model;

        private PlayersService _playersService;
        
        public new PlayerView View => _view;
        public new PlayerModel Model => _model;

        private void Awake()
        {
            _playersService = DiContainerRef.Container.Resolve<PlayersService>();
        }

        private void Start()
        {
            _model.AttackRange
                .AsObservable()
                .Subscribe(value => _view.AttackComponent.UpdateRange(value))
                .AddTo(_view);
            
            _playersService.SetLocalPlayer();
        }

        #region AttackRange_Stat

        public void UpgradeRange(float additiveValue)
        {
            if (Object.StateAuthority)
            {
                Model.NetworkedAttackRange += additiveValue;
            }
            else
            {
                Rpc_UpgradeRange(additiveValue);
            }
        }
        
        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void Rpc_UpgradeRange(float additiveValue)
        {
            Model.NetworkedAttackRange += additiveValue;
        }

        #endregion
        
        #region DamagePerSecond_Stat
        
        public void UpgradeDamagePerSecond(float additiveValue)
        {
            if (Object.StateAuthority)
            {
                Model.NetworkedDamagePerSecond += additiveValue;
            }
            else
            {
                Rpc_UpgradeDamagePerSecond(additiveValue);
            }
        }
        
        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void Rpc_UpgradeDamagePerSecond(float additiveValue)
        {
            Model.NetworkedDamagePerSecond += additiveValue;
        }
        
        #endregion

        #region SpeedMove_Stat

        public void UpgradeSpeedMove(float additiveValue)
        {
            if (Object.StateAuthority)
            {
                Model.NetworkedSpeedMoving += additiveValue;
            }
            else
            {
                Rpc_UpgradeSpeedMove(additiveValue);
            }
        }
        
        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void Rpc_UpgradeSpeedMove(float additiveValue)
        {
            Model.NetworkedSpeedMoving += additiveValue;
        }
        
        #endregion

        #region CountKills_Stat

        public void AddCountKills()
        {
            if (Object.StateAuthority)
            {
                Model.NetworkedCountKills++;
            }
            else
            {
                Rpc_AddCountKills();
            }
        }
        
        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void Rpc_AddCountKills()
        {
            Model.NetworkedCountKills++;
        }

        #endregion
        
        
    }
}