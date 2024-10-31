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
        id = 1,
        Price = 50.50M,
        Style = "17-inch Pair Radial"
    },
    new Wheels()
    {
        id = 2,
        Price = 50.50M,
        Style = "17-inch Pair Radial Black"
    },
    new Wheels()
    {
        id = 3,
        Price = 75.50M,
        Style = "18-inch Pair Spoke Silver"
    },
    new Wheels()
    {
        id = 4,
        Price = 75.50M,
        Style = "18-inch Pair Spoke Black"
    }
};

List<Order> orders = new List<Order>
{
    new Order()
    {
        id = 1,
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

app.Run();
