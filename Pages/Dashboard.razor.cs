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
                        Font = new PSC.Blazor.Components.Chartjs.Models.Common.Font()
                        {
                            Weight = "bold",
                            Size = 20
                        },
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

        var ApprovedDeductionQuantitySet = new BarDataset()
        {
            Data = new List<decimal>(),
            BackgroundColor= new () { "rgb(0, 163, 68)" },
            Label = "Approved Deduction (Taken Out)",           
        };

        var AvailableQuantitySet = new BarDataset()
        {
            Data = new List<decimal>(),
            BackgroundColor = new() { "rgb(111, 83, 255)" },
            Label = "Available"
        };

        var PendingDeductionQuantitySet = new BarDataset()
        {
            Data = new List<decimal>(),
            BackgroundColor = new() { "rgb(252, 152, 0)" },
            Label = "Pending Deduction (On Hold)"
        };

        var DisapprovedDeductionQuantitySet = new BarDataset()
        {
            Data = new List<decimal>(),
            BackgroundColor = new() { "rgb(255, 45, 13)" },
            Label = "Disapproved Deduction",
        };


        foreach (var group in ActivityLogRepository.GetAll().GroupBy(x => x.SpareID).ToList())
        {
            var l = SpareRepository.Get(x => x.Id, group.Key).Name;
            var approved = group.Where(x => x.ApprovalStatus == Data.Enums.ApprovalStatus.Approve && x.Action == Data.Enums.InventoryAction.Deduct).Sum(x => x.Quantity);
            var pending = group.Where(x => x.ApprovalStatus == Data.Enums.ApprovalStatus.Pending && x.Action == Data.Enums.InventoryAction.Deduct).Sum(x => x.Quantity);
            var disapproved = group.Where(x => x.ApprovalStatus == Data.Enums.ApprovalStatus.Disapprove && x.Action == Data.Enums.InventoryAction.Deduct).Sum(x => x.Quantity);
            
            Config.Data.Labels.Add(l);

            ApprovedDeductionQuantitySet.Data.Add(approved);
            PendingDeductionQuantitySet.Data.Add(pending);
            DisapprovedDeductionQuantitySet.Data.Add(disapproved);
            AvailableQuantitySet.Data.Add(SpareRepository.Get(x => x.Id, group.Key).AvailableQuantity);
        }

        Config.Data.Datasets.Add(ApprovedDeductionQuantitySet);
        Config.Data.Datasets.Add(PendingDeductionQuantitySet);
        Config.Data.Datasets.Add(AvailableQuantitySet);
        Config.Data.Datasets.Add(DisapprovedDeductionQuantitySet);
    }
}
