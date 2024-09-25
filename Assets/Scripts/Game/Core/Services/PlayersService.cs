using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fusion;
using Game.Core.Entities.PlayerImpl;
using Zenject;

namespace Game.Core.Services
{
    public class PlayersService : IDisposable
    {
        [Inject] private NetworkRunner _networkRunner;
        
        private List<PlayerPresenter> _players = new List<PlayerPresenter>();
        private PlayerPresenter _localPlayer;
        
        public void AddPlayer(PlayerPresenter newPlayer)
        {
            if (!_players.Contains(newPlayer))
            {
                _players.Add(newPlayer);

                if (newPlayer.View.Object.HasStateAuthority)
                {
                    _localPlayer = newPlayer;
                }
            }
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void SetLocalPlayer()
        {
            _localPlayer = _networkRunner.GetPlayerObject(_networkRunner.LocalPlayer).GetComponent<PlayerPresenter>();
        }
        
        public PlayerPresenter GetPlayer(PlayerRef player)
        {
            return _players.FirstOrDefault(p => p.Model.Player == player);
        }

        public async Task<PlayerPresenter> GetLocalPlayer()
        {
            while (_localPlayer == null)
            {
                await Task.Yield(); 
            }

            return _localPlayer;
        }

        public void Dispose()
        {
            _players.Clear();
        }
    }
}