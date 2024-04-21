using InecoBankTask.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InecoBankTask.Services.Implementations
{
    public class MyHostedService : IHostedService, IDisposable
    {
        private System.Threading.Timer _timer;
        private readonly TimeSpan _desiredTimeOfDay = new TimeSpan(09, 0, 0);
        private readonly IServiceProvider _serviceProvider;

        public MyHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Calculate time until first execution
            TimeSpan timeUntilFirstExecution = CalculateTimeUntilFirstExecution();

            // Set up timer
            _timer = new System.Threading.Timer(TimerCallback, null, timeUntilFirstExecution, TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Dispose timer
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void TimerCallback(object state)
        {
            // Check if current time matches desired time
            if (DateTime.Now.TimeOfDay >= _desiredTimeOfDay &&
                DateTime.Now.TimeOfDay < _desiredTimeOfDay.Add(TimeSpan.FromMinutes(15))) // 09:00 - 09:15
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var discService = scope.ServiceProvider.GetRequiredService<IDiscountService>();
                    await discService.SetDiscount();
                }
            }
        }

        private TimeSpan CalculateTimeUntilFirstExecution()
        {
            DateTime now = DateTime.Now;
            DateTime desiredTime = new DateTime(now.Year, now.Month, now.Day, _desiredTimeOfDay.Hours, _desiredTimeOfDay.Minutes, _desiredTimeOfDay.Seconds);

            if (now > desiredTime)
            {
                desiredTime = desiredTime.AddDays(1);
            }

            return desiredTime - now;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
