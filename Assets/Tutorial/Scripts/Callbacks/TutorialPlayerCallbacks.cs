using Bolt.AdvancedTutorial;
using UnityEngine;

[BoltGlobalBehaviour("Level2")]
public class TutorialPlayerCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string map)
    {
        // this just instantiates our player camera,
        // the Instantiate() method is supplied by the BoltSingletonPrefab<T> class
        PlayerCamera.Instantiate();
    }

    public override void ControlOfEntityGained(BoltEntity entity)
    {
        // give the camera our players pitch
        PlayerCamera.instance.getPitch = () => entity.GetState<ITutorialPlayerState>().pitch;

        // this tells the player camera to look at the entity we are controlling
        PlayerCamera.instance.SetTarget(entity);

        // add an audio listener for our character
        entity.gameObject.AddComponent<AudioListener>();
    }
}
