namespace BikeSparesInventorySystem.Components.Pages;

public partial class Dashboard
{
    public const string Route = "/dashboard";

    private BarChartConfig Config;

    [CascadingParameter]
    private Action<string> SetAppBarTitle { get; set; }

    protected sealed override void OnInitialized()
    {
        SetAppBarTitle.Invoke("Dashboard");
        PSC.Blazor.Components.Chartjs.Models.Common.Font axisLabelFont = new()
        {
            Weight = "bold",
            Size = 16
        };

        Config = new BarChartConfig()
        {
            Options = new Options()
            {
                Responsive = true,
                Plugins = new Plugins()
                {
                    Crosshair = new Crosshair()
                    {
                        Horizontal = new CrosshairLine()
                        {
                            Color = "rgb(255, 99, 132)"
                        }
                    },
                    Zoom = new Zoom()
                    {
                        Enabled = true,
                        Mode = "xy",
                        ZoomOptions = new ZoomOptions()
                        {
                            Wheel = new Wheel()
                            {
                                Enabled = true
                            },
                            Pinch = new Pinch()
                            {
                                Enabled = true
                            },
                        }
                    },
                    Title = new Title()
                    {
                        Text = "Inventory Items",
                        Display = true,
                        Font = new PSC.Blazor.Components.Chartjs.Models.Common.Font()
                        {
                            Weight = "bold",
                            Size = 20
                        },
                        Position = PSC.Blazor.Components.Chartjs.Models.Common.Position.Top
                    },
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
                                Align= PSC.Blazor.Components.Chartjs.Models.Common.Align.Center,
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
                                Align= PSC.Blazor.Components.Chartjs.Models.Common.Align.Center,
                                Font = axisLabelFont
                            },
                        }
                    }
                }
            }
        };

        BarDataset ApprovedDeductionQuantitySet = new()
        {
            Data = new List<decimal?>(),
            BackgroundColor = new() { "rgb(0, 163, 68)" },
            Label = "Approved Deduction (Taken Out)",
        };

        BarDataset AvailableQuantitySet = new()
        {
            Data = new List<decimal?>(),
            BackgroundColor = new() { "rgb(111, 83, 255)" },
            Label = "Available"
        };

        BarDataset PendingDeductionQuantitySet = new()
        {
            Data = new List<decimal?>(),
            BackgroundColor = new() { "rgb(252, 152, 0)" },
            Label = "Pending Deduction (On Hold)"
        };

        BarDataset DisapprovedDeductionQuantitySet = new()
        {
            Data = new List<decimal?>(),
            BackgroundColor = new() { "rgb(255, 45, 13)" },
            Label = "Disapproved Deduction",
        };


        foreach (IGrouping<Guid, ActivityLog> group in ActivityLogRepository.GetAll().GroupBy(x => x.SpareID).ToList())
        {
            Spare spare = SpareRepository.Get(x => x.Id, group.Key);
            if (spare is null)
            {
                continue;
            }

            List<ActivityLog> deductedStock = group.Where(x => x.Action == StockAction.Deduct).ToList();
            int approved = deductedStock.Where(x => x.ApprovalStatus == ApprovalStatus.Approve).Sum(x => x.Quantity);
            int pending = deductedStock.Where(x => x.ApprovalStatus == ApprovalStatus.Pending).Sum(x => x.Quantity);
            int disapproved = deductedStock.Where(x => x.ApprovalStatus == ApprovalStatus.Disapprove).Sum(x => x.Quantity);

            Config.Data.Labels.Add(spare.Name);

            ApprovedDeductionQuantitySet.Data.Add(approved);
            PendingDeductionQuantitySet.Data.Add(pending);
            DisapprovedDeductionQuantitySet.Data.Add(disapproved);

            AvailableQuantitySet.Data.Add(spare.AvailableQuantity);
        }

        Config.Data.Datasets.Add(ApprovedDeductionQuantitySet);
        Config.Data.Datasets.Add(PendingDeductionQuantitySet);
        Config.Data.Datasets.Add(AvailableQuantitySet);
        Config.Data.Datasets.Add(DisapprovedDeductionQuantitySet);
    }
}
