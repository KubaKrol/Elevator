namespace Assets.Scripts.Input
{
    public class MouseInput : IGameInput
    {
        public bool LeftMouseButtonPressed()
        {
            return UnityEngine.Input.GetMouseButtonDown(0);
        }
    }
}
