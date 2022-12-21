using BikeSparesInventorySystem.Shared;
using PSC.Blazor.Components.Chartjs;
using PSC.Blazor.Components.Chartjs.Models.Bar;
using PSC.Blazor.Components.Chartjs.Models.Common;

namespace BikeSparesInventorySystem.Pages;

public partial class Dashboard
{
    private BarChartConfig Config; 

    protected override void OnInitialized()
	{
        MainLayout.Title = "Dashboard";
        var axisLabelFont = new PSC.Blazor.Components.Chartjs.Models.Common.Font()
        {
            Weight = "bold",
            Size = 16
        };

        Config = new BarChartConfig()
        {            
            Options = new Options()
            {
                Responsive= true,
                Plugins = new Plugins()
                {
                    Title = new Title()
                    {
                        Text = "Inventory Items",
                        Display = true,
                        Position = Position.Top                        
                    },

                    Legend = new Legend()
                    {
                        Display = true,
                    }
                    
                },
                Scales = new Dictionary<string, Axis>()
                {
                    {
                        Scales.XAxisId, new Axis()
                        {
                            Stacked = true,
                            Ticks = new Ticks()
                            {
                                MaxRotation = 45,
                                MinRotation = 0                                
                            },                            
                            Title = new AxesTitle()
                            {
                                Text = "Spares",
                                Display = true,
                                Align= Align.Center,
                                Font = axisLabelFont
                            },
                        }
                    },
                    {
                        Scales.YAxisId, new Axis()
                        {                           
                            Stacked = true,
                            Title = new AxesTitle()
                            {
                                Text = "Quantity",
                                Display = true,
                                Align= Align.Center,
                                Font = axisLabelFont
                            },
                        }
                    }
                }
            }
        };

        var TakenOutQuantitySet = new BarDataset()
        {
            Data = new List<decimal>(),
            BackgroundColor= new () { "rgb(179, 150, 219)" },
            Label = "Taken Out",           
        };

        var AvailableQuantitySet = new BarDataset()
        {
            Data = new List<decimal>(),
            BackgroundColor = new() { "rgb(111, 83, 255)" },
            Label = "Available"
        };

        var OnHoldQuantitySet = new BarDataset()
        {
            Data = new List<decimal>(),
            BackgroundColor = new() { "rgb(255, 45, 13)" },
            Label = "On Hold"
        };

        foreach (var group in ActivityLogRepository.GetAll().GroupBy(x => x.SpareID).ToList())
        {
            var l = SpareRepository.Get(x => x.Id, group.Key).Name;
            var takenOut = group.Where(x => !x.Approver.Equals(Guid.Empty)).Sum(x => x.Quantity);
            var onHold = group.Where(x => x.Approver.Equals(Guid.Empty)).Sum(x => x.Quantity);

            Config.Data.Labels.Add(l);
            TakenOutQuantitySet.Data.Add(takenOut);
            OnHoldQuantitySet.Data.Add(onHold);
            AvailableQuantitySet.Data.Add(SpareRepository.Get(x => x.Id, group.Key).AvailableQuantity);
        }

        Config.Data.Datasets.Add(TakenOutQuantitySet);
        Config.Data.Datasets.Add(OnHoldQuantitySet);
        Config.Data.Datasets.Add(AvailableQuantitySet);
    }
}
