namespace NITHemulation.Modules.Mouse
{
    public interface IMouseBehavior
    {
        int ReceiveSample(MouseDataSample sample);
    }
}