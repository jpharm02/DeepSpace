using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS SCRIPT IS LEFT IN THIS PROJECT FOR FUTURE WORK ONLY.  CURRENTLY NOT UTILIZED IN SOLUTION.
public class AsteroidManager : MonoBehaviour
{
    public int numberOfAsteroidsOnAnAxis;
    public int numberOfPlanetsOnAnAxis; 
    public int numberOfGalaxiesOnAnAxis;

    public int asteroidGridSpacing;
    public int planetGridSpacing; 
    public int galaxyGridSpacing;

    public GameObject[] asteroids;
    public GameObject[] planets;
    public GameObject[] galaxies;

    private Vector3 defPosAsteroids;

    public void CleanLevelArea()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(500, 500, 500));

        foreach (Collider toDestroy in colliders)
        {
            if (toDestroy.gameObject.tag == "asteroid" || toDestroy.gameObject.tag == "planet" || toDestroy.gameObject.tag == "galaxy")
            {
                Destroy(toDestroy.gameObject);
            }
        }

        PlaceAsteroids();
        PlacePlanets();
        PlaceGalaxies();
    }

    void Start()
    {
        defPosAsteroids = transform.position;
    }

    void PlaceAsteroids()
    {
        for (int x = 0; x < numberOfAsteroidsOnAnAxis; x++)
        {
            for (int y = 0; y < numberOfAsteroidsOnAnAxis; y++)
            {
                for (int z = 0; z < numberOfAsteroidsOnAnAxis; z++)
                {
                    int spawnChance = Random.Range(0, 8);

                    if (spawnChance == 0)
                        InstantiateAsteroid(x, y, z);
                    else if (spawnChance == 1)
                        InstantiateAsteroid(x, -y, z);
                    else if (spawnChance == 2)
                        InstantiateAsteroid(x, -y, -z);
                    else if (spawnChance == 3)
                        InstantiateAsteroid(x, y, -z);
                    else if (spawnChance == 4)
                        InstantiateAsteroid(-x, y, z);
                    else if (spawnChance == 5)
                        InstantiateAsteroid(-x, -y, z);
                    else if (spawnChance == 6)
                        InstantiateAsteroid(-x, y, -z);
                    else if (spawnChance == 7)
                        InstantiateAsteroid(-x, -y, -z);                 
                }
            }
        }
    }

    void InstantiateAsteroid(int x, int y, int z)
    {
        int rand = Random.Range(0, asteroids.Length);

        float x_create = transform.position.x + (x * asteroidGridSpacing) + AsteroidOffset();
        float y_create = transform.position.y + (y * asteroidGridSpacing) + AsteroidOffset();
        float z_create = transform.position.z + (z * asteroidGridSpacing) + AsteroidOffset();

        if (x_create < 400 && x_create > -400 &&
            y_create < 400 && y_create > -400 &&
            z_create < 400 && z_create > -400)
        {
            var obstacle = Instantiate(asteroids[rand], new Vector3(x_create, y_create, z_create), Quaternion.identity);

            obstacle.transform.SetParent(transform, false);
        }
    }

    float AsteroidOffset()
    {
        return Random.Range(-asteroidGridSpacing / 2f, asteroidGridSpacing / 2f);
    }

    float PlanetOffset()
    {
        return Random.Range(-planetGridSpacing / 2f, planetGridSpacing / 2f);
    }

    float GalaxyOffset()
    {
        return Random.Range(-galaxyGridSpacing / 2f, galaxyGridSpacing / 2f);
    }

    void PlacePlanets()
    {
        for (int x = 0; x < numberOfPlanetsOnAnAxis; x++)
        {
            for (int y = 0; y < numberOfPlanetsOnAnAxis; y++)
            {
                for (int z = 0; z < numberOfPlanetsOnAnAxis; z++)
                {
                    int spawnChance = Random.Range(0, 8);

                    if (spawnChance == 0)
                        InstantiatePlanets(x, y, z);
                    else if (spawnChance == 1)
                        InstantiatePlanets(x, -y, z);
                    else if (spawnChance == 2)
                        InstantiatePlanets(x, -y, -z);
                    else if (spawnChance == 3)
                        InstantiatePlanets(x, y, -z);
                    else if (spawnChance == 4)
                        InstantiatePlanets(-x, y, z);
                    else if (spawnChance == 5)
                        InstantiatePlanets(-x, -y, z);
                    else if (spawnChance == 6)
                        InstantiatePlanets(-x, y, -z);
                    else if (spawnChance == 7)
                        InstantiatePlanets(-x, -y, -z);
                }
            }
        }
    }

    void InstantiatePlanets(int x, int y, int z)
    {
        int rand = Random.Range(0, planets.Length);

        float x_create = transform.position.x + (x * planetGridSpacing) + PlanetOffset();
        float y_create = transform.position.y + (y * planetGridSpacing) + PlanetOffset();
        float z_create = transform.position.z + (z * planetGridSpacing) + PlanetOffset();

        if (x_create < 400 && x_create > -400 &&
            y_create < 400 && y_create > -400 &&
            z_create < 400 && z_create > -400)
        {
            var obstacle = Instantiate(planets[rand], new Vector3(x_create, y_create, z_create), Quaternion.identity);
            obstacle.transform.SetParent(transform, false);
        }       
    }

    void PlaceGalaxies()
    {
        for (int x = 0; x < numberOfGalaxiesOnAnAxis; x++)
        {
            for (int y = 0; y < numberOfGalaxiesOnAnAxis; y++)
            {
                for (int z = 0; z < numberOfGalaxiesOnAnAxis; z++)
                {
                    int spawnChance = Random.Range(0, 8);

                    if (spawnChance == 0)
                        InstantiateGalaxies(x, y, z);
                    else if (spawnChance == 1)
                        InstantiateGalaxies(x, -y, z);
                    else if (spawnChance == 2)
                        InstantiateGalaxies(x, -y, -z);
                    else if (spawnChance == 3)
                        InstantiateGalaxies(x, y, -z);
                    else if (spawnChance == 4)
                        InstantiateGalaxies(-x, y, z);
                    else if (spawnChance == 5)
                        InstantiateGalaxies(-x, -y, z);
                    else if (spawnChance == 6)
                        InstantiateGalaxies(-x, y, -z);
                    else if (spawnChance == 7)
                        InstantiateGalaxies(-x, -y, -z);
                }
            }
        }
    }

    void InstantiateGalaxies(int x, int y, int z)
    {
        int rand = Random.Range(0, galaxies.Length);


        float x_create = transform.position.x + (x * galaxyGridSpacing) + GalaxyOffset();
        float y_create = transform.position.y + (y * galaxyGridSpacing) + GalaxyOffset();
        float z_create = transform.position.z + (z * galaxyGridSpacing) + GalaxyOffset();

        if (x_create < 400 && x_create > -400 &&
            y_create < 400 && y_create > -400 &&
            z_create < 400 && z_create > -400)
        {
            var obstacle = Instantiate(galaxies[rand], new Vector3(x_create, y_create, z_create), Quaternion.identity);

            obstacle.transform.SetParent(transform, false);
        }
    }
}
