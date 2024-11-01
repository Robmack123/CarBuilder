using CarBuilder.Models;
using CarBuilder.Models.DTOs;

List<PaintColor> paintColors = new List<PaintColor>
{
    new PaintColor()
    {
        Id = 1,
        Price = 50.50M,
        Color = "Midnight Blue"
    },
    new PaintColor()
    {
        Id = 2,
        Price = 50.50M,
        Color = "Silver"
    },
    new PaintColor()
    {
        Id = 3,
        Price = 100.25M,
        Color = "Firebrick Red"
    },
    new PaintColor()
    {
        Id = 4,
        Price = 110.99M,
        Color = " Spring Green",
    }
};
List<Interior> interiors = new List<Interior>
{
    new Interior()
    {
        Id = 1,
        Price = 50.50M,
        Material = "Beige Fabric"
    },
    new Interior()
    {
        Id = 2,
        Price = 50.50M,
        Material = "Charcoal Fabric"
    },
    new Interior()
    {
        Id = 3,
        Price = 150.50M,
        Material = "White Leather"
    },
    new Interior()
    {
        Id = 4,
        Price = 150.99M,
        Material = "Black Leather"
    }
};

List<Technology> technologies = new List<Technology>
{
    new Technology()
    {
        Id = 1,
        Price = 100.90M,
        Package = "Basic Package (basic sound system)"
    },
    new Technology()
    {
        Id = 2,
        Price = 300.99M,
        Package = "Navigation Package (includes integrated navigation controls"
    },
    new Technology()
    {
        Id = 3,
        Price = 300.99M,
        Package = "Visibility Package (includes side and rear cameras)"
    },
    new Technology()
    {
        Id = 4,
        Price = 549.99M,
        Package = "Ultra Package (includes navigation and visibility packages)"
    }
};

List<Wheels> wheels = new List<Wheels>
{
    new Wheels()
    {
        Id = 1,
        Price = 50.50M,
        Style = "17-inch Pair Radial"
    },
    new Wheels()
    {
        Id = 2,
        Price = 50.50M,
        Style = "17-inch Pair Radial Black"
    },
    new Wheels()
    {
        Id = 3,
        Price = 75.50M,
        Style = "18-inch Pair Spoke Silver"
    },
    new Wheels()
    {
        Id = 4,
        Price = 75.50M,
        Style = "18-inch Pair Spoke Black"
    }
};

List<Order> orders = new List<Order>
{
    new Order()
    {
        Id = 1,
        Timestamp = new DateTime (2024, 02, 02),
        WheelId = 2,
        TechnologyId = 1,
        PaintId = 3,
        InteriorId = 4
    }
};


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/wheels", () =>
{
    return wheels.Select(w => new WheelsDTO
    {
        Id = w.Id,
        Price = w.Price,
        Style = w.Style
    });
});

app.MapGet("/paintcolors", () => 
{
    return paintColors.Select(p => new PaintColorDTO
    {
        Id = p.Id,
        Price = p.Price,
        Color = p.Color
    });
});

app.MapGet("/interiors", () => 
{
    return interiors.Select(i => new InteriorDTO
    {
        Id = i.Id,
        Price = i.Price,
        Material = i.Material
    });
});

app.MapGet("/technologies", () => 
{
    return technologies.Select(t => new TechnologyDTO
    {
        Id = t.Id,
        Price = t.Price,
        Package = t.Package
    });
});

app.MapGet("/orders", () => 
{
    return orders.Select(o => new OrderDTO
    {
        Id = o.Id,
        Timestamp = o.Timestamp,
        WheelId = o.WheelId,
        Wheel = wheels.FirstOrDefault(w => w.Id == o.WheelId) != null 
        ? new WheelsDTO
        {
            Id = wheels.First(w => w.Id == o.WheelId).Id,
            Price = wheels.First(w => w.Id == o.WheelId).Price,
            Style = wheels.First(w => w.Id == o.WheelId).Style
        }
        : null, 
        TechnologyId = o.TechnologyId,
        Technology = technologies.FirstOrDefault(t => t.Id == o.TechnologyId) != null
        ? new TechnologyDTO
        {
            Id = technologies.First(t => t.Id == o.TechnologyId).Id,
            Price = technologies.First(t => t.Id == o.TechnologyId).Price,
            Package = technologies.First(t => t.Id == o.TechnologyId).Package
            
        }
        : null,
        PaintId = o.PaintId,
        PaintColor = paintColors.FirstOrDefault(p => p.Id == o.PaintId) != null
        ? new PaintColorDTO
        {
            Id = paintColors.First(p => p.Id == o.PaintId).Id,
            Price = paintColors.First(p => p.Id == o.PaintId).Price,
            Color = paintColors.First(p => p.Id == o.PaintId).Color
        }
        : null,
        InteriorId = o.InteriorId,
        Interior = interiors.FirstOrDefault(i => i.Id == o.Id) != null
        ? new InteriorDTO
        {
            Id = interiors.First(i => i.Id == o.Id).Id,
            Price = interiors.First(i => i.Id == o.Id).Price,
            Material = interiors.First(i => i.Id == o.Id).Material
        }
        : null
    });
});


app.MapPost("/orders", (Order order) =>
{
    Wheels wheel = wheels.FirstOrDefault(w => w.Id == order.WheelId);
    Technology technology = technologies.FirstOrDefault(t => t.Id == order.TechnologyId);
    PaintColor paintColor = paintColors.FirstOrDefault(p => p.Id == order.PaintId);
    Interior interior = interiors.FirstOrDefault(i => i.Id == order.InteriorId);

    if (wheel == null || technology == null || paintColor == null || interior == null)
    {
        return Results.BadRequest("One or more related entities were not found.");
    }

    order.Id = orders.Any() ? orders.Max(o => o.Id) + 1 : 1;
    order.Timestamp = DateTime.Now;

    orders.Add(order);

    return Results.Created($"/orders/{order.Id}", new OrderDTO
     {
        Id = order.Id,
        Timestamp = order.Timestamp,
        WheelId = order.WheelId,
        Wheel = new WheelsDTO
        {
            Id = wheel.Id,
            Price = wheel.Price,
            Style = wheel.Style
        },
        TechnologyId = order.TechnologyId,
        Technology = new TechnologyDTO
        {
            Id = technology.Id,
            Price = technology.Price,
            Package = technology.Package
        },
        PaintId = order.PaintId,
        PaintColor = new PaintColorDTO
        {
            Id = paintColor.Id,
            Price = paintColor.Price,
            Color = paintColor.Color
        },
        InteriorId = order.InteriorId,
        Interior = new InteriorDTO
        {
            Id = interior.Id,
            Price = interior.Price,
            Material = interior.Material
        }
        
    });
});


app.Run();
