namespace CreativeCoders.SmartHal.Config.Base.Kernel
{
    public interface IKernelAssemblyReference
    {
        string Kind { get; }
        
        string Reference { get; }
    }
}