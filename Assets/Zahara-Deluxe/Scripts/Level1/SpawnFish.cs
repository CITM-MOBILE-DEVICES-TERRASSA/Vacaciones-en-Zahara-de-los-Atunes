    using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;


    [System.Serializable]
    public class SpawnablePrefab    
    {
        public GameObject prefab; // Prefab a spawnear
        [Range(0f, 1f)] public float spawnProbability; // Probabilidad de spawneo (entre 0 y 1)
    }

    public class SpawnFish : MonoBehaviour
    {
        private Timer Tiempo;
        private GameObject CartaPescao;
        public List<SpawnablePrefab> spawnablePrefabs; // Lista de prefabs configurables en el inspector
        public Vector2 cooldownRange = new Vector2(1f, 5f); // Rango de cooldown (mínimo y máximo)
        public Transform spawnPoint; // Punto donde se spawnearán los objetos

        private float timer; // Temporizador interno
        private float currentCooldown; // Cooldown actual
        private bool carta = false;
        void Start()
        {
            Time.timeScale = 1;
            Tiempo = GameObject.Find("Overlay").GetComponent<Timer>();
            CartaPescao = GameObject.Find("CartaPescaos");
            Tiempo.isPaused = true;
            CartaPescao.SetActive(true);    
            carta = true;
            SetRandomCooldown(); // Inicializa con un cooldown aleatorio
            timer = currentCooldown;
        }

        void Update()
        {
        if (Input.GetMouseButtonDown(0) && carta==true)
        {
            Tiempo.isPaused = false;
            carta =false;
            CartaPescao.SetActive(false);
        }
            if (carta)
            {
             return;
            }
        
        timer -= Time.deltaTime;

            if (timer <= 0)
            {
                SpawnPrefab();
                SetRandomCooldown(); // Genera un nuevo cooldown aleatorio
                timer = currentCooldown; // Reinicia el temporizador
            }
        }
            
        void SpawnPrefab()
        {
            if (spawnablePrefabs.Count == 0)
            {
                Debug.LogWarning("No hay prefabs configurados para spawnear.");
                return;
            }

            // Seleccionar un prefab basado en las probabilidades
            GameObject prefabToSpawn = GetRandomPrefab();
            if (prefabToSpawn != null)
            {
                Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
            }
        }

        GameObject GetRandomPrefab()
        {
            float totalProbability = 0f;

            // Suma todas las probabilidades de la lista
            foreach (var spawnable in spawnablePrefabs)
            {
                totalProbability += spawnable.spawnProbability;
            }

            // Genera un número aleatorio entre 0 y la probabilidad total
            float randomPoint = Random.value * totalProbability;

            // Encuentra el prefab correspondiente
            float currentSum = 0f;
            foreach (var spawnable in spawnablePrefabs)
            {
                currentSum += spawnable.spawnProbability;
                if (randomPoint <= currentSum)
                {
                    return spawnable.prefab;
                }
            }

            return null; // En caso de que no se encuentre un prefab
        }

        void SetRandomCooldown()
        {
            // Genera un cooldown aleatorio dentro del rango definido
            currentCooldown = Random.Range(cooldownRange.x, cooldownRange.y);
        }
    }
