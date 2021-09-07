using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Requests
{
    [PublicAPI]
    public class KernelRequest
    {
        private static int _idCounter;
        
        private readonly Func<Task> _execute;
        
        private string _name;

        public KernelRequest(Func<Task> execute)
        {
            _execute = execute;
            Id = Interlocked.Increment(ref _idCounter);
        }

        public KernelRequest(string name, Func<Task> execute) : this(execute)
        {
            _name = name;
        }

        public int Id { get; }

        public string DisplayName => _name ?? _execute.Method.Name;

        public async Task ExecuteAsync()
        {
            await _execute().ConfigureAwait(false);
        }
    }
}