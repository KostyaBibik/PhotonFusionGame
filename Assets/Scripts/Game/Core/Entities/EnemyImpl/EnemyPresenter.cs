using System;
using Fusion;
using UniRx;
using UnityEngine;

namespace Game.Core.Entities.EnemyImpl
{
    public class EnemyPresenter : NetworkEntityPresenter<EnemyView, EnemyModel>, IDamageable<EnemyPresenter>
    {
        [SerializeField] private EnemyView _view;
        [SerializeField] private EnemyModel _model;

        public new EnemyView View => _view;
        public new EnemyModel Model => _model;

        public Action<EnemyPresenter> OnDeath { get; set; }
        
        private void Start()
        {
            _view.InvokeUpdateSliderValueLocal(_model.NetworkedHealth / _model.StartHealth);
            
            if (!Object.HasStateAuthority)
            {
                return;
            }
                
            _model
                .Health
                .AsObservable()
                .Subscribe(value =>
                {
                    _view.InvokeUpdateSliderValueRPC(_model.Health.Value / _model.StartHealth);
                        
                    if (!CheckIsAlive(value))
                    {
                        Death();
                    }
                })
                .AddTo(_view);
        }

        public void TakeDamage(float damage, PlayerRef damageInvoker)
        {
            if (!CanBeAttacked())
            {
                return;
            }
            
            if (Object.HasStateAuthority) 
            {
                
                
                Model.NetworkedHealth -= damage;
                _model.LastDamageInvoker = damageInvoker;
            }
            else
            {
                Rpc_TakeDamage(damage, damageInvoker);
            }
        }
        
        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void Rpc_TakeDamage(float damage, PlayerRef damageInvoker)
        {
            Model.NetworkedHealth -= damage;
            _model.LastDamageInvoker = damageInvoker;
        }

        public bool CanBeAttacked()
            =>  Object != null && CheckIsAlive(Model.NetworkedHealth);

        private bool CheckIsAlive(float currentHp)
            => currentHp > 0;

        private void Death()
        {
            OnDeath?.Invoke(this);
            
            Debug.Log($"DEATH: {_model.Grade}");
        }
    }
}