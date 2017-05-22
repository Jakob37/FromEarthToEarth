using UnityEngine;
using System.Collections;
using System;
using Assets.Particles;

namespace DigitalRuby.RainMaker
{
    public class RainScript2D : BaseRainScript
    {
        private static readonly Color32 explosionColor = new Color32(255, 255, 255, 255);

        public float base_level_size_unit;

        private float cameraMultiplier = 1.0f;
        private Bounds visibleBounds;
        private float yOffset;
        private float visibleWorldWidth;
        private float initialEmissionRain;
        private float initialStartSpeedRain;
        private float initialStartSizeRain;
        private float initialStartSpeedMist;
        private float initialStartSizeMist;
        private float initialStartSpeedExplosion;
        private float initialStartSizeExplosion;
        private readonly ParticleSystem.Particle[] particles = new ParticleSystem.Particle[4096];

        private float start_speed;
        private float start_size;

        public bool move_rain_with_camera;

        [Tooltip("The starting y offset for rain and mist. This will be offset as a percentage of visible height from the top of the visible world.")]
        public float RainHeightMultiplier = 0.15f;

        [Tooltip("The total width of the rain and mist as a percentage of visible width")]
        public float RainWidthMultiplier = 1.5f;

        [Tooltip("Collision mask for the rain particles")]
        public LayerMask CollisionMask = -1;

        [Tooltip("Lifetime to assign to rain particles that have collided. 0 for instant death. This can allow the rain to penetrate a little bit beyond the collision point.")]
        [Range(0.0f, 0.5f)]
        public float CollisionLifeTimeRain = 0.02f;

        [Tooltip("Multiply the velocity of any mist colliding by this amount")]
        [Range(0.0f, 0.99f)]
        public float RainMistCollisionMultiplier = 0.75f;

        private void EmitExplosion(ref Vector3 pos)
        {
            int count = UnityEngine.Random.Range(2, 5);
            while (count != 0)
            {
                float xVelocity = UnityEngine.Random.Range(-2.0f, 2.0f) * cameraMultiplier;
                float yVelocity = UnityEngine.Random.Range(1.0f, 3.0f) * cameraMultiplier;
                float lifetime = UnityEngine.Random.Range(0.1f, 0.2f);
                float size = UnityEngine.Random.Range(0.05f, 0.1f) * cameraMultiplier;
                ParticleSystem.EmitParams param = new ParticleSystem.EmitParams();
                param.position = pos;
                param.velocity = new Vector3(xVelocity, yVelocity, 0.0f);
                param.startLifetime = lifetime;
                param.startSize = size;
                param.startColor = explosionColor;
                RainExplosionParticleSystem.Emit(param, 1);
                count--;
            }
        }

        private void TransformParticleSystem(ParticleSystem p, float initialStartSpeed, float initialStartSize, bool translate_rain_pos=false)
        {
            if (p == null)
            {
                return;
            }

            if (translate_rain_pos) {
                p.transform.position = new Vector3(transform.position.x, visibleBounds.max.y + yOffset, p.transform.position.z);
            }
            else {
                p.transform.position = new Vector3(transform.position.x, visibleBounds.max.y + yOffset, p.transform.position.z);
                // p.transform.position = new Vector3(Camera.transform.position.x, visibleBounds.max.y + yOffset, p.transform.position.z);
            }
            p.transform.localScale = new Vector3(visibleWorldWidth * RainWidthMultiplier, 1.0f, 1.0f);

            var main = p.main;
            start_speed = initialStartSpeed * cameraMultiplier;
            start_size = initialStartSize * cameraMultiplier;
            main.startSpeed = start_speed;
            main.startSize = start_size;


            //p.main.startSpeed = initialStartSpeed * cameraMultiplier;
            // p.startSpeed = initialStartSpeed * cameraMultiplier;
            // p.startSize = initialStartSize * cameraMultiplier;
        }

        private void CheckForCollisionsRainParticles()
        {
            int count = 0;
            bool changes = false;

            if (CollisionMask != 0)
            {
                count = RainFallParticleSystem.GetParticles(particles);
                RaycastHit2D hit;

                for (int i = 0; i < count; i++)
                {
                    Vector3 pos = particles[i].position + RainFallParticleSystem.transform.position;
                    hit = Physics2D.Raycast(pos, particles[i].velocity.normalized, particles[i].velocity.magnitude * Time.deltaTime);
                    if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & CollisionMask) != 0)
                    {
                        if (CollisionLifeTimeRain == 0.0f)
                        {
                            particles[i].remainingLifetime = 0.0f;
                        }
                        else
                        {
                            particles[i].remainingLifetime = Mathf.Min(particles[i].remainingLifetime, UnityEngine.Random.Range(CollisionLifeTimeRain * 0.5f, CollisionLifeTimeRain * 2.0f));
                            pos += (particles[i].velocity * Time.deltaTime);
                        }
                        changes = true;
                    }
                }
            }

            if (RainExplosionParticleSystem != null)
            {
                if (count == 0)
                {
                    count = RainFallParticleSystem.GetParticles(particles);
                }
                for (int i = 0; i < count; i++)
                {
                    if (particles[i].remainingLifetime < 0.24f)
                    {
                        Vector3 pos = particles[i].position + RainFallParticleSystem.transform.position;
                        EmitExplosion(ref pos);
                    }
                }
            }
            if (changes)
            {
                RainFallParticleSystem.SetParticles(particles, count);
            }
        }

        private void CheckForCollisionsMistParticles()
        {
            if (RainMistParticleSystem == null || CollisionMask == 0)
            {
                return;
            }

            int count = RainMistParticleSystem.GetParticles(particles);
            bool changes = false;
            RaycastHit2D hit;

            for (int i = 0; i < count; i++)
            {
                Vector3 pos = particles[i].position + RainMistParticleSystem.transform.position;
                hit = Physics2D.Raycast(pos, particles[i].velocity.normalized, particles[i].velocity.magnitude * Time.deltaTime);
                if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & CollisionMask) != 0)
                {
                    particles[i].velocity *= RainMistCollisionMultiplier;
                    changes = true;
                }
            }

            if (changes)
            {
                RainMistParticleSystem.SetParticles(particles, count);
            }
        }

        private ParticleSystem.MainModule FindParticleMainInChildren(string main_type) {

            ParticleSystemScript particle_script;

            if (main_type == "rain") {
                particle_script = GetComponentInChildren<RainFallParticleSystem>();
            }
            else if (main_type == "mist") {
                particle_script = GetComponentInChildren<RainMistParticleSystem>();
            }
            else if (main_type == "explosion") {
                particle_script = GetComponentInChildren<RainExplosionParticleSystem>();
            }
            else {
                throw new ArgumentException("Unknown main type: " + main_type);
            }

            ParticleSystem p = particle_script.gameObject.GetComponent<ParticleSystem>();
            var main = p.main;
            return main;
        }

        protected override void Start()
        {
            base.Start();

            AdjustForLevelSize();

            var rain_main = FindParticleMainInChildren("rain");
            var mist_main = FindParticleMainInChildren("mist");
            var expl_main = FindParticleMainInChildren("explosion");

            initialEmissionRain = RainFallParticleSystem.emission.rateOverTime.constantMax;
            initialStartSpeedRain = rain_main.startSpeed.constant;
            initialStartSizeRain = rain_main.startSize.constant;

            if (RainMistParticleSystem != null)
            {
                initialStartSpeedMist = mist_main.startSpeed.constant;
                initialStartSizeMist = mist_main.startSize.constant;
            }

            if (RainExplosionParticleSystem != null)
            {
                initialStartSpeedExplosion = expl_main.startSpeed.constant;
                initialStartSizeExplosion = expl_main.startSize.constant;
            }
        }

        private void AdjustForLevelSize() {
            transform.position = new Vector3(0, 0, 0);

            LeftEdge left_edge = FindObjectOfType<LeftEdge>();
            RightEdge right_edge = FindObjectOfType<RightEdge>();

            float width = right_edge.gameObject.transform.position.x - left_edge.gameObject.transform.position.x;
            float scaling = width / base_level_size_unit;

            transform.localScale = new Vector3(scaling, 1, 1);
        }

        protected override void Update()
        {
            base.Update();

            cameraMultiplier = (Camera.orthographicSize * 0.25f);
            visibleBounds.min = Camera.main.ViewportToWorldPoint(Vector3.zero);
            visibleBounds.max = Camera.main.ViewportToWorldPoint(Vector3.one);
            visibleWorldWidth = visibleBounds.size.x;
            yOffset = (visibleBounds.max.y - visibleBounds.min.y) * RainHeightMultiplier;
            
            TransformParticleSystem(RainFallParticleSystem, initialStartSpeedRain, initialStartSizeRain, translate_rain_pos:move_rain_with_camera);
            TransformParticleSystem(RainMistParticleSystem, initialStartSpeedMist, initialStartSizeMist);
            TransformParticleSystem(RainExplosionParticleSystem, initialStartSpeedExplosion, initialStartSizeExplosion);
            
            CheckForCollisionsRainParticles();
            CheckForCollisionsMistParticles();
        }

        protected override float RainFallEmissionRate()
        {
            return initialEmissionRain * RainIntensity;
        }

        protected override bool UseRainMistSoftParticles
        {
            get
            {
                return false;
            }
        }
    }
}