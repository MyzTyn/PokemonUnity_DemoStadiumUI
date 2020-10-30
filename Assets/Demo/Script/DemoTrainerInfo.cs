using UnityEngine;
using PokemonUnity;
using PokemonUnity.Combat;

public class DemoTrainerInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Trainer trainer = new Trainer("Test", TrainerTypes.PLAYER);
        Debug.Log(trainer.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
