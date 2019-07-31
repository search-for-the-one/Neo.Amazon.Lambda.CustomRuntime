using System.Threading.Tasks;

namespace Neo.Amazon.Lambda.CustomRuntime
{
    public interface IRunableFunction
    {
        Task Run();
    }
}