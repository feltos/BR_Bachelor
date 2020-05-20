using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server, "Level2")]
public class TutorialServerCallbacks : Bolt.GlobalEventListener
{
    void Awake()
    {
        TutorialPlayerObjectRegistry.CreateServerPlayer();
    }

    public override void Connected(BoltConnection connection)
    {
        TutorialPlayerObjectRegistry.CreateClientPlayer(connection);
    }

    public override void SceneLoadLocalDone(string map)
    {
        TutorialPlayerObjectRegistry.ServerPlayer.Spawn();
    }

    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        TutorialPlayerObjectRegistry.GetTutorialPlayer(connection).Spawn();
    }
}
