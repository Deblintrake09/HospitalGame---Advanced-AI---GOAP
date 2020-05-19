using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GStateMonitor : MonoBehaviour
{
    public string state;    // estado actual (cansado, con ganas de orinar, etc)
    public float stateStrength; //cuan fuerte es el estado
    public float stateDecayRate; //cuan rápido se reduce, cuando llega a 0 el personaje no aguanta más
    public WorldStates beliefs;  //estados del personaje
    public GameObject resourcePrefab; //el prefab de lo que va a dejar tirado cuando se cumpla el tiempo
    public string queueName; //la pila de recursos donde va a ser dejado el recurso
    public string worldState; //estado del mundo al que corresponde el estado
    public GAction action; // acción a la que aplica el estado, para que no se orine de camino al baño, por ejemplo

    bool stateFound = false;
    float initialStrength;

    void Awake()
    {
        beliefs = this.GetComponent<GAgent>().beliefs;
        initialStrength = stateStrength;
    }

    void LateUpdate()
    {   
        if (action.running) // si la acción está corriendo, no activar el estado
        {
            stateFound = false;
            stateStrength = initialStrength;
        }

        if (!stateFound && beliefs.HasState(state)) // si agente ya tiene el estado en sus beliefs, activarlo, para que empiece a correr el tiempo
            stateFound = true;
        //si se encuentra en el estado buscado, comienza a bajar el tiempo
        if (stateFound)
        { //cuando el tiempo llega a 0, se instancia el lo que deja atrás, en este caso un rastro de orina y se resetea el estado a 0.
            stateStrength -= stateDecayRate * Time.deltaTime;
            if (stateStrength <= 0)
            {
                Vector3 location = new Vector3(this.transform.position.x, resourcePrefab.transform.position.y, this.transform.position.z);
                GameObject p = Instantiate(resourcePrefab, location, resourcePrefab.transform.rotation);
                stateFound = false;
                stateStrength = initialStrength;
                beliefs.RemoveState(state);
                GWorld.Instance.GetResourceQueue(queueName).AddResourse(p);
                GWorld.Instance.GetWorld().ModifyState(worldState, 1);
            }
        }
    }
}
