using JCsDiner;

namespace DinerBlazorServer.Services
{
    public class SimulationHostedService : BackgroundService
    {
        public Simulator Simulator { get; set; }
        public SimulatorArguments Arguments { get; set; }
        public SimulatorResults Results { get; set; }
        public bool CanStart { get; set; }

        public SimulationHostedService()
        {
            Simulator = new();
            Arguments = new();
            Results = new();
            CanStart = false;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (CanStart)
            {
                this.Simulator.StateChanged += Simulator_StateChanged;
                await Task.Run(() => Simulator.Run(Arguments));
            }
            RaiseStateChanged();
        }

        private void Simulator_StateChanged(object? sender, EventArgs e)
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseStateChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler StateChanged;
    }
}
