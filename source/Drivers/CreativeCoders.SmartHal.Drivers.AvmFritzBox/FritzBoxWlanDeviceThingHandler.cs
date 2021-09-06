using System;
using System.Threading;
using System.Threading.Tasks;
using CreativeCoders.Core.Logging;
using CreativeCoders.Net.Avm;
using CreativeCoders.SmartHal.Drivers.Base;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Channels;
using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Drivers.AvmFritzBox
{
    public class FritzBoxWlanDeviceThingHandler : ThingHandlerBase
    {
        private static readonly ILogger Log = LogManager.GetLogger<FritzBoxWlanDeviceThingHandler>();
        
        private readonly IThingSetupInfo _thingSetupInfo;
        
        private readonly FritzBox _fritzBox;

        private readonly SimpleThingChannelHandler<bool> _isActiveChannelHandler;
        
        private readonly SimpleThingChannelHandler<DateTime> _lastConnectChannelHandler;
        
        private readonly SimpleThingChannelHandler<DateTime> _lastDisconnectChannelHandler;
        
        private readonly string _hostAddress;
        
        private readonly int _interval;
        
        private Timer _timer;

        private readonly bool _checkMethodHostIsActive;

        public FritzBoxWlanDeviceThingHandler(IThingSetupInfo thingSetupInfo, FritzBox fritzBox)
        {
            _thingSetupInfo = thingSetupInfo;
            _fritzBox = fritzBox;

            _isActiveChannelHandler = new SimpleThingChannelHandler<bool>(_thingSetupInfo.Id, "IsActive");
            _lastConnectChannelHandler = new SimpleThingChannelHandler<DateTime>(_thingSetupInfo.Id, "LastConnect");
            _lastDisconnectChannelHandler = new SimpleThingChannelHandler<DateTime>(_thingSetupInfo.Id, "LastDisconnect");
            
            _hostAddress = _thingSetupInfo.Address;
            _interval = _thingSetupInfo.ReadSetting("Interval", 10000);
            _checkMethodHostIsActive = _thingSetupInfo.ReadSetting<string>("CheckMode") == "HostIsActive";
        }

        protected override Task<ThingState> OnInitAsync()
        {
            MessageHub.SendMessage(new NewThingChannelMessage(_thingSetupInfo.Id, _isActiveChannelHandler));
            MessageHub.SendMessage(new NewThingChannelMessage(_thingSetupInfo.Id, _lastConnectChannelHandler));
            MessageHub.SendMessage(new NewThingChannelMessage(_thingSetupInfo.Id, _lastDisconnectChannelHandler));

            StartTimer();
            
            return Task.FromResult(ThingState.Online);
        }
        
        private void StartTimer()
        {
            if (_timer != null)
            {
                return;
            }
            _timer = new Timer(_ => CheckIsActive(), null, 0, _interval);
        }
        
        private void CheckIsActive()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);

            try
            {
                var isActive = DeviceIsActive();

                if (isActive && (bool?)_isActiveChannelHandler.Value == false)
                {
                    _lastConnectChannelHandler.SendValueUpdate(DateTime.Now);
                }
                if (!isActive && (bool?)_isActiveChannelHandler.Value == true)
                {
                    _lastDisconnectChannelHandler.SendValueUpdate(DateTime.Now);
                }
                _isActiveChannelHandler.SendValueUpdate(isActive);
            }
            catch (Exception ex)
            {
                Log.Error($"Host.CheckIsActive '{_thingSetupInfo.Id.Thing}' failed", ex);
            }
            finally
            {
                _timer.Change(_interval, Timeout.Infinite);
            }
        }

        private bool DeviceIsActive()
        {
            try
            {
                if (_checkMethodHostIsActive)
                {
                    var hostEntry = _fritzBox.Hosts.GetHostEntry(_hostAddress);

                    return hostEntry?.IsActive == true;
                }

                var device = _fritzBox.Wlan.GetWlanDeviceInfo(_hostAddress);

                return device.Speed > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}