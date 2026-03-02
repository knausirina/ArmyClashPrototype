
    using System;
    using UnityEngine;

    public class EffectsService
    {
        private readonly ParticleSystem _damageParticleSystem;

        public EffectsService(ParticleSystem damageParticleSystem)
        {
            _damageParticleSystem = damageParticleSystem;
        }

        public void ShowDamage(Vector3 position, float damage)
        {
            var emitParams = new ParticleSystem.EmitParams
            {
                position = position,
                applyShapeToPosition = true
            };
            _damageParticleSystem.Emit(emitParams, Math.Clamp((int)(10 * damage), 1, 10));
        }
    }