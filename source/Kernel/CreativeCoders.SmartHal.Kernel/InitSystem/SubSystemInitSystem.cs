using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Dependencies;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;

namespace CreativeCoders.SmartHal.Kernel.InitSystem
{
    public class SubSystemInitSystem : ISubSystemInitSystem
    {
        private static readonly ILogger Log = LogManager.GetLogger<SubSystemInitSystem>();

        private readonly IBootStep[] _bootSteps;

        private readonly ISubSystem[] _subSystems;

        private readonly IHaltStep[] _haltSteps;

        public SubSystemInitSystem(IEnumerable<ISubSystem> subSystems, IEnumerable<IBootStep> bootSteps, IEnumerable<IHaltStep> haltSteps)
        {
            _haltSteps = haltSteps.ToArray();
            _bootSteps = bootSteps.ToArray();
            _subSystems = subSystems.ToArray();
        }

        private IReadOnlyCollection<ISubSystem> GetSortedSubSystems()
        {
            var dependencyCollection = new DependencyObjectCollection<ISubSystem>();

            foreach (var subSystem in _subSystems)
            {
                var dependsOnSubSystems = subSystem
                    .DependsOnSubSystems
                    .Select(subSystemType =>
                        _subSystems.FirstOrDefault(subSystemType.IsInstanceOfType))
                    .WhereNotNull();

                dependencyCollection.AddDependency(subSystem, dependsOnSubSystems.ToArray());
            }

            dependencyCollection.RemoveRedundancies();

            var sorter = new DependencySorter<ISubSystem>(dependencyCollection);

            return sorter.Sort().ToArray();
        }

        private static IEnumerable<SubSystemInitSystemStepInfo<T>> GetSteps<T>(
            IReadOnlyCollection<ISubSystem> sortedSubSystems, T[] steps)
        {
            var bootStepInfos = steps.Select(x => new SubSystemInitSystemStepInfo<T>(x)).ToArray();

            return from subSystem in sortedSubSystems
                let bootStepInfo = bootStepInfos
                    .FirstOrDefault(x =>
                        x.SubSystemType.IsInstanceOfType(subSystem))
                where bootStepInfo != null
                select bootStepInfo.SetName(subSystem.Name);
        }

        public async Task ExecuteBootStepsAsync()
        {
            var sortedSubSystems = GetSortedSubSystems();

            Log.Info($"{sortedSubSystems.Count} SubSystem(s) found");

            sortedSubSystems.ForEach(x => Log.Info($"SubSystem: {x.Name}"));

            var bootStepInfos = GetSteps(sortedSubSystems, _bootSteps);

            await bootStepInfos.ForEachAsync(async x =>
            {
                Log.Info($"Executing '{x.Name}' boot step...");

                try
                {
                    await x.Step.ExecuteAsync().ConfigureAwait(false);

                    Log.Info($"Boot step '{x.Name}' executed.");
                }
                catch (Exception ex)
                {
                    Log.Error($"Boot step '{x.Name}' execution failed", ex);
                }
            });
        }

        public async Task ExecuteHaltStepsAsync()
        {
            var sortedSubSystems = GetSortedSubSystems().Reverse().ToArray();

            var haltSteps = GetSteps(sortedSubSystems, _haltSteps);

            await haltSteps.ForEachAsync(async x =>
            {
                Log.Info($"Executing '{x.Name}' halt step...");

                await x.Step.ExecuteAsync().ConfigureAwait(false);

                Log.Info($"Halt step '{x.Name}' executed.");
            });
        }
    }
}